using Microsoft.AspNetCore.Mvc.Rendering;
using OVERTIME.MANAGER.MAIN.Models;
using X.PagedList;

namespace OVERTIME.MANAGER.MAIN.ViewModels
{
    public class ListOvertime
    {
        #nullable disable
        public PagedList<Overtime> list { get; set; }
        public string searchQuery { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 15;
        public int totalRecord { get; set; } = 0;
        public Byte status { get; set; }
    }
}
