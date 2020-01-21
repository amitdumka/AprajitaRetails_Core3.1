using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Voyager.Models
{
    public class PurchaseTaxType
    {
        public int PurchaseTaxTypeId { get; set; }
        public string TaxName { get; set; }
        public TaxType TaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CompositeRate { get; set; }

        //Navigation
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }


}
