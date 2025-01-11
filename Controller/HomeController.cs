using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public HomeController(ProductService productService, CategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Category()
        {
            return RedirectToAction("Category", "Category");
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            const int pageSize = 10; // Số sản phẩm mỗi trang
            try
            {
                // Lấy toàn bộ danh sách sản phẩm
                var products = await _productService.GetProductHot();

                if (products == null || !products.Any())
                {
                    products = new List<Product>();
                }

                // Tính toán số trang
                var totalProducts = products.Count();
                var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

                // Đảm bảo giá trị page hợp lệ
                if (page < 1) page = 1;
                if (page > totalPages) page = totalPages;

                // Lấy danh sách sản phẩm cho trang hiện tại
                var productsForPage = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                // Truyền dữ liệu qua ViewBag
                ViewBag.Products = productsForPage; // Danh sách sản phẩm
                ViewBag.CurrentPage = page;        // Trang hiện tại
                ViewBag.TotalPages = totalPages;   // Tổng số trang

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Lỗi xảy ra khi lấy danh sách sản phẩm.";
                ViewBag.ErrorDetail = ex.Message;
                return View("Error");
            }
        }
    }
}
