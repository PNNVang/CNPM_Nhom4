using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.EntityFrameworkCore;

public class ProductManagementService
{
    private AppDBContext _context { get; set; }
    private readonly Cloudinary _cloudinary;

    public ProductManagementService(AppDBContext context, Cloudinary cloudinary)
    {
        _context = context;
        _cloudinary = cloudinary;
    }
    public async Task<IEnumerable<Product>> GetProductAsync()
    {
        return await _context.Products
            .Where(p => p.StatusDeleted == "chưa xóa")
            .Select(p => new Product
            {
                Id = p.Id,
                CategoryId = p.CategoryId,
                ProductName = p.ProductName,
                Price = p.Price,
                Status = p.Status,
            })
           .ToListAsync();
    }
    public async Task<bool> AddProductAsync(ProductForm model)
    {
        //6.1.1: Khởi tạo id tự động tăng 
        var newProductId = _context.Products.Any() ? _context.Products.Max(x => x.Id) + 1 : 1;
        var newProductImageId = _context.ProductImages.Any() ? _context.ProductImages.Max(x => x.Id) + 1 : 1;

        //6.1.2: Tạo đối tượng sản phẩm 
        var product = new Product
        {
            Id = newProductId,
            CategoryId = model.categoryID,
            StatusDeleted = "chưa xóa",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Sale = 0,
            ProductName = model.productName,
            Price = (float)model.price,
            Status = model.Status,
            Description = model.Description,
        };

        //6.1.3: Lưu sản phẩm vào bảng Product trong database
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        //6.1.4: Upload các ảnh sản phẩm lên Cloudinary và nhận về URL
        var imageUrls = await UploadImagesToCloudinary(model);
        var productImage = new ProductImage
        {
            Id = newProductImageId,
            ImgMain = imageUrls[0],
            Img1 = imageUrls[1],
            Img2 = imageUrls[2],
            Img3 = imageUrls[3],
            Img4 = imageUrls[4]
        };

        //6.1.5: Lưu thông tin ảnh sản phẩm vào database
        _context.ProductImages.Add(productImage);
        await _context.SaveChangesAsync();

        return true;
    }
    private async Task<string[]> UploadImagesToCloudinary(ProductForm model)
    {
        var images = new[] { model.MainImage, model.Image1, model.Image2, model.Image3, model.Image4 };
        var urls = new string[images.Length];

        for (int i = 0; i < images.Length; i++)
        {
            if (images[i] != null)
            {
                var uploadResult = await UploadImageToCloudinary(images[i]);
                if (uploadResult != null)
                {
                    urls[i] = uploadResult.Url.ToString();
                }
            }
        }

        return urls;
    }
    private async Task<ImageUploadResult> UploadImageToCloudinary(IFormFile image)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(image.FileName, image.OpenReadStream())
        };

        return await _cloudinary.UploadAsync(uploadParams);
    }
}


