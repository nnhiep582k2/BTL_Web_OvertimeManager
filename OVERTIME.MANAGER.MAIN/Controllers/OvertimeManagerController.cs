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
        private OvertimeManagerContext db = new OvertimeManagerContext();

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
            IEnumerable<Overtime> lstOvertime;

            lstOvertime = db.Overtimes.AsNoTracking().Where(x => (x.EmployeeName.ToLower().Contains(searchQuery.ToLower()) || x.EmployeeCode.ToLower().Contains(searchQuery.ToLower()) || x.JobPositionName.ToLower().Contains(searchQuery.ToLower()) || x.OrganizationName.ToLower().Contains(searchQuery.ToLower())) && (x.StatusOvertime == status)).OrderByDescending(x => x.ModifiedDate);

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
            Overtime overtimeDetail = db.Overtimes.AsNoTracking().FirstOrDefault(x => x.OverTimeId == overtimeId);

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
            ot.OverTimeInWorkingShiftId = owsID;
            ot.WorkingShiftId = workingShiftID;
            ot.ApprovalId = approvalID;
            ot.OverTimeEmployeeIds = ot.OverTimeEmployeeIds ?? "";
            ot.OverTimeEmployeeCodes = ot.OverTimeEmployeeCodes ?? "";
            ot.OverTimeEmployeeNames = ot.OverTimeEmployeeNames ?? "";
            ot.EmployeeCode = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == employeeID).EmployeeCode;
            ot.EmployeeName = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == employeeID).EmployeeName;
            ot.JobPositionId = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == employeeID).JobPositionId;
            ot.JobPositionName = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == employeeID).JobPositionName;
            ot.OverTimeInWorkingShiftCode = db.OverTimeInWorkingShifts.AsNoTracking().FirstOrDefault(x => x.OverTimeInWorkingShiftId == owsID).OverTimeInWorkingShiftCode;
            ot.OverTimeInWorkingShiftName = db.OverTimeInWorkingShifts.AsNoTracking().FirstOrDefault(x => x.OverTimeInWorkingShiftId == owsID).OverTimeInWorkingShiftName;
            ot.WorkingShiftCode = db.WorkingShifts.AsNoTracking().FirstOrDefault(x => x.WorkingShiftId == workingShiftID).WorkingShiftCode;
            ot.WorkingShiftName = db.WorkingShifts.AsNoTracking().FirstOrDefault(x => x.WorkingShiftId == workingShiftID).WorkingShiftName;
            ot.ApprovalName = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == approvalID).EmployeeName;
            ot.Employee = db.Employees.FirstOrDefault(x => x.EmployeeId == employeeID);
            ot.JobPosition = db.JobPositions.FirstOrDefault(x => x.JobPositionId == ot.JobPositionId);
            ot.Organization = db.Organizations.FirstOrDefault(x => x.OrganizationId == ot.OrganizationId);
            ot.Approval = db.Employees.FirstOrDefault(x => x.EmployeeId == approvalID);
            ot.WorkingShift = db.WorkingShifts.FirstOrDefault(x => x.WorkingShiftId == workingShiftID);
            ot.OverTimeInWorkingShift = db.OverTimeInWorkingShifts.FirstOrDefault(x => x.OverTimeInWorkingShiftId == owsID);

            try
            {
                db.Overtimes.Add(ot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ViewBag.Employees = GetSelectListItems(SelectedItem.employee);
                ViewBag.WorkingShifts = GetSelectListItems(SelectedItem.workingshift);
                ViewBag.OWs = GetSelectListItems(SelectedItem.overtime_workingshift);
                ViewBag.Approvals = GetSelectListItems(SelectedItem.approval);
                ViewBag.StatusOvertimes = GetSelectListItems(SelectedItem.status_overtime);

                return View(ot);
            }
        }

        /// <summary>
        /// Sửa thông tin đơn làm thêm
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult OvertimeUpdate(string overtimeId)
        {
            Overtime ot = db.Overtimes.AsNoTracking().FirstOrDefault(x => x.OverTimeId == overtimeId);

            ViewBag.Employees = GetSelectListItems(SelectedItem.employee);
            ViewBag.WorkingShifts = GetSelectListItems(SelectedItem.workingshift);
            ViewBag.OWs = GetSelectListItems(SelectedItem.overtime_workingshift);
            ViewBag.Approvals = GetSelectListItems(SelectedItem.approval);
            ViewBag.StatusOvertimes = GetSelectListItems(SelectedItem.status_overtime);

            return View(ot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OvertimeUpdate(Overtime ot)
        {
            DateTime now = DateTime.Now;

            ot.ModifiedBy = "nnhiep";
            ot.ModifiedDate = now;
            ot.OverTimeEmployeeIds = ot.OverTimeEmployeeIds ?? "";
            ot.OverTimeEmployeeCodes = ot.OverTimeEmployeeCodes ?? "";
            ot.OverTimeEmployeeNames = ot.OverTimeEmployeeNames ?? "";
            ot.EmployeeCode = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == ot.EmployeeId).EmployeeCode;
            ot.EmployeeName = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == ot.EmployeeId).EmployeeName;
            ot.JobPositionId = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == ot.EmployeeId).JobPositionId;
            ot.JobPositionName = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == ot.EmployeeId).JobPositionName;
            ot.OverTimeInWorkingShiftCode = db.OverTimeInWorkingShifts.AsNoTracking().FirstOrDefault(x => x.OverTimeInWorkingShiftId == ot.OverTimeInWorkingShiftId).OverTimeInWorkingShiftCode;
            ot.OverTimeInWorkingShiftName = db.OverTimeInWorkingShifts.AsNoTracking().FirstOrDefault(x => x.OverTimeInWorkingShiftId == ot.OverTimeInWorkingShiftId).OverTimeInWorkingShiftName;
            ot.WorkingShiftCode = db.WorkingShifts.AsNoTracking().FirstOrDefault(x => x.WorkingShiftId == ot.WorkingShiftId).WorkingShiftCode;
            ot.WorkingShiftName = db.WorkingShifts.AsNoTracking().FirstOrDefault(x => x.WorkingShiftId == ot.WorkingShiftId).WorkingShiftName;
            ot.ApprovalName = db.Employees.AsNoTracking().FirstOrDefault(x => x.EmployeeId == ot.ApprovalId).EmployeeName;
            ot.Employee = db.Employees.FirstOrDefault(x => x.EmployeeId == ot.EmployeeId);
            ot.JobPosition = db.JobPositions.FirstOrDefault(x => x.JobPositionId == ot.JobPositionId);
            ot.Organization = db.Organizations.FirstOrDefault(x => x.OrganizationId == ot.OrganizationId);
            ot.Approval = db.Employees.FirstOrDefault(x => x.EmployeeId == ot.ApprovalId);
            ot.WorkingShift = db.WorkingShifts.FirstOrDefault(x => x.WorkingShiftId == ot.WorkingShiftId);
            ot.OverTimeInWorkingShift = db.OverTimeInWorkingShifts.FirstOrDefault(x => x.OverTimeInWorkingShiftId == ot.OverTimeInWorkingShiftId);

            try
            {
                db.Attach(ot);
                db.Entry(ot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                ViewBag.Employees = GetSelectListItems(SelectedItem.employee);
                ViewBag.WorkingShifts = GetSelectListItems(SelectedItem.workingshift);
                ViewBag.OWs = GetSelectListItems(SelectedItem.overtime_workingshift);
                ViewBag.Approvals = GetSelectListItems(SelectedItem.approval);
                ViewBag.StatusOvertimes = GetSelectListItems(SelectedItem.status_overtime);

                return View(ot);
            }
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
            OvertimeManagerContext db = new OvertimeManagerContext();
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
