using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MyFirstEF
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        [MaxLength(50)]
        public string SupplierName { get; set; }
        public bool Active { get; set; }
        public List<Product> Products { get; set; }
        public Address Address { get; set; }
    }

    public class Producer : Supplier
    {
        [MaxLength(100)]
        public string Notice { get; set; }
    }
}
