namespace SIS.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Location_LUT
    {
        [Key]
        [StringLength(8)]
        public string Location { get; set; }

        [Required]
        [StringLength(12)]
        public string Loc_Type { get; set; }

        [StringLength(10)]
        public string Old_Loc { get; set; }
    }
}
