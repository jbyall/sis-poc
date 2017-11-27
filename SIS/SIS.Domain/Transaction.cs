using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain
{
    #region enums
    public static class TransactionTypes
    {
        public const string Handout = "Handout";
        public const string Receive = "Receive";
    }
    #endregion

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
        public int QuantityChange { get; set; }
        public decimal ItemPrice { get; set; }
        public decimal TransactionValue { get; set; }
        public string LocationId { get; set; }
        #endregion
    }
}