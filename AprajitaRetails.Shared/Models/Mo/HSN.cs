using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Areas.Sales.Models.Views
{



    public class HSN
    {
        [Key]
        public long HSNCode { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal CESS { get; set; }

        public ICollection<RegularSaleItem> RegularSaleItems { get; set; }
    }

    public class HSNSeeder
    {
        public List<HSN> HSNList { get; set; }
        public static void GetHSNList()
        {
            List<HSN> list = new List<HSN>();


        }
    }
}
