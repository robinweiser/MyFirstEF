using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyFirstEF
{
    public class Product
    {
        public int productId { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }    
        public string Description { get; set; }
        public Supplier Supplier { get; set; }
        public Producer Producer { get; set; }
        public Productgroup WG { get; set; }
        public List<Material> Materials { get; set; }
    }
}
