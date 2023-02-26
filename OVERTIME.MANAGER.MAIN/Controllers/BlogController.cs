using Microsoft.AspNetCore.Mvc;

namespace OVERTIME.MANAGER.MAIN.Controllers
{
    public class BlogController : Controller
    {
        // Trang chủ - Blog
        public IActionResult Index()
        {
            return View();
        }

        // Trang chi tiết 1 blog
        public IActionResult Details()
        {
            return View();
        }
    }
}
