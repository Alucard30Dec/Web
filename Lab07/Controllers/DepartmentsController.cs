using Microsoft.AspNetCore.Mvc;

namespace Lab07.Controllers;

public class DepartmentsController : Controller
{
    public IActionResult Index() => View();
}
