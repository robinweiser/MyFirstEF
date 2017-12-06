using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstEF
{
    public class Material
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public List<Product> Products { get; set; }
    }
}
