using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OVERTIME.MANAGER.MAIN.Models;
using OVERTIME.MANAGER.MAIN.Utils.Enums;
using OVERTIME.MANAGER.MAIN.ViewModels;

namespace OVERTIME.MANAGER.MAIN.Controllers
{
    public class AccessController : Controller
    {
        private readonly OvertimeManagerContext db = new OvertimeManagerContext();

#nullable disable
        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("Account") == null)
            {
                ViewBag.JobPosition = GetSelectListItems(SelectedItem.jobposition);
                ViewBag.Organization = GetSelectListItems(SelectedItem.organization);

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Register(UserRegister userRegister)
        {
            DateTime now = DateTime.Now;
            HttpContext.Session.SetString("Account", userRegister.Account.ToString());
            int maxEmployeeCode = db.Employees.Max(x => Convert.ToInt32(x.EmployeeCode.Replace("NV", ""))) + 1;

            Employee employee = new Employee
            {
                EmployeeId = Guid.NewGuid().ToString(),
                EmployeeCode = "NV" + maxEmployeeCode.ToString(),
                EmployeeName = userRegister.EmployeeName,
                JobPositionId = userRegister.JobPositionId,
                JobPositionName = db.JobPositions.FirstOrDefault(x => x.JobPositionId == userRegister.JobPositionId).JobPositionName,
                OrganizationId = userRegister.OrganizationId,
                OrganizationName = db.Organizations.FirstOrDefault(x => x.OrganizationId == userRegister.OrganizationId).OrganizationName,
                Account = userRegister.Account,
                Pwd = userRegister.Pwd,
                EmployeeRole = 0,
                CreatedBy = "nnhiep",
                ModifiedBy = "nnhiep",
                CreatedDate = now,
                ModifiedDate = now,
            };

            db.Employees.Add(employee);
            db.SaveChanges();

            return RedirectToAction("Index", "OvertimeManager");
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Account") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(UserLogin employee)
        {
            if (HttpContext.Session.GetString("Account") == null)
            {
                var user = db.Employees.Where(x => x.Account.Equals(employee.Account) && x.Pwd.Equals(employee.Pwd)).FirstOrDefault();

                if (user != null)
                {
                    HttpContext.Session.SetString("Account", employee.Account.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Account");
            return RedirectToAction("Login", "Access");
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
            OvertimeManagerContext db = new OvertimeManagerContext();
            switch (item)
            {
                case (SelectedItem.jobposition):
                    items = new SelectList(db.JobPositions.ToList(), "JobPositionId", "JobPositionName");
                    break;
                case (SelectedItem.organization):
                    items = new SelectList(db.Organizations.ToList(), "OrganizationId", "OrganizationName");
                    break;
                default:
                    items = null;
                    break;
            }
            return items;
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// @author nnhiep
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="newPassword">Mật khẩu mới</param>
        /// @author nnhiep
        public IActionResult ChangePassword(UserChangePwd userChangePwd)
        {
            Employee temp = db.Employees.FirstOrDefault(x => x.Account.Equals(HttpContext.Session.GetString("Account")));

            if(temp != null && userChangePwd.OldPwd.Equals(temp.Pwd) && !string.IsNullOrEmpty(userChangePwd.NewPwd.Trim()))
            {
                temp.Pwd = userChangePwd.NewPwd;
                temp.ModifiedDate = DateTime.Now;
                db.Attach(temp);
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "OvertimeManager");
            } else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult ChangeInformation()
        {
            Employee temp = db.Employees.FirstOrDefault(x => x.Account.Equals(HttpContext.Session.GetString("Account")));

            ViewBag.JobPosition = GetSelectListItems(SelectedItem.jobposition);
            ViewBag.Organization = GetSelectListItems(SelectedItem.organization);

            return View(temp);
        }

        [HttpPost]
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// @author nnhiep
        public IActionResult ChangeInformation(Employee employee)
        {
            Employee temp = db.Employees.FirstOrDefault(x => x.Account.Equals(HttpContext.Session.GetString("Account")));

            string jobPositionName = db.JobPositions.FirstOrDefault(x => x.JobPositionId == employee.JobPositionId).JobPositionName.ToString();
            string organizationName = db.Organizations.FirstOrDefault(x => x.OrganizationId == employee.OrganizationId).OrganizationName.ToString();

            if (temp != null)
            {
                temp.EmployeeName = employee.EmployeeName;
                temp.OrganizationId = employee.OrganizationId;
                temp.OrganizationName = employee.OrganizationName;
                temp.JobPositionId = employee.JobPositionId;
                temp.JobPositionName = employee.JobPositionName;
                temp.PhoneNumber = employee.PhoneNumber;
                temp.ModifiedDate = DateTime.Now;
                db.Attach(temp);
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "OvertimeManager");
            }
            else
            {
                return View();
            }
        }
    }
}
