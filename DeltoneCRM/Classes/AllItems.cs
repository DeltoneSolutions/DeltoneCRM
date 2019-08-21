using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltoneCRM.Classes
{
    public class AllItems
    {
        public int ItemID { get; set; }
        public String SupplierName { get; set; }
        public String ProductCode { get; set; }
        public String Description { get; set; }
        public String PrinterCompatibility { get; set; }
        public float COG { get; set; }
        public float ManagerUnitPrice { get; set; }
        public String Active { get; set; }
        public int Qty { get; set; }
        public String ViewEdit { get; set; }
        public float DSB { get; set; }
    }
}