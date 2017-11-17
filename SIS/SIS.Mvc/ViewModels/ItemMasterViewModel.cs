using SIS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIS.Mvc.ViewModels
{
    public class ItemMasterViewModel
    {

        [Required]
        [Display(Name = "Name")]
        public string ItemName { get; set; }
        public string Location { get; set; }
    }
}