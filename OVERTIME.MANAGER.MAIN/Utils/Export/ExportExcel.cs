using OfficeOpenXml;
using OfficeOpenXml.Style;
using OVERTIME.MANAGER.MAIN.Models;
using OVERTIME.MANAGER.MAIN.Utils.Enums;
using OVERTIME.MANAGER.MAIN.ViewModels;
using System.Drawing;

namespace OVERTIME.MANAGER.MAIN.Utils
{
    public class ExportExcel
    {
        private static readonly OvertimeManagerContext db = new OvertimeManagerContext();

        // Khởi tạo file excel - nnhiep
        public static async Task<byte[]> GenerateExcelFile()
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Author = "nnhiep";
            var workSheet = package.Workbook.Worksheets.Add("List Overtime");

            List<OvertimeExport> data = GetData();
            
            FormatFile(workSheet, data);
            FillData(workSheet, data);

            return await package.GetAsByteArrayAsync();
        }

        // Khởi tạo file excel lựa chọn toolbar ẩn - nnhiep
        public static async Task<byte[]> GenerateExcelFileOption(string[] overtimeIds)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Author = "nnhiep";
            var workSheet = package.Workbook.Worksheets.Add("List Overtime");

            List<OvertimeExport> data = GetData(overtimeIds);

            FormatFile(workSheet, data);
            FillData(workSheet, data);

            return await package.GetAsByteArrayAsync();
        }

        // Định dạng File - nnhiep
        public static void FormatFile(ExcelWorksheet workSheet, List<OvertimeExport> data)
        {
            List<string> listHeaders = new List<string>
            {
                "A2", "B2", "C2", "D2", "E2", "F2", "G2", "H2", "I2", "J2", "K2", "L2", "M2", "N2", "O2"
            };

            FormatTitle(workSheet);
            FormatHeader(workSheet, listHeaders);
            FormatCell(workSheet, data);
        }

        // Định dạng Title - nnhiep
        public static void FormatTitle(ExcelWorksheet workSheet)
        {
            workSheet.Cells["A1:O1"].Merge = true;
            workSheet.Cells["A1"].Value = "List Overtimes".ToUpper();
            workSheet.Cells["A1"].Style.Font.SetFromFont("Times New Roman", 16, true);
            workSheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            workSheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        // Định dạng Header - nnhiep
        public static void FormatHeader(ExcelWorksheet workSheet, List<string> listHeaders)
        {
            listHeaders.ForEach(header =>
            {
                workSheet.Cells[header].Style.Font.SetFromFont("Times New Roman", 12, true);
                workSheet.Cells[header].Style.Fill.PatternType = ExcelFillStyle.Solid;
                workSheet.Cells[header].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                workSheet.Cells[header].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[header].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[header].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[header].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[header].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                workSheet.Cells[header].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            });

            DefineName(workSheet, listHeaders);
        }

        // Định dạng Cell - nnhiep
        public static void FormatCell(ExcelWorksheet worksheet, List<OvertimeExport> data)
        {
            worksheet.Cells.AutoFitColumns();

            worksheet.Cells[3, 3].AutoFitColumns(20);
            worksheet.Cells[3, 4].AutoFitColumns(20);
            worksheet.Cells[3, 5].AutoFitColumns(20);
            worksheet.Cells[3, 6].AutoFitColumns(18);
            worksheet.Cells[3, 7].AutoFitColumns(18);
            worksheet.Cells[3, 8].AutoFitColumns(18);
            worksheet.Cells[3, 9].AutoFitColumns(18);
            worksheet.Cells[3, 12].AutoFitColumns(20);
            worksheet.Cells[3, 13].AutoFitColumns(14);

            for (int i = 0; i < data.Count; i++)
            {
                for(int j = 1; j <= 15; j++)
                {
                    worksheet.Cells[i + 3, j].Style.Font.SetFromFont("Times New Roman", 12, false);
                    worksheet.Cells[i + 3, j].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, j].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, j].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, j].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[i + 3, j].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if(j == 1 || j == 6 || j == 7 || j == 8 || j == 13)
                    {
                        if(j == 6 || j == 7 || j == 8)
                        {
                            worksheet.Cells[i + 3, j].Style.Numberformat.Format = "dd/mm/yyyy";
                        }
                        worksheet.Cells[i + 3, j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                }
            }
        }

        // Định nghĩa tên cho các cột - nnhiep
        public static void DefineName(ExcelWorksheet workSheet, List<string> listHeaders)
        {
            workSheet.Cells[listHeaders[0]].Value = "Number";
            workSheet.Cells[listHeaders[1]].Value = "Employee Code";
            workSheet.Cells[listHeaders[2]].Value = "Employee Name";
            workSheet.Cells[listHeaders[3]].Value = "JobPosition";
            workSheet.Cells[listHeaders[4]].Value = "Organization";
            workSheet.Cells[listHeaders[5]].Value = "Apply Date";
            workSheet.Cells[listHeaders[6]].Value = "From Date";
            workSheet.Cells[listHeaders[7]].Value = "To Date";
            workSheet.Cells[listHeaders[8]].Value = "WorkingShift";
            workSheet.Cells[listHeaders[9]].Value = "OverTime WorkingShift";
            workSheet.Cells[listHeaders[10]].Value = "Reason";
            workSheet.Cells[listHeaders[11]].Value = "Approval";
            workSheet.Cells[listHeaders[12]].Value = "Status";
            workSheet.Cells[listHeaders[13]].Value = "OverTime Employee Codes";
            workSheet.Cells[listHeaders[14]].Value = "OverTime Employee Names";
        }
        
        // Lấy dữ liệu - nnhiep
        public static List<OvertimeExport> GetData()
        {
            return db.Overtimes.Select(x => new OvertimeExport
            {
                EmployeeCode = x.EmployeeCode,
                EmployeeName = x.EmployeeName,
                JobPositionName = x.JobPositionName,
                OrganizationName = x.OrganizationName,
                ApplyDate = x.ApplyDate,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                Reason = x.Reason,
                WorkingShiftName = x.WorkingShiftName,
                OverTimeInWorkingShiftName = x.OverTimeInWorkingShiftName,
                ApprovalName = x.ApprovalName,
                StatusOvertime = x.StatusOvertime,
                OverTimeEmployeeCodes = x.OverTimeEmployeeCodes,
                OverTimeEmployeeNames = x.OverTimeEmployeeNames,
            }).OrderBy(x => x.EmployeeCode).ToList();
        }

        // Lấy dữ liệu theo toolbar ẩn - nnhiep
        public static List<OvertimeExport> GetData(string[] overtimeIds)
        {
            return db.Overtimes.Select(x => new OvertimeExport
            {
                OvertimeId = x.OverTimeId,
                EmployeeCode = x.EmployeeCode,
                EmployeeName = x.EmployeeName,
                JobPositionName = x.JobPositionName,
                OrganizationName = x.OrganizationName,
                ApplyDate = x.ApplyDate,
                FromDate = x.FromDate,
                ToDate = x.ToDate,
                Reason = x.Reason,
                WorkingShiftName = x.WorkingShiftName,
                OverTimeInWorkingShiftName = x.OverTimeInWorkingShiftName,
                ApprovalName = x.ApprovalName,
                StatusOvertime = x.StatusOvertime,
                OverTimeEmployeeCodes = x.OverTimeEmployeeCodes,
                OverTimeEmployeeNames = x.OverTimeEmployeeNames,
            }).Where(x => overtimeIds.Contains(x.OvertimeId)).OrderBy(x => x.EmployeeCode).ToList();
        }

        // Đổ dữ liệu ra file - nnhiep
        public static void FillData(ExcelWorksheet workSheet, List<OvertimeExport> data)
        {
            for(int i = 0; i < data.Count; i++)
            {
                workSheet.Cells[i + 3, 1].Value = (i + 1).ToString();
                workSheet.Cells[i + 3, 2].Value = data[i].EmployeeCode;
                workSheet.Cells[i + 3, 3].Value = data[i].EmployeeName;
                workSheet.Cells[i + 3, 4].Value = data[i].JobPositionName;
                workSheet.Cells[i + 3, 5].Value = data[i].OrganizationName;
                workSheet.Cells[i + 3, 6].Value = data[i].ApplyDate == DateTime.MinValue ? "" : data[i].ApplyDate;
                workSheet.Cells[i + 3, 7].Value = data[i].FromDate == DateTime.MinValue ? "" : data[i].FromDate;
                workSheet.Cells[i + 3, 8].Value = data[i].ToDate == DateTime.MinValue ? "" : data[i].ToDate;
                workSheet.Cells[i + 3, 9].Value = data[i].Reason;
                workSheet.Cells[i + 3, 10].Value = data[i].WorkingShiftName;
                workSheet.Cells[i + 3, 11].Value = data[i].OverTimeInWorkingShiftName;
                workSheet.Cells[i + 3, 12].Value = data[i].ApprovalName;
                workSheet.Cells[i + 3, 13].Value = data[i].StatusOvertime;
                workSheet.Cells[i + 3, 13].Value = data[i].StatusOvertime == (byte)OvertimeStatus.pending ? "Pending" : data[i].StatusOvertime == (byte)OvertimeStatus.approved ? "Approved" : data[i].StatusOvertime == (byte)OvertimeStatus.refused ? "Refused" : "";
                workSheet.Cells[i + 3, 14].Value = data[i].OverTimeEmployeeCodes;
                workSheet.Cells[i + 3, 15].Value = data[i].OverTimeEmployeeNames;
            }
        }
    }
}
