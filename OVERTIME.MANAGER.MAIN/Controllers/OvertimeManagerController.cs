using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OVERTIME.MANAGER.MAIN.Models;
using OVERTIME.MANAGER.MAIN.Utils.Enums;
using OVERTIME.MANAGER.MAIN.ViewModels;
using X.PagedList;

namespace OVERTIME.MANAGER.MAIN.Controllers
{
    public class OvertimeManagerController : Controller
    {
        #nullable disable
        private readonly OvertimeManagerContext db = new OvertimeManagerContext();

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
                status = status,
                totalRecord = lstOvertime.Count(),
            };

            ViewBag.PageSizes = GetSelectListItems(SelectedItem.pageSize);
            ViewBag.Statuses = GetSelectListItems(SelectedItem.status);

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
        public IActionResult OvertimeAdd(string searchQuery = "", int pageIndex = 1, int pageSize = 15)
        {
            ViewBag.Employees = GetSelectListItems(SelectedItem.employee);
            ViewBag.WorkingShifts = GetSelectListItems(SelectedItem.workingshift);
            ViewBag.OWs = GetSelectListItems(SelectedItem.overtime_workingshift);
            ViewBag.Approvels = GetSelectListItems(SelectedItem.approvel);
            ViewBag.StatusOvertimes = GetSelectListItems(SelectedItem.status_overtime);

            var lstEmployee = db.Employees.AsNoTracking().Where(x => (x.EmployeeName.ToLower().Contains(searchQuery.ToLower()) || x.EmployeeCode.ToLower().Contains(searchQuery.ToLower()) || x.JobPositionName.ToLower().Contains(searchQuery.ToLower()) || x.OrganizationName.ToLower().Contains(searchQuery.ToLower()))).OrderByDescending(x => x.ModifiedDate);

            OvertimeAdd lst = new OvertimeAdd()
            {
                list = new PagedList<Employee>(lstEmployee, pageIndex, pageSize),
                searchQuery = searchQuery,
                pageIndex = pageIndex,
                pageSize = pageSize,
                totalRecord = lstEmployee.Count(),
            };

            ViewBag.PageSizes = GetSelectListItems(SelectedItem.pageSize);
            ViewBag.Statuses = GetSelectListItems(SelectedItem.status);

            return View(lst);
        }

        [AutoValidateAntiforgeryToken]
        public IActionResult AddOvertime(Overtime ot)
        {
            try
            {
                db.Add(ot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ViewBag.Title = "Adding Failed";
                ViewBag.Description = "Add";
                return RedirectToAction("OvertimeAdd");
            }
        }

        /// <summary>
        /// Hàm lấy giá trị dropdown
        /// </summary>
        /// <param name="item">Dropdown cần lấy</param>
        /// <returns>Danh sách các giá trị của dropdown</returns>
        /// @author nnhiep
        public List<SelectListItem> GetSelectListItems(SelectedItem item)
        {
            List<SelectListItem> items;
            switch (item)
            {
                case (SelectedItem.pageSize):
                    items = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "15", Value = "15" },
                        new SelectListItem { Text = "25", Value = "25" },
                        new SelectListItem { Text = "50", Value = "50" },
                        new SelectListItem { Text = "100", Value = "100" },
                    };
                    break;
                case (SelectedItem.status):
                    items = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "All", Value = "3" },
                        new SelectListItem { Text = "Pending", Value = "0" },
                        new SelectListItem { Text = "Approved", Value = "1" },
                        new SelectListItem { Text = "Refused", Value = "2" },
                    };
                    break;
                case (SelectedItem.employee):
                    items = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Nguyễn Ngọc Hiệp", Value = "Nguyễn Ngọc Hiệp" },
                        new SelectListItem { Text = "Ứng Phương Thảo", Value = "Ứng Phương Thảo" },
                        new SelectListItem { Text = "Nguyễn Ngọc Sơn", Value = "Nguyễn Ngọc Sơn" },
                        new SelectListItem { Text = "Nguyễn Thị Hoa", Value = "Nguyễn Thị Hoa" },
                        new SelectListItem { Text = "Nguyễn Ngọc Điệp", Value = "Nguyễn Ngọc Điệp" },
                    };
                    break;
                case (SelectedItem.workingshift):
                    items = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Hành chính", Value = "Hành chính" },
                        new SelectListItem { Text = "Cuối tuần", Value = "Cuối tuần" },
                        new SelectListItem { Text = "Ngày lễ", Value = "Ngày lễ" },
                        new SelectListItem { Text = "Ngoài giờ", Value = "Ngoài giờ" },
                    };
                    break;
                case (SelectedItem.overtime_workingshift):
                    items = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Trước ca", Value = "Trước ca" },
                        new SelectListItem { Text = "Sau ca", Value = "Sau ca" },
                        new SelectListItem { Text = "Giữa ca", Value = "Giữa ca" },
                        new SelectListItem { Text = "Cuối tuần", Value = "Cuối tuần" },
                    };
                    break;
                case (SelectedItem.approvel):
                    items = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Nguyễn Ngọc Hiệp", Value = "Nguyễn Ngọc Hiệp" },
                        new SelectListItem { Text = "Ứng Phương Thảo", Value = "Ứng Phương Thảo" },
                        new SelectListItem { Text = "Nguyễn Ngọc Sơn", Value = "Nguyễn Ngọc Sơn" },
                        new SelectListItem { Text = "Nguyễn Thị Hoa", Value = "Nguyễn Thị Hoa" },
                        new SelectListItem { Text = "Nguyễn Ngọc Điệp", Value = "Nguyễn Ngọc Điệp" },
                    };
                    break;
                case (SelectedItem.status_overtime):
                    items = new List<SelectListItem>
                    {
                        new SelectListItem { Text = "Chờ duyệt", Value = "0" },
                        new SelectListItem { Text = "Đã duyệt", Value = "1" },
                        new SelectListItem { Text = "Từ chối", Value = "2" },
                    };
                    break;
                default:
                    items = new List<SelectListItem>();
                    break;
            }
            return items;
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
