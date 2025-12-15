using Microsoft.AspNetCore.Mvc;

namespace Lab07.Controllers;

public class CoursesController : Controller
{
    public IActionResult Index() => View();
}
