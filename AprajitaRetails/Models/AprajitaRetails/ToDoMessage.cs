using System;
//using System.Data.Entity;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class ToDoMessage
    {
        public int ToDoMessageId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime OnDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? OverDate { get; set; }
        public string Status { get; set; }
        public bool IsOver { get; set; }
    }

  

}
