using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SIS.Domain
{
    public class Item
    {
        public Item()
        {
            this.ItemLocations = new List<ItemLocation>();
            this.Transactions = new List<Transaction>();
        }
        [Key]
        [Column("ItemNumber")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [StringLength(70)]
        public string Name { get; set; }

        [Required]
        [StringLength(2)]
        public string Unit { get; set; }

        public decimal? Price { get; set; }
        [Range(-100000, 100000)]
        public int? ReorderPoint { get; set; }

        // Defines 1-1 relationship for Item-Supplier
        [Required]
        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }

        // Defines 1-many relationships
        [ScriptIgnore]
        public List<Transaction> Transactions { get; set; }
        public ICollection<ItemLocation> ItemLocations { get; set; }

    }
}