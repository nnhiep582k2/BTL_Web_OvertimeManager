using Microsoft.AspNetCore.Mvc;

namespace OVERTIME.MANAGER.MAIN.Controllers
{
    public class PortfolioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
