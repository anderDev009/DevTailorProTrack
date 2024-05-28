

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TailorProTrack.domain.Core;

namespace TailorProTrack.domain.Entities
{
    public class Expenses : BaseEntity
    {
        public string NAME { get; set; }
        public string DESCR { get; set; }
        public decimal AMOUNT { get; set; }
        public string? VOUCHER { get; set; }
        public string? DOCUMENT_NUMBER { get; set; }
        public bool? COMPLETED {  get; set; }
    }
}
