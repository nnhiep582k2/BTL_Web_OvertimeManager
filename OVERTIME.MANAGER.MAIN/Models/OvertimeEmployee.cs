namespace OVERTIME.MANAGER.MAIN.Models;

// Nhân viên làm thêm
public partial class OvertimeEmployee : BaseModel
{
    // ID chi tiết của đơn làm thêm
    public string OverTimeDetailId { get; set; } = null!;

    // ID đơn làm thêm
    public string OverTimeId { get; set; } = null!;

    // ID nhân viên làm thêm
    public string EmployeeId { get; set; } = null!;

    // Mã nhân viên làm thêm
    public string EmployeeCode { get; set; } = null!;

    // Tên nhân viên làm thêm
    public string EmployeeName { get; set; } = null!;

    // ID đon vị công tác
    public string OrganizationId { get; set; } = null!;

    // Tên đơn vị công tác
    public string? OrganizationName { get; set; }

    // ID vị trí công việc
    public string JobPositionId { get; set; } = null!;

    // Tên vị trí công việc
    public string? JobPositionName { get; set; }

    // Số điện thoại nhân viên đi làm thêm
    public string? PhoneNumber { get; set; }

    // Email của nhân viên đi làm thêm
    public string? Email { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual JobPosition JobPosition { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual Overtime OverTime { get; set; } = null!;
}
