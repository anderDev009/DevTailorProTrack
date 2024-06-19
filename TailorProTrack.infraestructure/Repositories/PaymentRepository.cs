
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


		public PaymentRepository(TailorProTrackContext context, IPreOrderProductsRepository preOrderProductRepository) : base(context)
		{
			_context = context;
			_preOrderProductRepository = preOrderProductRepository;
		}

		public override int Save(Payment entity)
		{
			entity.CREATED_AT = DateTime.Now;
			entity.USER_CREATED = 1;
			//logica para sumarle el monto a la cuenta
			if (entity.FK_BANK_ACCOUNT != null)
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

			this._context.Add(entity);
			this._context.SaveChanges();
			return entity.ID;
		}



		public override void Remove(Payment entity)
		{
			//logica para restarle el saldo en caso de que un pago sea cancelado
			if (entity.FK_BANK_ACCOUNT != 0)
			{
				BankAccount account = _context.Set<BankAccount>().Find(entity.FK_BANK_ACCOUNT);
				account.BALANCE += entity.AMOUNT;
				_context.Set<BankAccount>().Update(account);
				_context.SaveChanges();
			}
			base.Remove(entity);
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
			return true;
		}

		public decimal GetAmountPendingByIdPreOrder(int idPreOrder)
		{
			var amount = _preOrderProductRepository.GetAmountByIdPreOrder(idPreOrder);
			var extra = (decimal)((double)amount * 18) / 100;
			amount += (decimal)extra;
			return _context.Set<Payment>().Where(x => x.FK_ORDER == idPreOrder).Sum(x => x.AMOUNT) - amount;
		}	
	}
}
