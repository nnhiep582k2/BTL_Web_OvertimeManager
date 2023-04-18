namespace OVERTIME.MANAGER.MAIN.Models
{
    public class BaseModel
    {
       
        public DateTime? CreatedDate {
            get;

            set;
        }

        // Người tạo
        public string? CreatedBy { get; set; }

        // Thời gian chỉnh sửa gần nhất
        public DateTime? ModifiedDate { get; set; }

        // Người chỉnh sửa gần nhất
        public string? ModifiedBy { get; set; }
    }
}
