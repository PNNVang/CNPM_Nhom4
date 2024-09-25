using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Utils;
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

    //Get: api/image/getimage
    [HttpGet("getimage")]
    public async Task<ActionResult<ProductImage>> GetImage()
    {
        var image = await _dbContext.ProductImages.ToListAsync();
        return Ok(image);
    }

    //Post: api/image/upload
    [HttpPost("upload")]
    public async Task<ActionResult> InsertImage(List<IFormFile> files)
    {
        var cloudinaryUtils = new Constants();
        var uploadResults = new List<ImageUploadResult>();

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream)
                };
                await cloudinaryUtils._cloudinary.UploadAsync(uploadParams);
            }


            return Ok();
        }

        return BadRequest();
    }
}