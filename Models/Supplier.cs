using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFramework.Intro.Models
{
    public class Supplier
    {
        public long SupplierId { get; set; }

        public string Name { get; set; }
        public string City { get; set; }

        virtual public IEnumerable<Product> Products { get; set; }

    }
    }
