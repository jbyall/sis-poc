using SIS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SIS.Web.Models
{
    public class ReceiveViewModel
    {
        public ReceiveViewModel()
        {

        }
        public ReceiveViewModel(Item item)
        {
            this.ItemId = item.Id;
            this.ItemName = item.Name;
            this.ItemUnit = item.Unit;
            this.ItemPrice = item.Price;
            this.Transactions = new List<ReceiveTransaction>();
            foreach (var location in item.ItemLocations)
            {

            }
        }
        [Display(Name = "Item Number")]
        public string ItemId { get; set; }
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Display(Name = "Unit")]
        public string ItemUnit { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Item Price")]
        public decimal? ItemPrice { get; set; }
        [Display(Name = "Qty Available")]
        public int? QuantityAvailable { get; set; }

        [Display(Name = "Department")]
        public string DepartmentId { get; set; }

        public string LocationId { get; set; }

        public string LocationType { get; set; }

        public List<ReceiveTransaction> Transactions { get; set; }
    }

    public class ReceiveTransaction
    {
        public ItemLocation Location { get; set; }
        public int ReceiveQuantity { get; set; }
    }
}