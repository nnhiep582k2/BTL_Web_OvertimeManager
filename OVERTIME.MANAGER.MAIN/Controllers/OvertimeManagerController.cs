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

            ListItem<Overtime> lst = new ListItem<Overtime>()
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
        [HttpGet]
        public IActionResult OvertimeAdd()
        {
            ViewBag.Employees = GetSelectListItems(SelectedItem.employee);
            ViewBag.WorkingShifts = GetSelectListItems(SelectedItem.workingshift);
            ViewBag.OWs = GetSelectListItems(SelectedItem.overtime_workingshift);
            ViewBag.Approvals = GetSelectListItems(SelectedItem.approval);
            ViewBag.StatusOvertimes = GetSelectListItems(SelectedItem.status_overtime);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OvertimeAdd(Overtime ot)
        {
            DateTime now = DateTime.Now;
            string employeeID = ot.EmployeeName;
            string workingShiftID = ot.WorkingShiftName;
            string owsID = ot.OverTimeInWorkingShiftName;
            string approvalID = ot.ApprovalName;

            ot.OverTimeId = Guid.NewGuid().ToString();
            ot.ModifiedBy = "nnhiep";
            ot.CreatedBy = "nnhiep";
            ot.ModifiedDate = now;
            ot.CreatedDate = now;
            ot.EmployeeId = employeeID;
            ot.EmployeeCode = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == employeeID).EmployeeCode;
            ot.EmployeeName = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == employeeID).EmployeeName;
            ot.OrganizationName = db.Organizations.AsNoTracking().FirstOrDefault(x => x.OrganizationId == ot.OrganizationId).OrganizationName;
            ot.OverTimeInWorkingShiftId = owsID;
            ot.OverTimeInWorkingShiftCode = db.OverTimeInWorkingShifts.AsNoTracking().FirstOrDefault(x => x.OverTimeInWorkingShiftId == owsID).OverTimeInWorkingShiftCode;
            ot.OverTimeInWorkingShiftName = db.OverTimeInWorkingShifts.AsNoTracking().FirstOrDefault(x => x.OverTimeInWorkingShiftId == owsID).OverTimeInWorkingShiftName;
            ot.WorkingShiftId = workingShiftID;
            ot.WorkingShiftCode = db.WorkingShifts.AsNoTracking().FirstOrDefault(x => x.WorkingShiftId == workingShiftID).WorkingShiftCode;
            ot.WorkingShiftName = db.WorkingShifts.AsNoTracking().FirstOrDefault(x => x.WorkingShiftId == workingShiftID).WorkingShiftName;
            ot.ApprovalId = approvalID;
            ot.ApprovalName = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == approvalID).EmployeeName;
            ot.Dsc = ot.Dsc ?? "";
            ot.OverTimeEmployeeIds = ot.OverTimeEmployeeIds ?? "";
            ot.OverTimeEmployeeCodes = ot.OverTimeEmployeeCodes ?? "";
            ot.OverTimeEmployeeNames = ot.OverTimeEmployeeNames ?? "";

            if (ModelState.IsValid)
            {
                db.Overtimes.Add(ot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Employees = GetSelectListItems(SelectedItem.employee);
            ViewBag.WorkingShifts = GetSelectListItems(SelectedItem.workingshift);
            ViewBag.OWs = GetSelectListItems(SelectedItem.overtime_workingshift);
            ViewBag.Approvals = GetSelectListItems(SelectedItem.approval);
            ViewBag.StatusOvertimes = GetSelectListItems(SelectedItem.status_overtime);

            return View(ot);
        }

        /// <summary>
        /// Hàm lấy giá trị dropdown
        /// </summary>
        /// <param name="item">Dropdown cần lấy</param>
        /// <returns>Danh sách các giá trị của dropdown</returns>
        /// @author nnhiep
        public SelectList GetSelectListItems(SelectedItem item)
        {
            SelectList items;
            List<ListTemplate> temp = null;
            switch (item)
            {
                case (SelectedItem.pageSize):
                    temp = new List<ListTemplate>
                    {
                        new ListTemplate { Text = "15", Value = "15" },
                        new ListTemplate { Text = "25", Value = "25" },
                        new ListTemplate { Text = "50", Value = "50" },
                        new ListTemplate { Text = "100", Value = "100" },
                    };
                    items = new SelectList(temp, "Value", "Text");
                    break;
                case (SelectedItem.status):
                    temp = new List<ListTemplate>
                    {
                        new ListTemplate { Text = "All", Value = "3" },
                        new ListTemplate { Text = "Pending", Value = "0" },
                        new ListTemplate { Text = "Approved", Value = "1" },
                        new ListTemplate { Text = "Refused", Value = "2" },
                    };
                    items = new SelectList(temp, "Value", "Text");
                    break;
                case (SelectedItem.employee):
                case (SelectedItem.approval):
                    items = new SelectList(db.Employees.ToList(), "EmployeeId", "EmployeeName");
                    break;
                case (SelectedItem.workingshift):
                    items = new SelectList(db.WorkingShifts.ToList(), "WorkingShiftId", "WorkingShiftName");
                    break;
                case (SelectedItem.overtime_workingshift):
                    items = new SelectList(db.OverTimeInWorkingShifts.ToList(), "OverTimeInWorkingShiftId", "OverTimeInWorkingShiftName");
                    break;
                case (SelectedItem.status_overtime):
                    temp = new List<ListTemplate>
                    {
                        new ListTemplate { Text = "Chờ duyệt", Value = "0" },
                        new ListTemplate { Text = "Đã duyệt", Value = "1" },
                        new ListTemplate { Text = "Từ chối", Value = "2" },
                    };
                    items = new SelectList(temp, "Value", "Text");
                    break;
                default:
                    items = null;
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
