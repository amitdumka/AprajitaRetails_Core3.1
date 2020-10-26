using AprajitaRetails.Areas.ToDo.Models;
using System.Collections.Generic;

namespace TodoList.Web.Models
{
    public class TodoViewModel
    {
        public IEnumerable<TodoItem> Todos { get; set; }
        public IEnumerable<TodoItem> Dones { get; set; }
        public IEnumerable<TodoItem> PublicTodos { get; set; }
        public IEnumerable<TodoItem> AssignedTodos { get; set; }//TODO : new additions
    }
}
