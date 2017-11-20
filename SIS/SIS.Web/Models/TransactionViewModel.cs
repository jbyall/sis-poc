using SIS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIS.Web.Models
{
    public class TransactionViewModel
    {
        public TransactionViewModel()
        {

        }
        public TransactionViewModel(Item item, ItemLocation location)
        {
            this.ItemId = item.Id;
            this.ItemName = item.Name;
            this.ItemUnit = item.Unit;
            this.ItemPrice = item.Price;
            this.QuantityAvailable = location.QuantityOnHand;
            this.LocationType = LocationTypes.GetLocationTypeFromLocation(location.LocationId);
        }
        [Display(Name ="Item Number")]
        public string ItemId { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Unit")]
        public string ItemUnit { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Item Price")]
        public decimal? ItemPrice { get; set; }
        [Display(Name ="Qty Available")]
        public int? QuantityAvailable { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        public string LocationId { get; set; }

        public string LocationType { get; set; }

    }
}