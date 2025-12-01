using Microsoft.AspNetCore.Mvc;

namespace LapWeb01.Controllers
{
    public class HomeController : Controller
    {
        // Hàm này để chạy trang chủ (nếu sau này cần)
        public IActionResult Index()
        {
            return View();
        }

        // Hàm này để chạy trang Privacy
        public IActionResult Privacy()
        {
            return View();
        }
    }
}