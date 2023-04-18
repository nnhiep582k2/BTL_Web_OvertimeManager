using System.ComponentModel.DataAnnotations;

namespace OVERTIME.MANAGER.MAIN.Models;

// Ca làm việc
public partial class WorkingShift : BaseModel
{
    // ID ca làm việc
    public string WorkingShiftId { get; set; } = null!;

    // Mã ca làm việc
    public string WorkingShiftCode { get; set; } = null!;

    // Tên ca làm việc
    public string WorkingShiftName { get; set; } = null!;

 

    public virtual ICollection<Overtime> Overtimes { get; } = new List<Overtime>();
}
