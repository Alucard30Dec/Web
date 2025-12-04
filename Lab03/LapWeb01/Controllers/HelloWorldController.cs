using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace LapWeb01.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /HelloWorld/Welcome/ 
        // ĐÃ SỬA: Thay đổi kiểu trả về thành IActionResult và dùng ViewData
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            // Lưu thông điệp vào túi chứa ViewData
            ViewData["Message"] = "Hello " + name;

            // Lưu số lần lặp
            ViewData["NumTimes"] = numTimes;

            // Trả về View (nó sẽ tìm file Welcome.cshtml để hiển thị)
            return View();
        }
    }
}