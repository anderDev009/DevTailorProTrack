
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

		public PaymentRepository(TailorProTrackContext context, IPreOrderProductsRepository preOrderProductRepository,
			INoteCreditRepository noteCreditRepository) : base(context)
		{
			_context = context;
			_noteCreditRepository = noteCreditRepository;
			_preOrderProductRepository = preOrderProductRepository;
		}

		

		public override int Save(Payment entity)
		{
			entity.CREATED_AT = DateTime.Now;
			entity.USER_CREATED = 1;
			//logica para sumarle el monto a la cuenta
			if (entity.FK_BANK_ACCOUNT != null && entity.FK_BANK_ACCOUNT != 0)
			{
				BankAccount account = _context.Set<BankAccount>().Find((int)entity.FK_BANK_ACCOUNT);
				if (account == null)
				{
					throw new Exception("Cuenta de banco invalida.");
				}
				//sumandole el monto a la cuenta
				account.BALANCE += entity.AMOUNT;
				//actualizando el monto
				_context.Set<BankAccount>().Update(account);
			}

			ConfirmPayment(entity.FK_ORDER);
			this._context.Add(entity);
			this._context.SaveChanges();
			//obtener el monto pendiente para confirmar si es necesario crear una nota de credito
			//en caso de que sea negativo se toma en cuenta una nota de credito
			decimal amountPending = GetAmountPendingByIdPreOrder(entity.FK_ORDER);
			if (amountPending < 0)
			{
				_noteCreditRepository.Save(new NoteCredit
				{
					AMOUNT = Math.Abs(amountPending),
					FK_CLIENT = _context.Set<PreOrder>().Find(entity.FK_ORDER).FK_CLIENT,
					FK_PAYMENT = entity.ID
				});
			}

			return entity.ID;
		}



		public override void Remove(Payment entity)
		{
			entity = GetEntity(entity.ID);

			//logica para restarle el saldo en caso de que un pago sea cancelado
			if (entity.FK_BANK_ACCOUNT != null && entity.FK_BANK_ACCOUNT > 0)
			{
				BankAccount account = _context.Set<BankAccount>().Find(entity.FK_BANK_ACCOUNT);
				account.BALANCE -= entity.AMOUNT;
				_context.Set<BankAccount>().Update(account);
				_context.SaveChanges();
			}

			//validacion para  saber si el pedido ha sido pagado por completo
			decimal amountPending = GetAmountPendingByIdPreOrder(entity.FK_ORDER);
			if (amountPending > 0)
			{
				PreOrder preOrder = _context.Set<PreOrder>().Find(entity.FK_ORDER);
				preOrder.COMPLETED = false;
				_context.Set<PreOrder>().Update(preOrder);
				_context.SaveChanges();
			}
			//revisar si tiene notas de credito disponibles
			var noteCredits = _noteCreditRepository.SearchNoteCreditByPaymentId(entity.ID);
			if (noteCredits.Count > 0)
			{
				foreach (var note in noteCredits)
				{
					_noteCreditRepository.Remove(note);
				}
			}

			_context.Remove(entity);
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
			return  amount - _context.Set<Payment>().Where(x => x.FK_ORDER == idPreOrder).Sum(x => x.AMOUNT);
		}	
	}
}
