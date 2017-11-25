using SIS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIS.Web.Models
{
    public class HandoutViewModel
    {
        public HandoutViewModel()
        {

        }
        public HandoutViewModel(Item item)
        {
            this.ItemId = item.Id;
            this.ItemName = item.Name;
            this.ItemUnit = item.Unit;
            this.ItemPrice = item.Price;
            //this.QuantityAvailable = location.QuantityOnHand;
            //this.LocationType = LocationTypes.GetLocationTypeFromLocation(location.LocationId);
            this.Transactions = new List<HandoutTransaction>();
            foreach (var location in item.ItemLocations)
            {
                var transaction = new HandoutTransaction
                {
                    Location = location,
                    HandOutQuantity = 0,
                };
                this.Transactions.Add(transaction);
            }
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
        public string DepartmentId { get; set; }

        public string LocationId { get; set; }

        public string LocationType { get; set; }

        public List<HandoutTransaction> Transactions { get; set; }
        //public List<ItemLocation> ItemLocations { get; set; }
        //public Dictionary<ItemLocation,int> TransactionLocations { get; set; }

    }

    public class HandoutTransaction
    {
        public ItemLocation Location { get; set; }
        public int HandOutQuantity { get; set; }
    }
}