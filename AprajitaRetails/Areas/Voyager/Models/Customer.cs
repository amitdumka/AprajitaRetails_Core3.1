﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Voyager.Models
{
    public class Customer
    {
        public int CustomerId { set; get; }

        [Display(Name = "First Name")]
        public string FirstName { set; get; }
        [Display(Name = " Last Name")]
        public string LastName { set; get; }
        public int Age { set; get; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public string City { set; get; }
        [Display(Name = "Contact No")]
        public string MobileNo { set; get; }
        public Genders Gender { set; get; }
        [Display(Name = "Bill Count")]
        public int NoOfBills { set; get; }
        [Display(Name = "Purchase Amount")]
        public decimal TotalAmount { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "Full Name")]
        public string FullName { get { return FirstName + " " + LastName; } }

    }
}
