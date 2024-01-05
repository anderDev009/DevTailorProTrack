namespace TailorProTrack.Application.Dtos.Order
{
    public class OrderDtoGet
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public decimal Amount { get; set; }
        public bool Checked { get; set; }
        public string DescriptionJob { get; set; }
        public string? StatusOrder {  get; set; }
        public dynamic Quantity { get; set; }

    }
}
