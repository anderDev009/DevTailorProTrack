

using TailorProTrack.Application.Core;

namespace TailorProTrack.Application.Contracts.Report
{
    public interface IReportOrderInventoryService
    {
        ServiceResult GetDiffItemsByOrderId(int orderId);
    }
}
