using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Extensions;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dot_Net_ECommerceWeb.Controller
{
    public class ShoppingCartController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly AppDBContext _context;

        // Constructor sử dụng dependency injection
        public ShoppingCartController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult ShoppingCart()
        {
           
            return View();
        }
        public IActionResult Checkout()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                ViewBag.CheckCart = cart;


            }
            return View();
        }
        public IActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                return PartialView(cart.Items);

            }   
            return PartialView();
        }
        public IActionResult Partial_Item_Checkout()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                return PartialView(cart.Items);

            }
            return PartialView();
        }

        public IActionResult ShowCount()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                return new JsonResult(new { Count = cart.Items.Count });
            }
            return new JsonResult(new { Count = 0 });
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, massage = "", code = -1, Count = 0 };
            //cái class context có trong class này rồi tài võ chỉ việc gọi ra thôi
            // var db = new AppDBContext();
            var checkProduct = _context.Products.FirstOrDefault(x => x.Id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");

                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.ProductName,
                    Quantity = quantity
                };

                if (checkProduct.ProductImage != null && checkProduct.ProductImage.ImgMain != null)
                {
                    item.ProductImg = checkProduct.ProductImage.ImgMain;
                }
                item.Price = checkProduct.Price;
                if (checkProduct.Sale > 0)
                {
                    item.Price = checkProduct.Price * (1 - checkProduct.Sale / 100f);
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
                code = new { Success = true, massage = "Thêm sản phẩm vào giỏ hàng thành công", code = 1, Count = cart.Items.Count };
            }
            return Json(code);
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var code = new { Success = false, massage = "", code = -1, Count = 0 };
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");

            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, massage = "", code = 1, Count = cart.Items.Count };
                }
            }
            return Json(code);
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new {Success = true});
            }
            return Json(new { Success = false });
        }
    }
}