using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Extensions;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using Dot_Net_ECommerceWeb.Service;

namespace Dot_Net_ECommerceWeb.Controller
{
    public class ShoppingCartController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDBContext _context;
        private readonly OrderService _orderService;

        // Constructor sử dụng dependency injection
        public ShoppingCartController(AppDBContext context, IHttpContextAccessor httpContextAccessor, OrderService orderService)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            this._orderService = orderService;
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
        public ActionResult CheckOut()
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult OrderSuccess()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CustomerViewModel req)
        {
            var code = new { Success = false, Code = -1 };

            var userJson = _httpContextAccessor.HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userJson))
            {
                // Nếu không tìm thấy thông tin người dùng trong session, bạn có thể redirect

                return RedirectToAction("Login", "Account");
            }
            var user = JsonConvert.DeserializeObject<User>(userJson);

            if (!ModelState.IsValid)
            {
                return View(req);
            }
            //if (ModelState.IsValid)
            //{
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null && cart.Items.Any())
            {
                Order order = new Order
                {
                    CreatedAt = DateTime.Now,
                    TotalPrice = cart.GetTotalQuantity(),
                    Status = "chờ xác nhận",
                    TypePayment = req.Payment,
                    UserId = user.Id,
                    Address = req.Address,
                    TypeShip = "giao hàng",
                    ShipCost = "20000",
                    StatusPayment = "chưa thanh toán",
                    Note = "Nhận tại cửa hàng"

                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                foreach (var item in cart.Items)
                {
                    OrderDetail orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        QuantityTotal = item.Quantity,
                        TotalPrice = item.Price * item.Quantity

                    };
                    _context.OrderDetails.Add(orderDetail);
                }

                await _context.SaveChangesAsync();

                cart.ClearCart();
                HttpContext.Session.SetObjectAsJson("Cart", cart);

                if (_orderService.SendMailOrder(user, req, order))
                {
                    return RedirectToAction("OrderSuccess");
                }
            }

            return Json(code);
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

                if (checkProduct.ProductImage != null && checkProduct.ProductImage.ImgMain != null)
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
        public IActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
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
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
    }
}