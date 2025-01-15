using System.ComponentModel.DataAnnotations;

namespace Dot_Net_ECommerceWeb.Model
{
    public class CustomerViewModel
    {
            [Required(ErrorMessage = "Tên khách hàng là bắt buộc.")]
            public string CustomerName { get; set; }
            [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
            public string Phone { get; set; }
            [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
            public string Address { get; set; }
            [Required(ErrorMessage = "Email là bắt buộc.")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Hình thức thanh toán là bắt buộc.")]
            public string Payment {  get; set; }

        

    }
}
