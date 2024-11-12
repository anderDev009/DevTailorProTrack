

namespace TailorProTrack.domain.Reports
{
    public class MissedInventory
    {
        public string NombreProducto { get; set; }
        public string Size { get; set; }
        public string ColorPrimary { get; set; }
        public string ColorSecondary { get; set; }
        public int CantidadFaltante { get; set; }
    }
}
