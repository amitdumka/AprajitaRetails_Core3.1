//using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
//using AprajitaRetails.Areas.Voyager.Models;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AprajitaRetails.Areas.Voyager.Models;

namespace AprajitaRetails.Models
{
    public class Salesman
    {
        public int SalesmanId { get; set; }
        [Display(Name = "Salesman")]
        public string SalesmanName { get; set; }

        public virtual ICollection<DailySale> DailySales { get; set; }
        // public virtual ICollection<SaleItem> SaleItems { get; set; }

        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }
        public virtual Store Store { get; set; }
        public int StoreLocationId { get; internal set; }
    }

    

}
