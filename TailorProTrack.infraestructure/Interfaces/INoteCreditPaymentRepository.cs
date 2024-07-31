

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
	public interface INoteCreditPaymentRepository : IBaseRepository<NoteCreditPayment>
	{
		void RemoveNoteCreditPaymentByPaymentId(int idPayment);
	}
}
