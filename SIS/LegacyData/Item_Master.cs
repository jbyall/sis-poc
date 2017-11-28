namespace LegacyData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item_Master
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item_Master()
        {
            Item_Location = new HashSet<Item_Location>();
            Master_Transactions = new HashSet<Master_Transactions>();
        }

        [Key]
        [Column("Item Number")]
        [StringLength(20)]
        public string Item_Number { get; set; }

        [Column("Item Name")]
        [Required]
        [StringLength(70)]
        public string Item_Name { get; set; }

        [StringLength(2)]
        public string Unit { get; set; }

        [Column(TypeName = "money")]
        public decimal? Price { get; set; }

        public short? Reorder_point { get; set; }

        [Column("Supplier ID")]
        [StringLength(10)]
        public string Supplier_ID { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item_Location> Item_Location { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_Transactions> Master_Transactions { get; set; }
    }
}
