using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot_Net_ECommerceWeb.Controller;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly AppDBContext _dbContext;

    public ImageController(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("getimage")]
    public async Task<ActionResult<ProductImage>> GetImage()
    {
        var image = await _dbContext.ProductImages.ToListAsync();
        return Ok(image);
    }
}