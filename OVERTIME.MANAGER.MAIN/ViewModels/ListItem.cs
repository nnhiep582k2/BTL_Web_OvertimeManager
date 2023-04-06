using X.PagedList;

namespace OVERTIME.MANAGER.MAIN.ViewModels
{
    public class ListItem<T>
    {
        #nullable disable
        public PagedList<T> list { get; set; }
        public string searchQuery { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 15;
        public int totalRecord { get; set; } = 0;
        public Byte? status { get; set; }
    }
}
