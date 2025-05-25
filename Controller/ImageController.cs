using CloudinaryDotNet; 
using CloudinaryDotNet.Actions;
using Dot_Net_ECommerceWeb.DBContext; 
using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dot_Net_ECommerceWeb.Controller
{
    // Đánh dấu đây là một API Controller, route là /api/image
    [ApiController]
    [Route("/api/[controller]")]
    public class ImageController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly AppDBContext _dbContext;

        // Constructor nhận AppDBContext để truy cập dữ liệu từ CSDL
        public ImageController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // API để lấy thông tin ảnh sản phẩm
        [HttpGet("getimage")]
        public async Task<ActionResult<ProductImage>> GetImage()
        {
            // Truy vấn kết hợp bảng Products và ProductImages
            var image = (from p in _dbContext.Products
                         join imgproduct in _dbContext.ProductImages on p.Id equals imgproduct.Id
                         select new
                         {
                             imgproduct.Id,
                             p.ProductName,
                             imgproduct.ImgMain,
                             imgproduct.Img1,
                             imgproduct.Img2,
                             imgproduct.Img3,
                             imgproduct.Img4,
                         });

            // Trả kết quả dạng HTTP 200 OK với danh sách ảnh
            return Ok(image);
        }
    }
}