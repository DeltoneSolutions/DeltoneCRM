using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltoneCRM
{
    public class QuoteItem
    {
        String description { get; set; }
        String ItemCode { get; set; }
        float UnitAmout { get; set; }
        int Quantity { get; set; }
        float COGAmount { get; set; }
        String SupplierItemCode { get; set; }
        String SupplierName { get; set; }

    }
}