using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Collections;


namespace DeltoneCRM
{
    public static class SimpleProcessCollection
    {
        /*Controller class  describing Please Wait Messages*/
        private static Hashtable _results = new Hashtable();

        //This Method Fetch the results from the Results Collection
        public static string GetResult(Guid id)
        {
            if (_results.Contains(id))
            {
                return Convert.ToString(_results[id]);
            }
            else
            {
                return String.Empty;
            }
        }

        //Add the Results to the Results Collection
        public static void Add(Guid id, string value)
        {
            _results[id] = value;
        }

        //Remove the Results From the Collection
        public static void Remove(Guid id)
        {
            _results.Remove(id);
        }
    }
}