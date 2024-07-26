
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

		public PaymentRepository(TailorProTrackContext context, IPreOrderProductsRepository preOrderProductRepository,
			INoteCreditRepository noteCreditRepository, IBankAccountRepository bankaAccountRepository) : base(context)
		{
			_context = context;
			_noteCreditRepository = noteCreditRepository;
			_preOrderProductRepository = preOrderProductRepository;
			_bankAccountRepository = bankaAccountRepository;
		}

		

		public override int Save(Payment entity)
		{
			entity.CREATED_AT = DateTime.Now;
			entity.USER_CREATED = 1;
			//logica para sumarle el monto a la cuenta
			if (entity.FK_BANK_ACCOUNT != null && entity.FK_BANK_ACCOUNT != 0)
			{
				var account = _bankAccountRepository.GetEntity((int)entity.FK_BANK_ACCOUNT);
				if (account == null)
				{
					throw new Exception("Cuenta de banco invalida.");
				}
				//sumandole el monto a la cuenta
				account.DEBIT_AMOUNT = GetDebitAmount((int)entity.FK_BANK_ACCOUNT);
                account.BALANCE = account.DEBIT_AMOUNT - account.CREDIT_AMOUNT;
                //actualizando el monto
				_context.Set<BankAccount>().Update(account);
			}

			ConfirmPayment(entity.FK_ORDER);
			this._context.Add(entity);
			this._context.SaveChanges();
			//obtener el monto pendiente para confirmar si es necesario crear una nota de credito
			//en caso de que sea negativo se toma en cuenta una nota de credito
			decimal amountPending = GetAmountPendingNegativeByIdPreOrder(entity.FK_ORDER);
			if (amountPending <  0)
			{
				_noteCreditRepository.Save(new NoteCredit
				{
					AMOUNT = Math.Abs(amountPending),
					FK_CLIENT = _context.Set<PreOrder>().Find(entity.FK_ORDER).FK_CLIENT,
				});
			}

			return entity.ID;
		}



		public override void Remove(Payment entity)
		{
			entity = GetEntity(entity.ID);

			
			//validacion para  saber si el pedido ha sido pagado por completo
			decimal amountPending = GetAmountPendingByIdPreOrder(entity.FK_ORDER);
			if (amountPending > 0)
			{
				PreOrder preOrder = _context.Set<PreOrder>().Find(entity.FK_ORDER);
				preOrder.COMPLETED = false;
				_context.Set<PreOrder>().Update(preOrder);
				var notecredit = _noteCreditRepository.SearchNoteCreditByClientId(preOrder.FK_CLIENT);
				_noteCreditRepository.ExtractAmount(notecredit.ID, entity.AMOUNT);
				_context.SaveChanges();
			}
			

			_context.Remove(entity);
			_context.SaveChanges();
			//logica para restarle el saldo en caso de que un pago sea cancelado
			if (entity.FK_BANK_ACCOUNT != null && entity.FK_BANK_ACCOUNT > 0)
			{
				BankAccount account = _context.Set<BankAccount>().Find(entity.FK_BANK_ACCOUNT);
				account.DEBIT_AMOUNT =entity.AMOUNT - this.GetDebitAmount((int)entity.FK_BANK_ACCOUNT);
				account.BALANCE = account.DEBIT_AMOUNT - account.CREDIT_AMOUNT;
				_context.Set<BankAccount>().Update(account);
				_context.SaveChanges();
			}

		}

		public bool ConfirmPayment(int idPreOrder)
		{
			//obteniendo el total del pedido
			decimal totalAmount = _preOrderProductRepository.GetAmountByIdPreOrder(idPreOrder);
			//confirmando el total
			decimal amountPreOrder = GetAmountPendingByIdPreOrder(idPreOrder);
			//retornando el bool
			if (totalAmount >= amountPreOrder)
			{
				return false;
			}

			var preOrder = _context.Set<PreOrder>().Find(idPreOrder);
			preOrder.COMPLETED = true;
			_context.Set<PreOrder>().Update(preOrder);
			_context.SaveChanges();
			return true;
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
			return   _context.Set<Payment>().Where(x => x.FK_ORDER == idPreOrder).Sum(x => x.AMOUNT) - amount;
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

			var entityToSend = new Payment();
			entityToSend = entity;

			if (note is not { AMOUNT: > 0 }) throw new Exception("No se puede realizar el pago con nota de credito.");

			
			var amountToUse = note.AMOUNT;
			//obteniendo el monto pendiente para compararlo con la nota de credito
			var amountPending = GetAmountPendingByIdPreOrder(entity.FK_ORDER);
			//---
			//realizar un pago con nota de credito
			if (amountPending < note.AMOUNT)
			{
				amountToUse = amountPending;
			}

			entity.AMOUNT = amountToUse;
			entity.FK_BANK_ACCOUNT = null;
			entity.CREATED_AT = DateTime.Now;
			entity.USER_CREATED = 1;
			ConfirmPayment(entity.FK_ORDER);
			this._context.Add(entity);
			this._context.SaveChanges();
			//obtener el monto pendiente para confirmar si es necesario crear una nota de credito
			//en caso de que sea negativo se toma en cuenta una nota de credito
			amountPending = GetAmountPendingByIdPreOrder(entity.FK_ORDER);
			if (amountPending < 0)
			{
				_noteCreditRepository.Save(new NoteCredit
				{
					AMOUNT = Math.Abs(amountPending),
					FK_CLIENT = _context.Set<PreOrder>().Find(entity.FK_ORDER).FK_CLIENT,
				});
			}
	
			entityToSend.ID = 0;
			Save(entityToSend);
			_noteCreditRepository.ExtractAmount(note.ID, amountToUse);
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
			return amount - _context.Set<Payment>().Where(x => x.FK_ORDER == idPreOrder).Sum(x => x.AMOUNT) ;
		}
	}
}
