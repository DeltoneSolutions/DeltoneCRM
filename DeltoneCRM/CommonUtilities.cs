using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltoneCRM
{
    public class CommonUtilities
    {



        public enum TableName
        {
            Company = 1,
            Order = 2,
            Quotes = 3

        }

        public enum ActionType
        {
            Created = 1,
            Updated = 2,
            Deleted = 3
        }
    }
}