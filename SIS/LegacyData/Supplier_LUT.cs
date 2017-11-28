namespace LegacyData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Supplier_LUT
    {
        [Key]
        [StringLength(10)]
        public string SupplierID { get; set; }

        [Required]
        [StringLength(50)]
        public string Supplier { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(2)]
        public string State { get; set; }

        [Required]
        [StringLength(10)]
        public string Zip { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }
    }
}
