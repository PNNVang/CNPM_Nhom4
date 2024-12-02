using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dot_Net_ECommerceWeb.Controller;

[ApiController]
[Route("/api/[controller]")]
public class ImageController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly AppDBContext _dbContext;

    public ImageController(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    //api lay thong tin anh
    [HttpGet("getimage")]
    public async Task<ActionResult<ProductImage>> GetImage()
    {
        var image = (from p in _dbContext.Products
            join imgproduct in _dbContext.ProductImages on p.Id equals imgproduct.Id select new
            {
                imgproduct.Id,
                p.ProductName,
                imgproduct.ImgMain,
                imgproduct.Img1,
                imgproduct.Img2,
                imgproduct.Img3,
                imgproduct.Img4,
            });
        return Ok(image);
    }

  
}