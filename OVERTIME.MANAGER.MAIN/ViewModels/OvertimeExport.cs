namespace OVERTIME.MANAGER.MAIN.ViewModels
{
    public class OvertimeExport
    {
        // Id đơn làm thêm
        public string? OvertimeId { get; set; }

        // Mã nhân viên nộp đơn làm thêm
        public string EmployeeCode { get; set; } = null!;

        // Tên nhân viên nộp đơn làm thêm
        public string EmployeeName { get; set; } = null!;

        // Tên đơn vị công tác
        public string? OrganizationName { get; set; }

        // Tên vị trí công việc
        public string? JobPositionName { get; set; }

        // Ngày nộp đơn
        public DateTime ApplyDate { get; set; }

        // Thời gian làm thêm từ
        public DateTime FromDate { get; set; }

        // Thời gian làm thêm đến
        public DateTime ToDate { get; set; }

        // Tên ca làm việc
        public string WorkingShiftName { get; set; } = null!;

        // Tên thời điểm làm thêm
        public string OverTimeInWorkingShiftName { get; set; } = null!;

        // Lý do làm thêm
        public string Reason { get; set; } = null!;

        // Tên người duyệt đơn
        public string ApprovalName { get; set; } = null!;

        // Trạng thái đơn làm thêm
        public Byte StatusOvertime { get; set; }

        // Danh sách mã nhân viên đi làm thêm
        public string? OverTimeEmployeeCodes { get; set; }

        // Danh sách tên nhân viên đi làm thêm
        public string? OverTimeEmployeeNames { get; set; }
    }
}
