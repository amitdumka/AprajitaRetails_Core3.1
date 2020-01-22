using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Sales.Models
{
    public class SaleTaxType
    {
        public int SaleTaxTypeId { get; set; }

        public string TaxName { get; set; }
        public TaxType TaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CompositeRate { get; set; }

        //Navigation
        public ICollection<SaleItem> SaleItems { get; set; }
    }


}
