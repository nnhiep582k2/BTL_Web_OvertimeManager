namespace OVERTIME.MANAGER.MAIN.Models;

// Thời điểm làm thêm
public partial class OverTimeInWorkingShift : BaseModel
{
    // ID thời điểm làm thêm
    public string OverTimeInWorkingShiftId { get; set; } = null!;

    // Mã thời điểm làm thêm
    public string OverTimeInWorkingShiftCode { get; set; } = null!;

    // Tên thời điểm làm thêm
    public string OverTimeInWorkingShiftName { get; set; } = null!;

    public virtual ICollection<Overtime> Overtimes { get; } = new List<Overtime>();
}
