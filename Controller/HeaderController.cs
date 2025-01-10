using Dot_Net_ECommerceWeb.DTO;
using Dot_Net_ECommerceWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller
{
    public class HeaderController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly CategoryService _categoryService;

        public HeaderController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Header()
        {

            return View();
        }
    }
}
