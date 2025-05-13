namespace Dot_Net_ECommerceWeb.Model
{
    public class OrderDetailViewModel
    {
        // Thông tin đơn hàng
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OrderStatus { get; set; }
        public string Note { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }

        // Thông tin khách hàng
        public int UserId { get; set; }
        public string UserFullName { get; set; }

        // Danh sách sản phẩm trong đơn
        public List<ProductDetail> Products { get; set; }

        public class ProductDetail
        {
            public string ProductImage { get; set; }
            public string ProductName { get; set; }
            public double? Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }

}
