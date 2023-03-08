using Microsoft.AspNetCore.Mvc;

namespace OVERTIME.MANAGER.MAIN.Controllers
{
    public class OvertimeManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
