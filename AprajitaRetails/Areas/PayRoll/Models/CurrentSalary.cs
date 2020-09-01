﻿using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
    public class CurrentSalary
    {
        //TODO: Think some thing others also 
        //TODO: Implement tailoring division on this model
        public int CurrentSalaryId { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

       
         [DataType (DataType.Currency), Column (TypeName = "money")] public decimal BasicSalary { get; set; }
        
         [DataType (DataType.Currency), Column (TypeName = "money")] public decimal SundaySalary { get; set; }
        
         [DataType (DataType.Currency), Column (TypeName = "money")] public decimal LPRate { get; set; }
        
         [DataType (DataType.Currency), Column (TypeName = "money")] public decimal IncentiveRate { get; set; }
        

         [DataType (DataType.Currency), Column (TypeName = "money")] public decimal IncentiveTarget { get; set; }
        
         [DataType (DataType.Currency), Column (TypeName = "money")] public decimal WOWBillRate { get; set; }

         [DataType (DataType.Currency), Column (TypeName = "money")] public decimal WOWBillTarget { get; set; }
        
        public bool IsSundayBillable { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EffectiveDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CloseDate { get; set; }

        public bool IsEffective { get; set; }
        public bool? IsTailoring { get; set; }

        public virtual ICollection<PaySlip> PaySlips { get; set; }
        
    }

}