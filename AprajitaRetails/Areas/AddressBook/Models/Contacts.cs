using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.AddressBook.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Phone]
        public string? MobileNo { get; set; }
        [Phone]
        public string? PhoneNo { get; set; }
        [EmailAddress]
        public string? EMailAddress { get; set; }
        public string? Remarks { get; set; }
    }
}
