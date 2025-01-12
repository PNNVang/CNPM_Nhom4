using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.DTO;
using Dot_Net_ECommerceWeb.Model;
using Microsoft.EntityFrameworkCore;

public class ProductService
{
    private AppDBContext _context { get; set; }
    private readonly Cloudinary _cloudinary;


    public ProductService(AppDBContext context, Cloudinary cloudinary)
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
                Sale = p.Sale,
                Hot = p.Hot
            })
            .ToListAsync();
    }

    public async Task<List<Product>> GetProduct()
    {
        return await _context.Products
            .Include(p => p.ProductImage)
            .Where(p => p.StatusDeleted == "chưa xóa")
            .ToListAsync();
    }

    public async Task<List<Product>> GetProductHot()
    {
        return await _context.Products
       .Include(p => p.ProductImage)
       .Where(p => p.StatusDeleted == "chưa xóa" && p.Hot == 1)
       .ToListAsync();
    }


    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        product.StatusDeleted = "đã xóa";
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddProductAsync(ProductForm model)
    {
        var newProductId = _context.Products.Any() ? _context.Products.Max(x => x.Id) + 1 : 1;
        var newInventoryId = _context.Inventories.Any() ? _context.Inventories.Max(x => x.id) + 1 : 1;
        var newProductImageId = _context.ProductImages.Any() ? _context.ProductImages.Max(x => x.Id) + 1 : 1;
        var newInventoryDetailId = _context.InventoryDetails.Any() ? _context.InventoryDetails.Max(x=>x.Id) + 1 : 1;
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
            Information = GenerateProductInfo(model),
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var inventory = new Inventories
        {
            id = newInventoryId,
            date = DateTime.Now,
            user_imported = "admin",
        };

        _context.Inventories.Add(inventory);
        await _context.SaveChangesAsync();

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

        _context.ProductImages.Add(productImage);
        await _context.SaveChangesAsync();

        return true;
    }

    public string GenerateProductInfo(ProductForm model)
    {
        return
            $"/color:{model.Color};weight:{model.Weight};size:{model.Size};opacity:{model.Opacity};cutting_form:{model.CuttingForm}/";
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

    public async Task<bool> UpdateProduct(ProductFormUpdate model)
    {
        if (model.productName != null)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == model.id);
            if (product != null)
            {
                product.ProductName = model.productName;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        if (model.price != null)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == model.id);
            if (product != null)
            {
                product.Price = (float)model.price;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        if (model.imageMain != null)
        {
            var image = _context.ProductImages.FirstOrDefault(p => p.Id == model.id);
            if (image != null)
            {
                // Upload image to Cloudinary
                var uploadResult = await UploadImageToCloudinary(model.imageMain);
                if (uploadResult != null)
                {
                    // Get the URL of the uploaded image
                    var imageUrl = uploadResult.SecureUrl.ToString();
                    Console.WriteLine(imageUrl);
                    // Update database with the new image URL
                    var productImage = _context.ProductImages.FirstOrDefault(p => p.Id == model.id);
                    if (productImage != null)
                    {
                        productImage.ImgMain = imageUrl;
                        _context.ProductImages.Update(productImage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        if (model.image1 != null)
        {
            var image = _context.ProductImages.FirstOrDefault(p => p.Id == model.id);
            if (image != null)
            {
                // Upload image to Cloudinary
                var uploadResult = await UploadImageToCloudinary(model.image1);
                if (uploadResult != null)
                {
                    // Get the URL of the uploaded image
                    var imageUrl = uploadResult.SecureUrl.ToString();

                    // Update database with the new image URL
                    var productImage = _context.ProductImages.FirstOrDefault(p => p.Id == model.id);
                    if (productImage != null)
                    {
                        productImage.Img1 = imageUrl;
                        _context.ProductImages.Update(productImage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        if (model.image2 != null)
        {
            var image = _context.ProductImages.FirstOrDefault(p => p.Id == model.id);
            if (image != null)
            {
                // Upload image to Cloudinary
                var uploadResult = await UploadImageToCloudinary(model.image2);
                if (uploadResult != null)
                {
                    // Get the URL of the uploaded image
                    var imageUrl = uploadResult.SecureUrl.ToString();

                    // Update database with the new image URL
                    var productImage = _context.ProductImages.FirstOrDefault(p => p.Id == model.id);
                    if (productImage != null)
                    {
                        productImage.Img2 = imageUrl;
                        _context.ProductImages.Update(productImage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        if (model.image3 != null)
        {
            var image = _context.ProductImages.FirstOrDefault(p => p.Id == model.id);
            if (image != null)
            {
                // Upload image to Cloudinary
                var uploadResult = await UploadImageToCloudinary(model.image3);
                if (uploadResult != null)
                {
                    // Get the URL of the uploaded image
                    var imageUrl = uploadResult.SecureUrl.ToString();

                    // Update database with the new image URL
                    var productImage = _context.ProductImages.FirstOrDefault(p => p.Id == model.id);
                    if (productImage != null)
                    {
                        productImage.Img3 = imageUrl;
                        _context.ProductImages.Update(productImage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        if (model.image4 != null)
        {
            var image = _context.ProductImages.FirstOrDefault(p => p.Id == model.id);
            if (image != null)
            {
                // Upload image to Cloudinary
                var uploadResult = await UploadImageToCloudinary(model.image4);
                if (uploadResult != null)
                {
                    // Get the URL of the uploaded image
                    var imageUrl = uploadResult.SecureUrl.ToString();

                    // Update database with the new image URL
                    var productImage = _context.ProductImages.FirstOrDefault(p => p.Id == model.id);
                    if (productImage != null)
                    {
                        productImage.Img4 = imageUrl;
                        _context.ProductImages.Update(productImage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
        }

        if (model.status != null)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == model.id);
            if (product != null)
            {
                product.Status = model.status;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        if (model.sale != null)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == model.id);
            if (product != null)
            {
                product.Sale = (int)model.sale;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                
            }
            
        }

        if (model.hot != null)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == model.id);
            if (product != null)
            {
                //product.Hot = (bool)model.hot;
                product.Hot=model.hot;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }

        if (model.description != null)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == model.id);
            if (product != null)
            {
                product.Description = model.description;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }
        return true;
    }

    public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
    {   
        try
        {
            // Truy vấn sản phẩm thuộc danh mục theo categoryId
            var products = await _context.Products
                .Include(p => p.ProductImage)
                .Where(p => p.CategoryId == categoryId)  // Lọc sản phẩm theo CategoryId
                .ToListAsync(); // Lấy danh sách sản phẩm

            return products;
        }
        catch (Exception ex)
        {
            // Log lỗi hoặc xử lý thông báo lỗi
            throw new InvalidOperationException("Error fetching products for category.", ex);
        }
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products
                         .Include(p => p.ProductImage)  // Bao gồm thông tin ProductImage
                         .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ProductImage> GetProductImageByIdAsync(int id)
    {
        return await _context.ProductImages
                             .FirstOrDefaultAsync(pi => pi.Id == id);
    }

   
}
