using Microsoft.Build.Framework;
using System.ComponentModel;

namespace OVERTIME.MANAGER.MAIN.Models;

// Đơn làm thêm
public partial class Overtime : BaseModel
{
    // ID đơn làm thêm
    public string OverTimeId { get; set; } = null!;

    // ID nhân viên nộp đơn làm thêm
    public string EmployeeId { get; set; } = null!;

    // Mã nhân viên nộp đơn làm thêm
    public string EmployeeCode { get; set; } = null!;

    // Tên nhân viên nộp đơn làm thêm
    [Required]
    [DisplayName("Employee Name")]
    public string EmployeeName { get; set; } = null!;

    // ID đơn vị công tác
    public string OrganizationId { get; set; } = null!;

    // Tên đơn vị công tác
    [DisplayName("Organization Name")]
    public string? OrganizationName { get; set; }

    // ID vị trí công việc
    public string JobPositionId { get; set; } = null!;

    // Tên vị trí công việc
    public string? JobPositionName { get; set; }

    // Ngày nộp đơn
    [Required]
    [DisplayName("Apply Date")]
    public DateTime ApplyDate { get; set; }

    // Thời gian làm thêm từ
    [Required]
    [DisplayName("From Date")]
    public DateTime FromDate { get; set; }

    // Thời gian nghỉ giữa ca từ
    [DisplayName("Breaktime From")]
    public DateTime? BreakTimeFrom { get; set; }

    // Thời gian nghỉ giữa ca đến
    [DisplayName("Breaktime To")]
    public DateTime? BreakTimeTo { get; set; }

    // Thời gian làm thêm đến
    [Required]
    [DisplayName("To Date")]
    public DateTime ToDate { get; set; }

    // ID của thời điểm làm thêm
    public string OverTimeInWorkingShiftId { get; set; } = null!;

    // Mã thời điểm làm thêm
    public string OverTimeInWorkingShiftCode { get; set; } = null!;

    // Tên thời điểm làm thêm
    [Required]
    [DisplayName("OWs")]
    public string OverTimeInWorkingShiftName { get; set; } = null!;

    // ID ca làm việc
    public string WorkingShiftId { get; set; } = null!;

    // Mã ca làm việc
    public string WorkingShiftCode { get; set; } = null!;

    // Tên ca làm việc
    [Required]
    [DisplayName("Workingshift Name")]
    public string WorkingShiftName { get; set; } = null!;

    // Lý do làm thêm
    [Required]
    [DisplayName("Reason")]
    public string Reason { get; set; } = null!;

    // ID người duyệt đơn
    public string ApprovalId { get; set; } = null!;

    // Tên người duyệt đơn
    [Required]
    [DisplayName("Approval Name")]
    public string ApprovalName { get; set; } = null!;

    // Trạng thái đơn làm thêm
    [Required]
    [DisplayName("Status")]
    public Byte StatusOvertime { get; set; }

    // Ghi chú của đơn làm thêm
    [DisplayName("Description")]
    public string? Dsc { get; set; }

    // Danh sách ID của nhân viên đi làm thêm
    public string? OverTimeEmployeeIds { get; set; }

    // Danh sách mã nhân viên đi làm thêm
    public string? OverTimeEmployeeCodes { get; set; }

    // Danh sách tên nhân viên đi làm thêm
    public string? OverTimeEmployeeNames { get; set; }

    public virtual Employee Approval { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual JobPosition JobPosition { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual OverTimeInWorkingShift OverTimeInWorkingShift { get; set; } = null!;

    public virtual ICollection<OvertimeEmployee> OvertimeEmployees { get; } = new List<OvertimeEmployee>();

    public virtual WorkingShift WorkingShift { get; set; } = null!;
}
