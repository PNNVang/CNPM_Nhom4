namespace Dot_Net_ECommerceWeb.Model
{
    public class OrderSummaryViewModel
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public float? TotalPrice { get; set; }
        public string? Status { get; set; }
        public string? StatusPayment { get; set; }
    }

}
