using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltoneCRM.Classes
{
    public class LiveDBQuotes
    {
        public int QuoteID { get; set; }
        public String QuoteDate { get; set; }
        public String CompanyName { get; set; }
        public String ContactName { get; set; }
        public String QuoteTotal { get; set; }
        public String QuoteStatus { get; set; }
        public String View { get; set; }
        public String QuotedBy { get; set; }
        public String CustomerType { get; set; }
    }
}