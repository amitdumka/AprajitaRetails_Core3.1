using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Purchase.Models
{
    //Global Class
    public class PurchaseTaxType
    {
        public int PurchaseTaxTypeId { get; set; }
        [Display(Name = "Tax")]
        public string TaxName { get; set; }
        [Display(Name = "Tax Type")]
        public TaxType TaxType { get; set; }

        [Display(Name = "Composite Rate")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CompositeRate { get; set; }

        //Navigation
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }


}
