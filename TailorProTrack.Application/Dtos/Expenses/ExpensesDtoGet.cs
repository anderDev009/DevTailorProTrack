

namespace TailorProTrack.Application.Dtos.Expenses
{
    public class ExpensesDtoGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string? Voucher {  get; set; }
        public string? DocumentNumber { get; set; }
        public bool? Completed { get; set; }

    }
}
