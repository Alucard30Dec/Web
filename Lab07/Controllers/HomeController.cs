using System.Diagnostics;
using Lab07.Data;
using Lab07.Models;
using Lab07.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab07.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SchoolContext _context;

    public HomeController(ILogger<HomeController> logger, SchoolContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index() => View();

    public async Task<IActionResult> About()
    {
        IQueryable<EnrollmentDateGroup> data =
            from student in _context.Students
            group student by student.EnrollmentDate into dateGroup
            select new EnrollmentDateGroup
            {
                EnrollmentDate = dateGroup.Key,
                StudentCount = dateGroup.Count()
            };

        return View(await data.AsNoTracking().ToListAsync());
    }

    public IActionResult Privacy() => View();

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
