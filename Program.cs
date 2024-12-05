using CloudinaryDotNet;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service;
using Dot_Net_ECommerceWeb.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(x =>
    new PaypalClient(
        builder.Configuration["PayPalOptions:ClientId"],
        builder.Configuration["PayPalOptions:ClientSecret"],
        builder.Configuration["PayPalOptions:Mode"]
    )
);
// Cấu hình dịch vụ đăng nhập Google OAuth
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = builder.Configuration.GetSection("GoogleKeys:ClientId").Value;
        options.ClientSecret = builder.Configuration.GetSection("GoogleKeys:ClientSecret").Value;
    });
// Đăng ký dịch vụ MVC và Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Đăng ký dịch vụ kết nối DB (MySQL)
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32))));

// Đăng ký các dịch vụ ứng dụng
builder.Services.AddScoped<TestUser>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<LogService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<InventoryService>();
builder.Services.AddScoped<SummaryService>();

// Cấu hình Cloudinary
var cloudinaryConfig = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
if (cloudinaryConfig == null || string.IsNullOrEmpty(cloudinaryConfig.CloudName) ||
    string.IsNullOrEmpty(cloudinaryConfig.ApiKey) || string.IsNullOrEmpty(cloudinaryConfig.ApiSecret))
{
    throw new Exception("CloudinarySettings configuration is missing or invalid.");
}

var cloudinaryAccount = new Account(
    cloudinaryConfig.CloudName,
    cloudinaryConfig.ApiKey,
    cloudinaryConfig.ApiSecret
);

var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);

// Cấu hình các middleware cần thiết
var app = builder.Build();

// Thiết lập middleware
app.UseExceptionHandler("/Error");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();

// Cấu hình endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); // Nếu bạn sử dụng Razor Pages

// Khởi động ứng dụng với dịch vụ DBContext
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDBContext>();
    // Thực hiện các công việc khởi tạo dữ liệu ở đây nếu cần
}

app.MapGet("/", () => "Hello World!");

app.Run();
