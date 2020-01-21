using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Areas.Voyager.Models
{
    public class SalesPerson
    {
        public int SalesPersonId { set; get; }
        [Display(Name = "Salesman")]
        public string SalesmanName { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }


}
