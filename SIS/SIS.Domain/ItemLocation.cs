﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SIS.Domain
{
    // NOTE - THIS IS A JOIN TABLE AND SHOULD NOT BE INSERTED NOR DELETED TO/FROM DIRECTLY
    // ITEM LOCATIONS SHOULD BE ADDED AND/OR REMOVED FROM THE ITEM IN CODE
    public class ItemLocation
    {
        // THE MULTIPLE KEYS DEFINE A 1-1 RELATIONSHIP BETWEEN AN ITEM LOCATION
        // AND UNIQUE COMBINATION OF ITEM AND LOCATION
        [Key]
        [Column(Order = 0)]
        public string ItemId { get; set; }
        [ScriptIgnore]
        public Item Item { get; set; }

        [Key]
        [Column(Order = 1)]
        public string LocationId { get; set; }
        [ScriptIgnore]
        public Location Location { get; set; }

        public int? QuantityOnHand { get; set; }
    }
}
