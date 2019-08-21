using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DeltoneCRM
{
    public class DeltoneItem
    {
        /* DELTONE CRM  ITEM  Bussines Object*/
        public String ItemDescription { get; set; }
        public String SupplierName { get; set; }
        public String SupplierCode { get; set; }
        public decimal COG { get; set; }
        public int  Qty{get;set;}
        public decimal UnitPrice { get; set; }
    }
}