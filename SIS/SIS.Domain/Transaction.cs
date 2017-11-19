using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain
{
    public class Transaction
    {
        #region Relationships(Needed for EntityFramework)
        // Unique Transaction ID
        public int Id { get; set; }

        // Defines 1-1 relationship for Transaction-Item
        [Column("ItemNumber")]
        public string ItemId { get; set; }
        public Item Item { get; set; }

        public DateTime Date { get; set; }

        // Defines 1-1 relationship for Transaction-Department
        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        #endregion

        #region Properties(Database Fields)
        // On hand transaction quantities (nullable)
        [Column("Qty_OnHand_Dist")]
        public int? QuantityOnHandDist { get; set; }
        [Column("Qty_OnHand_Store")]
        public int? QuantityOnHandStor { get; set; }
        [Column("Qty_OnHand_Sub")]
        public int? QuantityOnHandSub { get; set; }

        // Transaction change quantities (not-nullable)
        [Column("Qty_Change_Dist")]
        public int QuantityChangeDist { get; set; }
        [Column("Qty_Change_Stor")]
        public int QuantityChangeStor { get; set; }
        [Column("Qty_Change_Sub")]
        public int QuantityChangeSub { get; set; }


        public decimal ItemPrice { get; set; }
        public decimal TransactionValue { get; set; }
        #endregion
    }
}