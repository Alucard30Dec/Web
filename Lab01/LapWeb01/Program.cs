var builder = WebApplication.CreateBuilder(args);

// Kích hoạt dịch vụ MVC (Controllers và Views)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình môi trường (Giữ nguyên)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// --- ĐOẠN ĐÃ ĐƯỢC CHỈNH SỬA ---
// Cấu hình định tuyến (Routing):
// Khi chạy lên, mặc định sẽ tìm vào HelloWorldController thay vì HomeController
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();