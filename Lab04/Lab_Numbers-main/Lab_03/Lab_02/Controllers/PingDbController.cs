using Lab_02.Data;
using Microsoft.AspNetCore.Mvc;

namespace Lab_02.Controllers
{
    [Route("ping-db")]
    public class PingDbController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PingDbController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Ping()
        {
            bool ok = await _context.Database.CanConnectAsync();
            return Content(ok ? "OK" : "FAIL");
        }
    }
}
