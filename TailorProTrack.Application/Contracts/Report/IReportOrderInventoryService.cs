using TailorProTrack.Application.Core;

namespace TailorProTrack.Application.Contracts.Report
{
    public interface IReportOrderInventoryService
    {
        ServiceResult GetDiffItemsByPreOrderId(int preOrderId);
    }
}
