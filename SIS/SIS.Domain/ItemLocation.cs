using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain
{
    public class ItemLocation
    {
        [Key]
        [Column(Order = 0)]
        public string ItemId { get; set; }
        public Item Item { get; set; }

        [Key]
        [Column(Order = 1)]
        public string LocationId { get; set; }
        public Location Location { get; set; }

        public int? QuantityOnHand { get; set; }
    }
}
