using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltoneCRM.Classes
{
    public class QuoteItems
    {
        public String ItemDescription { get; set; }
        public float Quantity { get; set; }
        public float UnitPrice { get; set; }
        public float Total { get; set; }

    }
}