namespace OVERTIME.MANAGER.MAIN.Models;

// Vị trí công việc
public partial class JobPosition : BaseModel
{
    // ID của vị trí công việc
    public string JobPositionId { get; set; } = null!;

    // Mã vị trí công việc
    public string JobPositionCode { get; set; } = null!;

    // Tên vị trí công việc
    public string JobPositionName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<OvertimeEmployee> OvertimeEmployees { get; } = new List<OvertimeEmployee>();

    public virtual ICollection<Overtime> Overtimes { get; } = new List<Overtime>();
}
