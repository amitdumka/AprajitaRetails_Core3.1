//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace AprajitaRetails.Models
{
    public class TelegramAuthUser
    {
        public int TelegramAuthUserId { get; set; }
        public long TelegramChatId { get; set; }
        public string TelegramUserName { get; set; }
        [Phone]
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public EmpType EmpType { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }

    //TODO: List
    //TODO: Dues Recovery options
    //TODO: Tailoring 
    //TODO: Sales return policy update and check 
    //TODO: Purchase of Items/Assets
    //TODO: Arvind Payments
    //TODO: Purchase Invoice Entry

}
