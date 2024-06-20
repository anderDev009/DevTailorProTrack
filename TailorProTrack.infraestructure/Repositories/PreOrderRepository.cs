

using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
    public class PreOrderRepository : BaseRepository<PreOrder>, IPreOrderRepository
    {
        private readonly TailorProTrackContext _ctx;
        private readonly IPaymentRepository _paymentRepository;
        public PreOrderRepository(TailorProTrackContext ctx,IPaymentRepository paymentRepository) : base(ctx)
        {
                _ctx = ctx;
                _paymentRepository = paymentRepository;
        }
        //obtener cuentas por cobrar
        public List<PreOrder> GetAccountsReceivable()
        {
            var preOrders = _ctx.Set<PreOrder>()
                .Include(x => x.Client)
                .Where(x => x.REMOVED == false && x.COMPLETED == null || x.COMPLETED == false).ToList();

            List<PreOrder> preOrderReport = new();
            foreach(var item in preOrders)
            {
                if (!_paymentRepository.ConfirmPayment(item.ID))
                {
                    preOrderReport.Add(item);
                }

            }
            return preOrderReport;
        }
        //trae los 10 pedidos recientes
        public List<PreOrder> GetPreOrdersByRecentDate()
        {
            return _ctx.Set<PreOrder>().OrderBy(x => x.CREATED_AT).Take(10).ToList();
        }

        public void Complete(int id)
        {
	        var entity = GetEntity(id);
	        entity.COMPLETED = true;
			Update(entity);
		}

        public bool PreOrderIsEditable(int id)
        {
            return !_ctx.Set<Order>().Any(x => x.FK_PREORDER == id);
        }

      
    }
}
