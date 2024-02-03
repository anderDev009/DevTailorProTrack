
namespace TailorProTrack.Application.Dtos.Expenses
{
    public class ExpensesDtoBase : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Voucher {  get; set; }
    }
}
