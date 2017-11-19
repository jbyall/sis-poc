﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain
{
    [Table("Department_LUT")]
    public class Department
    {
        public Department()
        {
            this.Transactions = new List<Transaction>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        // Defines the 1-many relationship
        // Between department and transactions
        public List<Transaction> Transactions { get; set; }
    }
}
