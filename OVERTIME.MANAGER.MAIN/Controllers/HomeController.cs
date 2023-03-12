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

        public IActionResult Login()
        {
			return View();
		}

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        
        // Điều khoản và dịch vụ
        public IActionResult TermsOfService()
		{
			return View();
        }

        // Chính sách bảo mật
        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        /// <summary>
        /// 404 page
        /// </summary>
        /// <param name="statusCode">Giá trị trạng thái trả về</param>
        /// <returns>View NotFound</returns>
        /// Created by nnhiep 04.03.2023
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if(statusCode == 404)
            {
                return View("NotFound");
            }
            // ! Xử lý tạm
            return View("NotFound");
        }
    }
}
