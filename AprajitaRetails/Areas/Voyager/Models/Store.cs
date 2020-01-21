using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Voyager.Models
{
    public class Store
    {
        public int StoreID { get; set; }
        [Display(Name = "Store Code")]
        public string StoreCode { get; set; }
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name = "Pin Code")]
        public string PinCode { get; set; }
        [Display(Name = "Contact No")]
        public string PhoneNo { get; set; }
        [Display(Name = "Store Manager Name")]
        public string StoreManagerName { get; set; }
        [Display(Name = "SM Contact No")]
        public string StoreManagerPhoneNo { get; set; }
        [Display(Name = "PAN No")]
        public string PanNo { get; set; }
        [Display(Name = "GSTIN ")]
        public string GSTNO { get; set; }
        [Display(Name = "Employees Count")]
        public int NoOfEmployees { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Opening Date")]
        public DateTime OpeningDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Closing Date")]
        public DateTime? ClosingDate { get; set; }
        [Display(Name = "Operative")]
        public bool Status { get; set; }
    }


}
