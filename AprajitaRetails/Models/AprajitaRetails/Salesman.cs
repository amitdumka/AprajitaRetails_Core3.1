//using System.Data.Entity;
using AprajitaRetails.Areas.Sales.Models.Views;
//using AprajitaRetails.Areas.Voyager.Models;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AprajitaRetails.Areas.Voyager.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Models
{
    public class Salesman
    {
        public int SalesmanId { get; set; }
        [Display(Name = "Salesman")]
        public string SalesmanName { get; set; }

        public virtual ICollection<DailySale> DailySales { get; set; }
        public virtual ICollection<RegularSaleItem> SaleItems { get; set; }

        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }
        public virtual Store Store { get; set; }
        // public int StoreLocationId { get; internal set; }

        public int? EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }



}
