using Microsoft.AspNetCore.Mvc;
using OVERTIME.MANAGER.MAIN.Models;

namespace OVERTIME.MANAGER.MAIN.Controllers
{
    public class HomeController : Controller
    {
        OvertimeManagerContext db = new OvertimeManagerContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}