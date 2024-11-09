
using TailorProTrack.domain.Reports;

namespace TailorProTrack.infraestructure.Interfaces.Reports
{
    public interface IInventoryReports
    {
        List<MissedInventory> GetMissedInventory();
    }
}
