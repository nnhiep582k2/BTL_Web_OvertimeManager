namespace OVERTIME.MANAGER.MAIN.Models;

// Đơn vị công tác
public partial class Organization : BaseModel
{
    // ID của đơn vị công tác
    public string OrganizationId { get; set; } = null!;

    // Mã đơn vị công tác
    public string OrganizationCode { get; set; } = null!;

    // Tên đơn vị công tác
    public string OrganizationName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual ICollection<OvertimeEmployee> OvertimeEmployees { get; } = new List<OvertimeEmployee>();

    public virtual ICollection<Overtime> Overtimes { get; } = new List<Overtime>();
}
