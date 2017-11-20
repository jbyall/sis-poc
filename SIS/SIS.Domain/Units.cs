using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Domain
{
    public class Unit
    {
        public int Id { get; set; }
        [StringLength(2)]
        public string Code { get; set; }
        [StringLength(20)]
        public string Description { get; set; }
    }
}
