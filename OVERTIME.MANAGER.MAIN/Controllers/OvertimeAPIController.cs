using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OVERTIME.MANAGER.MAIN.Models;
using OVERTIME.MANAGER.MAIN.Utils;
using OVERTIME.MANAGER.MAIN.Utils.Enums;
using OVERTIME.MANAGER.MAIN.ViewModels;
using System;
using X.PagedList;

#nullable disable
namespace OVERTIME.MANAGER.MAIN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeAPIController : ControllerBase
    {
        private readonly OvertimeManagerContext db = new OvertimeManagerContext();

        /// <summary>
        /// Lấy danh sách đơn làm thêm theo bộ lọc và phân trang
        /// </summary>
        [HttpGet]
        [Route("searchQuery={searchQuery}&pageIndex={pageIndex}&pageSize={pageSize}")]
        public ListItem<Employee> GetByFilter(string searchQuery = "", int pageIndex = 1, int pageSize = 15)
        {
            var lstEmployee = db.Employees.AsNoTracking().Where(x => x.EmployeeName.ToLower().Contains(searchQuery.ToLower()) || x.EmployeeCode.ToLower().Contains(searchQuery.ToLower()) || x.JobPositionName.ToLower().Contains(searchQuery.ToLower()) || x.OrganizationName.ToLower().Contains(searchQuery.ToLower())).OrderByDescending(x => x.ModifiedDate);

            ListItem<Employee> lst = new ListItem<Employee>()
            {
                list = new PagedList<Employee>(lstEmployee, pageIndex, pageSize),
                searchQuery = searchQuery,
                pageIndex = pageIndex,
                pageSize = pageSize,
                totalRecord = lstEmployee.Count(),
            };

            return lst;
        }

        /// <summary>
        /// Xóa một đơn làm thêm
        /// </summary>
        [HttpDelete]
        [Route("deleteById/{overtimeId}")]    
        public IActionResult DeleteOneRecord(string overtimeId)
        {
            var ot = db.Overtimes.Find(overtimeId);
            if(ot.StatusOvertime == (int)OvertimeStatus.approved)
            {
                return RedirectToAction("DontHavePermission", "Home");
            } else
            {
                try
                {
                    if (ot != null)
                    {
                        db.Overtimes.Remove(ot);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Xóa nhiều đơn làm thêm
        /// </summary>
        [HttpPost]
        [Route("bulk/delete")]
        public IActionResult DeleteRecords([FromBody] string[] overtimeIds)
        {
            var ot = db.Overtimes.Where(x => overtimeIds.Contains(x.OverTimeId) && x.StatusOvertime != (int)OvertimeStatus.approved);

            try
            {
                db.Overtimes.RemoveRange(ot);
                db.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("GetExcelFile")]    
        public async Task<IActionResult> GetExcelFile()
        {
            try
            {
                byte[] data = await ExportExcel.GenerateExcelFile();
                string filePath = Path.Combine("C:\\Users\\Admin\\Desktop", "list_overtimes.xlsx");
                System.IO.File.WriteAllBytes(filePath, data);

                return Ok();
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok();
            }
        }

        [HttpPost]
        [Route("GetExcelFileOption")]
        public async Task<IActionResult> GetExcelFile([FromBody] string[] overtimeIds)
        {
            try
            {
                byte[] data = await ExportExcel.GenerateExcelFileOption(overtimeIds);
                string filePath = Path.Combine("C:\\Users\\Admin\\Desktop", "list_overtimes.xlsx");
                System.IO.File.WriteAllBytes(filePath, data);

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Ok();
            }
        }

        /// <summary>
        /// Từ chối nhiều đơn làm thêm
        /// </summary>
        [HttpPost]
        [Route("ConvertToRefuse")]
        public IActionResult UpdateStatusRefuse([FromBody] string[] overtimeIds)
        {
            var ot = db.Overtimes.Where(x => overtimeIds.Contains(x.OverTimeId) && x.StatusOvertime != (int)OvertimeStatus.refused);

            try
            {
                foreach (var item in ot)
                {
                    item.StatusOvertime = (int)OvertimeStatus.refused;
                    item.ModifiedDate = DateTime.Now;
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Duyệt nhiều đơn làm thêm
        /// </summary>
        [HttpPost]
        [Route("ConvertToApprove")]
        public IActionResult UpdateStatusApprove([FromBody] string[] overtimeIds)
        {
            var ot = db.Overtimes.Where(x => overtimeIds.Contains(x.OverTimeId) && x.StatusOvertime != (int)OvertimeStatus.approved);

            try
            {
                foreach (var item in ot)
                {
                    item.StatusOvertime = (int)OvertimeStatus.approved;
                    item.ModifiedDate = DateTime.Now;
                }

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
