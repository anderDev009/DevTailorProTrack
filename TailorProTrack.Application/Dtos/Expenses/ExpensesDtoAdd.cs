
using System.ComponentModel.DataAnnotations;

namespace TailorProTrack.Application.Dtos.Expenses
{
    public class ExpensesDtoAdd  
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string? Voucher { get; set; } = null;
        public string? DocumentNumber { get; set; } = null;
        [Required]
        public int IdPaymentType {  get; set; }
    }
}
