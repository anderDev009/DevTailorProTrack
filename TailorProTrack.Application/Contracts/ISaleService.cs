

using TailorProTrack.Application.Core;
using TailorProTrack.Application.Dtos.Sale;
using TailorProTrack.domain.Entities;

namespace TailorProTrack.Application.Contracts
{
    public interface ISaleService : IBaseServiceGeneric<SaleDtoAdd, SaleDtoUpdate, SaleDtoGet, Sales>
    {
        ServiceResult GetSalesInvoicedByDate(DateTime startDate, DateTime endDate);

    }
}
