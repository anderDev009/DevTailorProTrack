
namespace TailorProTrack.Application.Dtos.Order
{
    public class OrderDtoGetDetail
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public decimal Amount { get; set; }
        public dynamic Quantity { get; set; }

        public bool Checked { get; set; }   
        public string? DescriptionJob { get; set; }  
        public string? Observation { get; set; }

		public dynamic Products {  get; set; }

    }
}
