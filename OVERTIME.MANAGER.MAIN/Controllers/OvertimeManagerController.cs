using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OVERTIME.MANAGER.MAIN.Models;
using OVERTIME.MANAGER.MAIN.ViewModels;
using X.PagedList;

namespace OVERTIME.MANAGER.MAIN.Controllers
{
    public class OvertimeManagerController : Controller
    {
        OvertimeManagerContext db = new OvertimeManagerContext();

        /// <summary>
        /// Hàm filter, sort and paging
        /// </summary>
        /// <param name="pageIndex">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trên trang</param>
        /// <param name="searchQuery">Giá trị tìm kiếm</param>
        /// <param name="status">Trạng thái đơn làm thêm</param>
        /// <returns>Danh sách đơn làm thêm thỏa mãn điều kiện lọc</returns>
        /// @author nnhiep 20.03.2023
        public IActionResult Index(string searchQuery = "", int pageIndex = 1, int pageSize = 15, Byte status = 3)
        {
            var lstOvertime = db.Overtimes.AsNoTracking().Where(x => (x.EmployeeName.ToLower().Contains(searchQuery.ToLower()) || x.EmployeeCode.ToLower().Contains(searchQuery.ToLower()) || x.JobPositionName.ToLower().Contains(searchQuery.ToLower()) || x.OrganizationName.ToLower().Contains(searchQuery.ToLower())) && (x.StatusOvertime == status)).OrderByDescending(x => x.ModifiedDate);

            if (status == 3)
            {
                lstOvertime = db.Overtimes.AsNoTracking().Where(x => x.EmployeeName.ToLower().Contains(searchQuery.ToLower()) || x.EmployeeCode.ToLower().Contains(searchQuery.ToLower()) || x.JobPositionName.ToLower().Contains(searchQuery.ToLower()) || x.OrganizationName.ToLower().Contains(searchQuery.ToLower())).OrderByDescending(x => x.ModifiedDate);
            }

            ListOvertime lst = new ListOvertime()
            {
                list = new PagedList<Overtime>(lstOvertime, pageIndex, pageSize),
                searchQuery = searchQuery,
                pageIndex = pageIndex,
                pageSize = pageSize,
                totalRecord = lstOvertime.Count(),
            };
            ViewBag.PageSizes = new List<SelectListItem>
                {
                    new SelectListItem { Text = "15", Value = "15" },
                    new SelectListItem { Text = "25", Value = "25" },
                    new SelectListItem { Text = "50", Value = "50" },
                    new SelectListItem { Text = "100", Value = "100" },
                };

            ViewBag.Statuses = new List<SelectListItem>
                {
                    new SelectListItem { Text = "All", Value = "3" },
                    new SelectListItem { Text = "Pending", Value = "0" },
                    new SelectListItem { Text = "Approved", Value = "1" },
                    new SelectListItem { Text = "Refused", Value = "2" },
                };

            return View(lst);
        }

        /// <summary>
        /// Chi tiết đơn làm thêm
        /// </summary>
        /// <param name="overtimeId">ID đơn làm thêm</param>
        /// @author nnhiep
        public IActionResult OvertimeDetail(string overtimeId)
        {
            OvertimeDetails overtimeDetail = new OvertimeDetails
            {
                overtime = db.Overtimes.AsNoTracking().FirstOrDefault(x => x.OverTimeId == overtimeId),
                isEdit = false
            };

            return View(overtimeDetail);
        }

        /// <summary>
        /// Thêm đơn làm thêm mới
        /// </summary>
        /// @author nnhiep
        public IActionResult OvertimeAdd()
        {
            ViewBag.Employees = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Nguyễn Ngọc Hiệp", Value = "Nguyễn Ngọc Hiệp" },
                    new SelectListItem { Text = "Ứng Phương Thảo", Value = "Ứng Phương Thảo" },
                    new SelectListItem { Text = "Nguyễn Ngọc Sơn", Value = "Nguyễn Ngọc Sơn" },
                    new SelectListItem { Text = "Nguyễn Thị Hoa", Value = "Nguyễn Thị Hoa" },
                    new SelectListItem { Text = "Nguyễn Ngọc Điệp", Value = "Nguyễn Ngọc Điệp" },
                };
            ViewBag.WorkingShifts = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Hành chính", Value = "Hành chính" },
                    new SelectListItem { Text = "Cuối tuần", Value = "Cuối tuần" },
                    new SelectListItem { Text = "Ngày lễ", Value = "Ngày lễ" },
                    new SelectListItem { Text = "Ngoài giờ", Value = "Ngoài giờ" },
                };
            ViewBag.OWs = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Trước ca", Value = "Trước ca" },
                    new SelectListItem { Text = "Sau ca", Value = "Sau ca" },
                    new SelectListItem { Text = "Giữa ca", Value = "Giữa ca" },
                    new SelectListItem { Text = "Cuối tuần", Value = "Cuối tuần" },
                };
            ViewBag.Approvels = new List<SelectListItem> 
                {
                    new SelectListItem { Text = "Nguyễn Ngọc Hiệp", Value = "Nguyễn Ngọc Hiệp" },
                    new SelectListItem { Text = "Ứng Phương Thảo", Value = "Ứng Phương Thảo" },
                    new SelectListItem { Text = "Nguyễn Ngọc Sơn", Value = "Nguyễn Ngọc Sơn" },
                    new SelectListItem { Text = "Nguyễn Thị Hoa", Value = "Nguyễn Thị Hoa" },
                    new SelectListItem { Text = "Nguyễn Ngọc Điệp", Value = "Nguyễn Ngọc Điệp" },
                };
            ViewBag.StatusOvertimes = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Chờ duyệt", Value = "0" },
                    new SelectListItem { Text = "Đã duyệt", Value = "1" },
                    new SelectListItem { Text = "Từ chối", Value = "2" },
                };    
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
