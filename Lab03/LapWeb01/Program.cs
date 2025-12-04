using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LapWeb01.Data;
using LapWeb01.Models; // Quan trọng: Để nhận diện SeedData

var builder = WebApplication.CreateBuilder(args);

// Đăng ký Database
builder.Services.AddDbContext<LapWeb01Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LapWeb01Context") ?? throw new InvalidOperationException("Connection string 'LapWeb01Context' not found.")));

// Đăng ký MVC Controller
builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- ĐOẠN CODE QUAN TRỌNG ĐỂ TẠO DỮ LIỆU MẪU ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Gọi hàm Initialize bên file SeedData để nạp dữ liệu
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}
// ---------------------------------------------------

// Cấu hình môi trường
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();