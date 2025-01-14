using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Extensions;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;

            }
            return View();

        }
        public IActionResult Checkout()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }

        public IActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);

            }   
            return PartialView();
        }
        public IActionResult Partial_Item_ThanhToan()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null && cart.Items.Any())
            {
                return View(cart.Items);

            }
            return View();
        }


        public IActionResult ShowCount()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                return Json(new { Count = cart.Items.Count });
            }
            return Json(new { Count = 0 });
        }
        [HttpPost]
        public IActionResult CheckOut(CustomerViewModel order)
        {
            if (ModelState.IsValid)
            {
                ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
                if (cart != null)
                {
                }
            }
            return View(order);
        }
        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var code = new { success = false, massage = "", code = -1, Count = 0 };
            
            var checkProduct = _context.Products.Include(p => p.ProductImage).FirstOrDefault(x => x.Id == id);
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

                if (checkProduct.ProductImage != null && checkProduct.ProductImage.ImgMain!=null)
                {
                    item.ProductImg = checkProduct.ProductImage.ImgMain;
                }
                item.Price = checkProduct.Price;
                //if (checkProduct.Sale > 0)
                //{
                //    item.Price = checkProduct.Price * (1 - checkProduct.Sale / 100f);
                //}
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
                Console.WriteLine("Thêm Thành công");
                code = new { success = true, massage = "Thêm sản phẩm vào giỏ hàng thành công", code = 1, Count = cart.Items.Count };
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
                    HttpContext.Session.SetObjectAsJson("Cart", cart);
                    code = new { Success = true, massage = "", code = 1, Count = cart.Items.Count };
                }
            }
            return Json(code);
        }
        [HttpPost]
        public IActionResult Update(int id,int quantity)
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                cart.UpdateQuantity(id,quantity);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        [HttpPost]
        public IActionResult DeleteAll()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                cart.ClearCart();
                HttpContext.Session.SetObjectAsJson("Cart", cart);
                return Json(new {Success = true});
            }
            return Json(new { Success = false });
        }
    }
}