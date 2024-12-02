using CloudinaryDotNet;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Service;
using Dot_Net_ECommerceWeb.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();
// Đăng ký dịch vụ kết nối DB trước khi gọi builder.Build()
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)))); 
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<LogService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<InventoryService>();
builder.Services.AddScoped<SummaryService>();
// Đọc thông tin cấu hình từ appsettings.json
var cloudinaryConfig = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
if (cloudinaryConfig == null)
{
    throw new Exception("CloudinarySettings configuration is missing or invalid.");
}

if (string.IsNullOrEmpty(cloudinaryConfig.CloudName) || 
    string.IsNullOrEmpty(cloudinaryConfig.ApiKey) || 
    string.IsNullOrEmpty(cloudinaryConfig.ApiSecret))
{
    throw new Exception("One or more required Cloudinary settings are missing.");
}

var cloudinaryAccount = new Account(
    cloudinaryConfig.CloudName,
    cloudinaryConfig.ApiKey,
    cloudinaryConfig.ApiSecret
);

var cloudinary = new Cloudinary(cloudinaryAccount);

// Thêm Cloudinary vào DI container
builder.Services.AddSingleton(cloudinary);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Khởi động các dịch vụ ở đây
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDBContext>();
    // Thực hiện các công việc khởi động dữ liệu ở đây nếu cần
}
// Cấu hình middleware
app.UseRouting();
app.MapControllers();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();