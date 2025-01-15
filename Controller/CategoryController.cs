using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dot_Net_ECommerceWeb.Controller
{
    public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;

        public CategoryController(CategoryService categoryService, ProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        // Action để hiển thị danh mục
        public async Task<IActionResult> Category(int page = 1)
        {
            const int pageSize = 10;  // Số sản phẩm mỗi trang
            try
            {
                // Lấy tất cả sản phẩm
                var products = await _productService.GetProduct();

                if (products == null || !products.Any())
                {
                    products = new List<Product>();  // Đảm bảo có danh sách sản phẩm hợp lệ
                }

                // Tính toán số trang tổng cộng
                var totalProducts = products.Count();
                var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

                // Lấy danh sách sản phẩm cho trang hiện tại
                var productsForPage = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                // Lấy danh mục
                var categories = await _categoryService.GetAllCategories();

                // Truyền dữ liệu qua ViewBag
                ViewBag.Products = productsForPage;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.Categories = categories ?? new List<Category>(); // Đảm bảo danh mục không null
                //ViewBag.ProdcuctImage = productsForPage.

                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new { message = "Lỗi xảy ra khi lấy danh sách danh mục.", error = ex.Message });
            }
        }


        public async Task<IActionResult> CategoryDetails(int categoryId, int page = 1)
        {
            const int pageSize = 10; // Số sản phẩm mỗi trang
            try
            {
                // Lấy tất cả sản phẩm thuộc danh mục
                var products = await _productService.GetProductsByCategoryId(categoryId);

                if (products == null || !products.Any())
                {
                    products = new List<Product>(); // Đảm bảo danh sách sản phẩm không null
                }

                // Tính toán số trang tổng cộng
                var totalProducts = products.Count();
                var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

                // Đảm bảo giá trị page không vượt quá giới hạn
                if (page < 1) page = 1;
                if (page > totalPages) page = totalPages;

                // Lấy danh sách sản phẩm cho trang hiện tại
                var productsForPage = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                // Lấy thông tin danh mục hiện tại
                var category = await _categoryService.GetCategoryByIdAsync(categoryId);

                if (category == null)
                {
                    ViewBag.ErrorMessage = "Danh mục không tồn tại.";
                    return View("Error");
                }

                // Truyền dữ liệu qua ViewBag
                ViewBag.Products = productsForPage; // Danh sách sản phẩm cho trang hiện tại
                ViewBag.CurrentPage = page;        // Trang hiện tại
                ViewBag.TotalPages = totalPages;   // Tổng số trang
                ViewBag.Category = category;       // Thông tin danh mục hiện tại
                ViewBag.CategoryName = category.CategoryName;

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Lỗi xảy ra khi lấy sản phẩm theo danh mục.";
                ViewBag.ErrorDetail = ex.Message;
                return View("Error");
            }
        }







    }
}
