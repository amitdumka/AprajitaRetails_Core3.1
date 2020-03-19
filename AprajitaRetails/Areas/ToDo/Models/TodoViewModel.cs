using System.Collections.Generic;
using AprajitaRetails.Areas.ToDo.Models;

namespace TodoList.Web.Models
{
    public class TodoViewModel
    {
        public IEnumerable<TodoItem> Todos { get; set; }
        public IEnumerable<TodoItem> Dones { get; set; }
        public IEnumerable<TodoItem> PublicTodos { get; set; }
    }
}
