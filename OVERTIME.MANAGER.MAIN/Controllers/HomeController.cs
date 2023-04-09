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

        [Authentication]
        public IActionResult Index()
        {
            return View();
        }

        [Authentication]
        // Điều khoản và dịch vụ
        public IActionResult TermsOfService()
		{
			return View();
        }

        [Authentication]
        // Chính sách bảo mật
        public IActionResult PrivacyPolicy()
        {
            return View();
        }

        /// <summary>
        /// Không có quyền
        /// </summary>
        /// author nnhiep
        public IActionResult DontHavePermission()
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
