using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Voyager.Models
{
    public class ArvindPayment
    {
        public int ArvindPaymentId { get; set; }
        public ArvindAccount Arvind { get; set; }
        [DataType (DataType.Date), DisplayFormat (DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public string InvoiceNo { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Amount { get; set; }
        public string BankDetails { get; set; }
        public string Remarks { get; set; }

        public string UserName { get; set; }

    }
}
