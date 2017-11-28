namespace LegacyData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item_Location
    {
        [Key]
        [Column("Item Number", Order = 0)]
        [StringLength(20)]
        public string Item_Number { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string Location { get; set; }

        public short? Qty_OnHand { get; set; }

        public virtual Item_Master Item_Master { get; set; }
    }
}
