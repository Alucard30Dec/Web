using Microsoft.AspNetCore.Mvc;

namespace Lab07.Controllers;

public class InstructorsController : Controller
{
    public IActionResult Index() => View();
}
