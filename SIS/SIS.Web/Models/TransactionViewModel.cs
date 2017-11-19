using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIS.Web.Models
{
    public class TransactionViewModel
    {
        [Display(Name ="Item Number")]
        public string ItemId { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Unit")]
        public string ItemUnit { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Item Price")]
        public decimal ItemPrice { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public string LocationId { get; set; }


    }
}