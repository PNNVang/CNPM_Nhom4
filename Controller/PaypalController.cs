using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller;

[Route("/api/[controller]")]
[ApiController]
public class PaypalController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly PaypalService _paypalClient;
    private readonly AppDBContext db;

    public PaypalController(AppDBContext context, PaypalService paypalClient)
    {
        _paypalClient = paypalClient;
        db = context;
    }

    #region Paypal payment

    
    [HttpPost("Cart/create-paypal-order")]
    public async Task<IActionResult> CreatePaypalOrder(CancellationToken cancellationToken)
    {
        // Thông tin đơn hàng gửi qua Paypal
        var tongTien = "50000";
        var donViTienTe = "USD";
        var maDonHangThamChieu = "DH" + DateTime.Now.Ticks.ToString();

        try
        {
            var response = await _paypalClient.CreateOrder(tongTien, donViTienTe, maDonHangThamChieu);

            return Ok(response);
        }
        catch (Exception ex)
        {
            var error = new { ex.GetBaseException().Message };
            return BadRequest(error);
        }
    }

  
    [HttpPost("Cart/capture-paypal-order")]
    public async Task<IActionResult> CapturePaypalOrder(string orderID, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _paypalClient.CaptureOrder(orderID);

            // Lưu database đơn hàng của mình

            return Ok(response);
        }
        catch (Exception ex)
        {
            var error = new { ex.GetBaseException().Message };
            return BadRequest(error);
        }
    }

    #endregion
}