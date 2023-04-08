namespace OVERTIME.MANAGER.MAIN.Models;

// Nhân viên
public partial class Employee : BaseModel
{
    // ID của nhân viên
    public string EmployeeId { get; set; } = null!;

    // Mã nhân viên
    public string EmployeeCode { get; set; } = null!;

    // Tên nhân viên
    public string EmployeeName { get; set; } = null!;

    // ID của đơn vị công tác
    public string OrganizationId { get; set; } = null!;

    // Tên đơn vị công tác
    public string? OrganizationName { get; set; }

    // ID vị trí công việc
    public string JobPositionId { get; set; } = null!;

    // Tên vị trí công việc
    public string? JobPositionName { get; set; }

    // Ngày sinh
    public DateTime? DateOfBirth { get; set; }

    // Giới tính
    public byte? Gender { get; set; }

    // Số điện thoại
    public string? PhoneNumber { get; set; }

    // Địa chỉ
    public string? Addr { get; set; }

    // Email
    public string? Email { get; set; }

    // Tài khoản
    public string Account { get; set; } = null!;

    // Mật khẩu
    public string Pwd { get; set; } = null!;

    // Vai trò người dùng - 0 là người dùng bình thường và 1 là admin
    public byte EmployeeRole { get; set; }

    public virtual JobPosition JobPosition { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<Overtime> OvertimeApprovals { get; } = new List<Overtime>();

    public virtual ICollection<Overtime> OvertimeEmployees { get; } = new List<Overtime>();

    public virtual ICollection<OvertimeEmployee> OvertimeEmployeesNavigation { get; } = new List<OvertimeEmployee>();
}
