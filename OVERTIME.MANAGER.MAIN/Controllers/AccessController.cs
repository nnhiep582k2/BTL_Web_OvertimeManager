using Microsoft.AspNetCore.Mvc;
using OVERTIME.MANAGER.MAIN.Models;
using OVERTIME.MANAGER.MAIN.ViewModels;

namespace OVERTIME.MANAGER.MAIN.Controllers
{
    public class AccessController : Controller
    {
        private readonly OvertimeManagerContext db = new OvertimeManagerContext();

        [HttpGet]
        public IActionResult Register()
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
        public IActionResult Register(Employee employee)
        {
            HttpContext.Session.SetString("Account", employee.Account.ToString());

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
    }
}
