using Microsoft.AspNetCore.Mvc;

namespace Lab02.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            return Content("This is my default action...");
        }

        // GET: /HelloWorld/Welcome/
        public IActionResult Welcome()
        {
            return Content("This is the Welcome action method...");
        }
    }
}
