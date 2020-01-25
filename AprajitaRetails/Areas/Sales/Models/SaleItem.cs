
using AprajitaRetails.Areas.Purchase.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Sales.Models
{
    public class SaleItem
    {
        public int SaleItemId { get; set; }

        public int SaleInvoiceId { get; set; }

        public int ProductItemId { get; set; }
        public virtual ProductItem ProductItem { get; set; }
        public string BarCode { get; set; }

        public double Qty { get; set; }
        public Units Units { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TaxAmount { get; set; }

        public int? SaleTaxTypeId { get; set; }
        public virtual SaleTaxType SaleTaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BillAmount { get; set; }
        // SaleTax options and it will Done

        public int SalesPersonId { get; set; }

        public virtual SaleInvoice SaleInvoice { get; set; }
        public virtual SalesPerson Salesman { get; set; }
    }

    public class ManualSaleItem
    {
        public int ManualSaleItemId { get; set; }

        public int ManualInvoiceId { get; set; }

        public int ProductItemId { get; set; }
        public virtual ProductItem ProductItem { get; set; }
        public string BarCode { get; set; }

        public double Qty { get; set; }
        public Units Units { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MRP { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BasicAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TaxAmount { get; set; }

        public int? SaleTaxTypeId { get; set; }
        public virtual SaleTaxType SaleTaxType { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal BillAmount { get; set; }
        // SaleTax options and it will Done

        public int SalesPersonId { get; set; }

        public virtual ManualInvoice SaleInvoice { get; set; }
        public virtual SalesPerson Salesman { get; set; }
    }

}
