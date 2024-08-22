using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Core;
using TailorProTrack.infraestructure.Interfaces;

namespace TailorProTrack.infraestructure.Repositories
{
	public class NoteCreditPaymentRepository : BaseRepository<NoteCreditPayment>, INoteCreditPaymentRepository
	{
		private readonly TailorProTrackContext _ctx;
		private readonly INoteCreditRepository _noteCreditRepository;
		public NoteCreditPaymentRepository(TailorProTrackContext ctx, INoteCreditRepository noteCreditRepository) : base(ctx)
		{
			_ctx = ctx;
			_noteCreditRepository = noteCreditRepository;
		}

		public void RemoveNoteCreditPaymentByPaymentId(int idPayment)
		{
			var noteCreditPayment = _ctx.Set<NoteCreditPayment>().Where(x => x.FK_PAYMENT == idPayment).ToList();
			noteCreditPayment.ForEach(x => _noteCreditRepository.ExtractAmount((int)x.FK_CREDIT, x.AMOUNT));
			_ctx.Set<NoteCreditPayment>().RemoveRange(noteCreditPayment);
			_ctx.SaveChanges();
			
		}

		public override int Save(NoteCreditPayment entity)
		{
			return base.Save(entity);
		}

	}
}
