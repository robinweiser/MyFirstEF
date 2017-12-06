using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstEF
{
    public class Address
    {
        public string zip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }

    }
}
