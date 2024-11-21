using Dot_Net_ECommerceWeb.DBContext;
using Dot_Net_ECommerceWeb.Service;
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