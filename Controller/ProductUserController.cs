using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller
{
    public class ProductUserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ProductService _productService;

        public ProductUserController(ProductService productService)
        {
            _productService = productService;
        }

        // Phương thức xử lý chi tiết sản phẩm
        public async Task<IActionResult> ProductDetail(int productId)
        {
            // Lấy thông tin sản phẩm từ service
            var product = await _productService.GetProductByIdAsync(productId);

            if (product == null)
            {
                // Nếu không tìm thấy sản phẩm, trả về lỗi 404
                return NotFound();
            }

            //// Lấy thông tin hình ảnh sản phẩm từ service
            //var productImage = await _productService.GetProductImageByIdAsync(productId);

            // Truyền dữ liệu qua ViewBag
            ViewBag.Product = product;
            ViewBag.ProductImage = product.ProductImage;

            // Trả về view
            return View();
        }


    }
}
