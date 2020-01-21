using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Voyager.Models
{
    public class PurchaseItem
    {
        public int PurchaseItemId { get; set; }//Pk

        public int ProductPurchaseId { get; set; }//FK
        public virtual ProductPurchase ProductPurchase { get; set; }

        public int ProductItemId { get; set; } //FK 
        public virtual ProductItem ProductItem { get; set; }
        public string Barcode { get; set; }// TODO: if not working then link with productitemid

        public double Qty { get; set; }
        public Units Unit { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Cost { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TaxAmout { get; set; }

        public int? PurchaseTaxTypeId { get; set; } //TODO: Temp Purpose. need to calculate tax here
        public virtual PurchaseTaxType PurchaseTaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CostValue { get; set; }


        //Navigation Properties


    }


}
