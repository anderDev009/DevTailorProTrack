

using TailorProTrack.domain.Entities;
using TailorProTrack.infraestructure.Core;

namespace TailorProTrack.infraestructure.Interfaces
{
    public interface ISalesRepository : IBaseRepository<Sales>
    {
        void ConfirmSale(int idSales);
        List<Sales> GetSalesInvoicedByDate(DateTime startDate, DateTime endDate);
    }
}
