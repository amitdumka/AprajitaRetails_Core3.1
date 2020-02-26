using AprajitaRetails.Data;
using AprajitaRetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.Helpers
{
    public class ToDoManager
    {
        public void AddToDoList(AprajitaRetailsContext db, string title,string msg, DateTime duedate)
        {
            ToDoMessage todo = new  ToDoMessage{
                IsOver=false, OnDate=DateTime.Now,
                Message=msg, DueDate=duedate, Status="Pending", Title=title
            };
            db.ToDoMessages.Add(todo);
            db.SaveChanges();
        }
        public List<ToDoMessage> ListToDoList(AprajitaRetailsContext db)
        {
            return db.ToDoMessages.Where(c => !c.IsOver).OrderByDescending(c => c.DueDate).ToList();
        }
        public void UpDateToDoList(AprajitaRetailsContext db, ToDoMessage todo)
        {
            db.Update(todo);
            db.SaveChanges();

        }
    }
}
