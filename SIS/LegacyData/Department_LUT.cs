namespace LegacyData
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Department_LUT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Department_LUT()
        {
            Master_Transactions = new HashSet<Master_Transactions>();
        }

        [Key]
        [StringLength(5)]
        public string Department { get; set; }

        [Column(" Dept_desc")]
        [StringLength(50)]
        public string C_Dept_desc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Master_Transactions> Master_Transactions { get; set; }
    }
}
