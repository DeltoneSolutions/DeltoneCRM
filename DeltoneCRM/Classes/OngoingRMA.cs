using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltoneCRM.Classes
{
    public class OngoingRMA
    {
        public int CreditNoteID { get; set; }
        public String XeroCreditNote { get; set; }
        public String SupplierName { get; set; }
        public String Raised { get; set; }
        public String RaisedDateTime { get; set; }
        public String SentToSupplier { get; set; }
        public String SentToSupplierDateTime { get; set; }
        public String ApprovedRMA { get; set; }
        public String ApprovedRMADateTime { get; set; }
        public String CreditInXero { get; set; }
        public String CreditInXeroDateTime { get; set; }
        public String RMAToCustomer { get; set; }
        public String RMAToCustomerDateTime { get; set; }
        public String AdjustedNoteFromSupplier { get; set; }
        public String AdjustedNoteFromSupplierDateTime { get; set; }
        public String SupplierRMANumber { get; set; }
        public String TrackingNumber { get; set; }
        public String Status { get; set; }
        public String ViewEdit { get; set; }
        public string InHouse { get; set; }
    }
}