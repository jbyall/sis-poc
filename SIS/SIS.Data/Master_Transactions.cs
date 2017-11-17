namespace SIS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Master_Transactions
    {
        [Key]
        [Column("Item Number", Order = 0)]
        [StringLength(20)]
        public string Item_Number { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime Transaction_Date { get; set; }

        [StringLength(5)]
        // Not for receiving, for dispersments
        public string Department { get; set; }

        public short? Dist_Qty_OnHand { get; set; }

        public short Dist_QtyChange { get; set; }

        public short? Stor_Qty_OnHand { get; set; }

        public short Stor_QtyChange { get; set; }

        public short? Sub_Qty_OnHand { get; set; }

        public short Sub_QtyChange { get; set; }

        [Column(TypeName = "money")]
        // pulled from item master
        public decimal Item_Price { get; set; }

        [Column(TypeName = "money")]
        // quantity times price
        public decimal Transaction_Value { get; set; }

        public virtual Department_LUT Department_LUT { get; set; }

        public virtual Item_Master Item_Master { get; set; }
    }
}
