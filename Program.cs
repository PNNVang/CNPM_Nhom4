using CloudinaryDotNet;
using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Model;
using Dot_Net_ECommerceWeb.Service;
using Dot_Net_ECommerceWeb.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(x =>
    new PaypalService(
        builder.Configuration["PayPalOptions:ClientId"],
        builder.Configuration["PayPalOptions:ClientSecret"],
        builder.Configuration["PayPalOptions:Mode"]
    )
);

//Cấu hình session
builder.Services.AddDistributedMemoryCache(); // Cấu hình bộ nhớ để lưu trữ Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn của session
    options.Cookie.HttpOnly = true; // Chỉ có thể truy cập từ máy chủ
    options.Cookie.IsEssential = true; // Đánh dấu cookie là quan trọng đối với ứng dụng
});

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
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn của session
    options.Cookie.HttpOnly = true; // Đảm bảo cookie không thể truy cập qua JavaScript
    options.Cookie.IsEssential = true; // Đánh dấu cookie là cần thiết
});
// Đăng ký dịch vụ kết nối DB (MySQL)

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Cấu hình dịch vụ Session
builder.Services.AddDistributedMemoryCache(); // Đăng ký dịch vụ bộ nhớ cache cho session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session hết hạn (30 phút)
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký các dịch vụ ứng dụng
builder.Services.AddScoped<TestUser>();
builder.Services.AddScoped<CloudinaryService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<LogService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductManagementService>();
builder.Services.AddScoped<InventoryService>();
builder.Services.AddScoped<SummaryService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<EncryptAndDencrypt>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<ForgotPasswordService>();
builder.Services.AddScoped<ChangePasswordService>();
// Cấu hình dịch vụ
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    });

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
builder.Services.AddHttpContextAccessor();

// Cấu hình các middleware cần thiết
var app = builder.Build();

// Thiết lập middleware
app.UseExceptionHandler("/Error");
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseSession();

// Cấu hình endpoints

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "category",
        pattern: "Category/{action=Category}/{id?}",
        defaults: new { controller = "Category" });



// Khởi động ứng dụng với dịch vụ DBContext
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDBContext>();
    // Thực hiện các công việc khởi tạo dữ liệu ở đây nếu cần
}

// app.MapGet("/", () => "Hello World!");
app.MapGet("/", async (HttpContext context) =>
{
    context.Response.Redirect("HomePage");
    return Task.CompletedTask;
});

app.MapControllerRoute(
    name: "ShoppingCart",
    pattern: "cart",
    defaults: new { controller = "ShoppingCart", action = "ShoppingCart", alias = "DefaultAlias" }
    );
app.MapControllerRoute(
    name: "Checkout",
    pattern: "checkout",
    defaults: new { controller = "ShoppingCart", action = "Checkout", alias = "DefaultAlias" }
    );
app.Run();


