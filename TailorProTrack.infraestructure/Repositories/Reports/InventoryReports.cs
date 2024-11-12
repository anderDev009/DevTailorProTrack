

using Microsoft.EntityFrameworkCore;
using TailorProTrack.domain.Reports;
using TailorProTrack.infraestructure.Context;
using TailorProTrack.infraestructure.Interfaces.Reports;

namespace TailorProTrack.infraestructure.Repositories.Reports
{
    public class InventoryReports : IInventoryReports
    {
        private readonly TailorProTrackContext _ctx;
        public InventoryReports(TailorProTrackContext ctx)
        {
            _ctx = ctx;
        }

        public List<MissedInventory> GetMissedInventory()
        {
            var missedInv = _ctx.Set<MissedInventory>()
                .FromSqlRaw("exec GetPendingOrderProducts")
                .ToList();
            return missedInv;
        }
    }
}
