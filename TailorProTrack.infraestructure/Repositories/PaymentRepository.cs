
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
	{
		private readonly TailorProTrackContext _context;
		private readonly IPreOrderProductsRepository _preOrderProductRepository;
		private readonly INoteCreditRepository _noteCreditRepository;
		private readonly IBankAccountRepository _bankAccountRepository;
		private readonly INoteCreditPaymentRepository _noteCreditPaymentRepository;

		public PaymentRepository(TailorProTrackContext context, IPreOrderProductsRepository preOrderProductRepository,
			INoteCreditRepository noteCreditRepository, IBankAccountRepository bankaAccountRepository, INoteCreditPaymentRepository noteCreditPaymentRepository) : base(context)
		{
			_context = context;
			_noteCreditRepository = noteCreditRepository;
			_preOrderProductRepository = preOrderProductRepository;
			_bankAccountRepository = bankaAccountRepository;
			_noteCreditPaymentRepository = noteCreditPaymentRepository;
		}



		public override int Save(Payment entity)
		{
			entity.CREATED_AT = DateTime.Now;
			entity.USER_CREATED = 1;
			ValidateBankAccount(entity.FK_BANK_ACCOUNT);

			this._context.Add(entity);
			this._context.SaveChanges();
			RecalculateBankAccount(entity.FK_BANK_ACCOUNT);
			ConfirmPayment(entity.FK_ORDER);
			//obtener el monto pendiente para confirmar si es necesario crear una nota de credito
			//en caso de que sea negativo se toma en cuenta una nota de credito
			decimal amountPending = GetAmountPendingNegativeByIdPreOrder(entity.FK_ORDER);
			if (amountPending < 0)
			{
				int idClient = _context.Set<PreOrder>().Find(entity.FK_ORDER).FK_CLIENT;
				_noteCreditPaymentRepository.Save(new NoteCreditPayment
				{ 
					AMOUNT = Math.Abs(amountPending),
					FK_CREDIT = _noteCreditRepository.Save(new NoteCredit
					{
						FK_CLIENT = idClient,
						AMOUNT = 0,
					}),
					FK_PAYMENT = entity.ID
					
				});
				_noteCreditRepository.UpdateAmount(idClient);
			}

			return entity.ID;
		}



		public override void Remove(Payment entity)
		{
			entity = GetEntity(entity.ID);

			_noteCreditPaymentRepository.RemoveNoteCreditPaymentByPaymentId(entity.ID);

			int idPreOrder = entity.FK_ORDER;
			int? idBankAccount = entity.FK_BANK_ACCOUNT;
			_context.Remove(entity);
			_context.SaveChanges();
			RecalculateBankAccount(idBankAccount);
			ConfirmPayment(idPreOrder);


		}

		public override void Update(Payment entity)
		{
			var paymentToUpdate = GetEntity(entity.ID);
			if (paymentToUpdate == null)
			{
				throw new Exception("Pago no encontrado.");
			}

			bool hasNoteCredit = _context.Set<NoteCreditPayment>().Any(x => x.FK_PAYMENT == entity.ID);
			if (hasNoteCredit)
			{
				throw new Exception("No se puede actualizar un pago vinculado a una nota de credito. Eliminelo y registrelo nuevamente.");
			}

			ValidateBankAccount(entity.FK_BANK_ACCOUNT);

			int oldPreOrder = paymentToUpdate.FK_ORDER;
			int? oldBankAccount = paymentToUpdate.FK_BANK_ACCOUNT;

			paymentToUpdate.AMOUNT = entity.AMOUNT;
			paymentToUpdate.ACCOUNT_PAYMENT = entity.ACCOUNT_PAYMENT;
			paymentToUpdate.ACCOUNT_NUMBER = entity.ACCOUNT_NUMBER;
			paymentToUpdate.FK_BANK_ACCOUNT = entity.FK_BANK_ACCOUNT;
			paymentToUpdate.FK_ORDER = entity.FK_ORDER;
			paymentToUpdate.FK_TYPE_PAYMENT = entity.FK_TYPE_PAYMENT;
			paymentToUpdate.MODIFIED_AT = DateTime.Now;
			paymentToUpdate.USER_MOD = entity.USER_MOD;

			_context.Set<Payment>().Update(paymentToUpdate);
			_context.SaveChanges();

			RecalculateBankAccount(oldBankAccount);
			RecalculateBankAccount(entity.FK_BANK_ACCOUNT);
			ConfirmPayment(oldPreOrder);
			ConfirmPayment(entity.FK_ORDER);
		}

		public bool ConfirmPayment(int idPreOrder)
		{
			var preOrder = _context.Set<PreOrder>().Find(idPreOrder);
			if (preOrder == null)
			{
				throw new Exception("Pedido no encontrado.");
			}

			preOrder.COMPLETED = GetAmountPendingNegativeByIdPreOrder(idPreOrder) <= 0;
			_context.Set<PreOrder>().Update(preOrder);
			_context.SaveChanges();
			return preOrder.COMPLETED == true;
		}

		public decimal GetAmountPendingByIdPreOrder(int idPreOrder)
		{
			var preOrder = _context.Set<PreOrder>().Find(idPreOrder);
			var amount = _preOrderProductRepository.GetAmountByIdPreOrder(idPreOrder);
			if (preOrder.ITBIS != null && preOrder.ITBIS != false)
			{
				var extra = (decimal)((double)amount * 18) / 100;
				amount += (decimal)extra;
			}
			return _context.Set<Payment>().Where(x => x.FK_ORDER == idPreOrder).Sum(x => x.AMOUNT) - amount;
		}

		public decimal GetDebitAmount(int idAccount)
		{
			return _context.Set<Payment>().Where(x => x.FK_BANK_ACCOUNT == idAccount).Sum(x => x.AMOUNT);
		}

		public decimal GetDebitAmountThisMonth(int idAccount)
		{
			var now = DateTime.Now;
			var firstDayMonth = new DateTime(now.Year, now.Month, 1);
			return _context.Set<Payment>().Where(x => x.CREATED_AT >= firstDayMonth && x.FK_BANK_ACCOUNT == idAccount).Sum(x => x.AMOUNT);
		}
		public bool SaveWithNoteCredit(Payment entity)
		{

			var note = _noteCreditRepository.SearchNoteCreditByClientId(_context.Set<PreOrder>()
				.First(x => x.ID == entity.FK_ORDER).FK_CLIENT);

			if (note is not { AMOUNT: > 0 }) throw new Exception("No se puede realizar el pago con nota de credito.");


			var cashAmount = entity.AMOUNT;
			var amountToUse = note.AMOUNT;
			//obteniendo el monto pendiente para compararlo con la nota de credito
			var amountPending = Math.Max(GetAmountPendingNegativeByIdPreOrder(entity.FK_ORDER) - cashAmount, 0);
			//---
			//realizar un pago con nota de credito
			if (amountPending < note.AMOUNT)
			{
				amountToUse = amountPending;
			}

			if (cashAmount > 0)
			{
				Save(new Payment
				{
					AMOUNT = cashAmount,
					ACCOUNT_NUMBER = entity.ACCOUNT_NUMBER,
					ACCOUNT_PAYMENT = entity.ACCOUNT_PAYMENT,
					FK_BANK_ACCOUNT = entity.FK_BANK_ACCOUNT,
					FK_ORDER = entity.FK_ORDER,
					FK_TYPE_PAYMENT = entity.FK_TYPE_PAYMENT,
					USER_CREATED = entity.USER_CREATED
				});
			}

			if (amountToUse <= 0)
			{
				return true;
			}

			entity.AMOUNT = amountToUse;
			entity.FK_BANK_ACCOUNT = null;
			entity.NOTE_CREDIT = true;
			entity.CREATED_AT = DateTime.Now;
			entity.USER_CREATED = 1;
			this._context.Add(entity);
			this._context.SaveChanges();
			ConfirmPayment(entity.FK_ORDER);
			
			_noteCreditPaymentRepository.Save(new NoteCreditPayment
			{
				AMOUNT = -amountToUse,
				FK_CREDIT = note.ID,
				FK_PAYMENT = entity.ID
			});
			_noteCreditRepository.UpdateAmount(note.FK_CLIENT);
			return true;

		}

		public decimal GetAmountPendingNegativeByIdPreOrder(int idPreOrder)
		{
			var preOrder = _context.Set<PreOrder>().Find(idPreOrder);
			var amount = _preOrderProductRepository.GetAmountByIdPreOrder(idPreOrder);
			if (preOrder.ITBIS != null && preOrder.ITBIS != false)
			{
				var extra = (decimal)((double)amount * 18) / 100;
				amount += (decimal)extra;
			}
			return amount - _context.Set<Payment>().Where(x => x.FK_ORDER == idPreOrder).Sum(x => x.AMOUNT);
		}

		public List<Payment> DetailBankAccount(int idBankAccount)
		{
			return _context.Set<Payment>().Where(x => x.FK_BANK_ACCOUNT == idBankAccount).ToList();
		}

        public bool CheckIsLastPaymentPreOrder(int paymentId)
        {
			var payment = _context.Set<Payment>().Find(paymentId);
            var payments = _context.Set<Payment>().Where(x => x.FK_ORDER == payment.FK_ORDER).ToList();
            if (payments.Count == 1)
            {
                return true;
            }
            if (payments.OrderBy(x => x.ID).Last().ID == paymentId)
            {
				//validando si la nota de credito tiene monto para restarle en el caso de que si este vinculado a una nota de credito
				var noteCreditPayment = _context.Set<NoteCreditPayment>().Where(x => x.FK_PAYMENT == paymentId).FirstOrDefault();
				if(noteCreditPayment != null)
				{
					var noteCredit = _context.Set<NoteCredit>().Find(noteCreditPayment.FK_CREDIT);
					if(noteCredit.AMOUNT < payment.AMOUNT)
					{
						return false;
					}
                }
                return true;
            }
            return false;
        }

        private void ValidateBankAccount(int? idBankAccount)
        {
	        if (idBankAccount == null || idBankAccount == 0)
	        {
		        return;
	        }

	        if (_bankAccountRepository.GetEntity((int)idBankAccount) == null)
	        {
		        throw new Exception("Cuenta de banco invalida.");
	        }
        }

        private void RecalculateBankAccount(int? idBankAccount)
        {
	        if (idBankAccount == null || idBankAccount == 0)
	        {
		        return;
	        }

	        var account = _context.Set<BankAccount>().Find(idBankAccount);
	        if (account == null)
	        {
		        throw new Exception("Cuenta de banco invalida.");
	        }

	        account.DEBIT_AMOUNT = GetDebitAmount((int)idBankAccount);
	        account.BALANCE = account.DEBIT_AMOUNT - account.CREDIT_AMOUNT;
	        _context.Set<BankAccount>().Update(account);
	        _context.SaveChanges();
        }
    }
}
