using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltoneCRM.Classes
{
    public class ApprovedOrders
    {
        public String OrderNo { get; set; }
        public String CompanyName { get; set; }
        public String ContactPerson { get; set; }
        public String InvNumber { get; set; }
        public float OrderTotal { get; set; }
        public String OrderedDate { get; set; }
        public String DueDate { get; set; }
        public String CreatedBy { get; set; }
        public String Status { get; set; }
        public String ViewEdit { get; set; }
        public String SupplierNotes { get; set; }

        public string CreatedDate { get; set; }
        public string CanFlag { get; set; }
    }

    public class RMATRACKINGUI
    {
        public string RMAID { get; set; }
        public string CreditNoteID { get; set; }
        public string RaisedDateTime { get; set; }
        public string SentToSupplier { get; set; }
        public string SentToSupplierDatetime { get; set; }
        public string ApprovedRMA { get; set; }
        public string ApprovedRMADateTime { get; set; }
        public string CreditInXero { get; set; }
        public string CreditInXeroDateTime { get; set; }
        public string RMAToCustomer { get; set; }
        public string RMAToCustomerDateTime { get; set; }
        public string AdjustedNoteFromSupplier { get; set; }
        public string AdjustedNoteFromSupplierDateTime { get; set; }
        public string Status { get; set; }
        public string SupplierName { get; set; }
        public string SupplierRMANumber { get; set; }
        public string TrackingNumber { get; set; }
        public string InHouse { get; set; }
        public string BatchNumber { get; set; }
        public string ModelNumber { get; set; }
        public string ErrorMessage { get; set; }
        public string ViewEdit { get; set; }

    }
}