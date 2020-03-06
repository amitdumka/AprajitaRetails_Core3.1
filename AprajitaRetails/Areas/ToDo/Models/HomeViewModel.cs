using System.Collections.Generic;
using AprajitaRetails.Areas.ToDo.Models;

namespace TodoList.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<TodoItem> RecentlyAddedTodos { get; set; }
        public IEnumerable<TodoItem> CloseDueToTodos { get; set; }
        public IEnumerable<TodoItem> MonthlyToTodos { get; set; }
        public CalendarViewModel CalendarViewModel { get; set; }
    }
}
