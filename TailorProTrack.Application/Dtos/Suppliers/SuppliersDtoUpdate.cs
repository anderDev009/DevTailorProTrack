

using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace TailorProTrack.Application.Dtos.Suppliers
{
    public class SuppliersDtoUpdate
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string? Rnc { get; set; }
    }
}
