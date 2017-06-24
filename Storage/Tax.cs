using System;
using System.Collections.Generic;
using System.Text;

namespace Storage
{
    public class Tax
    {
        public long bottomRange { get; set; }
        public long upperRange { get; set; }
        public long BaseTax { get; set; }
        public decimal variableTax { get; set; }
    }
}
