using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Intro.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        public string Name { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        virtual public string Test { get; set; }

        public long CategoryId { get; set; }
        virtual public Category Category { get; set; }

        public long SupplierId { get; set; }
        virtual public Supplier Supplier { get; set; }
    }
}
