using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltoneCRM
{  
    /// <summary>
    /// Contact Object Created for the Reporting Purpose
    /// </summary>
    public class DelReport_Contact
    {
        public String FirstName {get;set;}
        public String LastName { get; set;}
        public String DepartmentId { get; set; }
        public int LoginId { get; set; }
    }
}