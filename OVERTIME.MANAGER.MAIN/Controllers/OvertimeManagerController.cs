using Microsoft.AspNetCore.Mvc;

namespace OVERTIME.MANAGER.MAIN.Controllers
{
    public class OvertimeManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Quản lý ca làm việc
        /// </summary>
        /// <returns>View</returns>
        /// @author nnhiep 15.03.2023
        public IActionResult WorkingShiftManager()
        {
            return View();
        }

        /// <summary>
        /// Quản lý thời điểm làm thêm
        /// </summary>
        /// <returns>View</returns>
        /// @author nnhiep 15.03.2023
        public IActionResult OvertimeWorkingShiftManager()
        {
            return View();
        }

        /// <summary>
        /// Quản lý vị trí công việc
        /// </summary>
        /// <returns>View</returns>
        /// @author nnhiep 15.03.2023
        public IActionResult JobPositionManager()
        {
            return View();
        }

        /// <summary>
        /// Quản lý đơn vị công tác
        /// </summary>
        /// <returns>View</returns>
        /// @author nnhiep 15.03.2023
        public IActionResult OrganizationManager()
        {
            return View();
        }

        /// <summary>
        /// Quản lý nhân viên
        /// </summary>
        /// <returns>View</returns>
        /// @author nnhiep 15.03.2023
        public IActionResult EmployeeManager()
        {
            return View();
        }
    }
}
