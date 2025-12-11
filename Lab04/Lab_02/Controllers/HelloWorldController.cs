using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class HelloWorldController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        //GET: /HelloWorld/

        //GET: /HelloWorld/Welcome/
        // Requires using System.Text.Encodings.Web;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome(string name, int numTimes = 1)
        {

            ViewData["Message"] = "Hello " + name;

            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}
