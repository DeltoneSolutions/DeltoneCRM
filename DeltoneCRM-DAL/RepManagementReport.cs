using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
   public class RepManagementReport
    {
       
    }

   public class RepManageReport
   {
      
       public Dictionary<string,string> DatesAndCommision { get; set; }
   }

   public class CommByRep
   {
      // public string
   }

  

   public class MainObject
   {
       public List<string> Date { get; set; }

      
           public List<string> Rep1Commission { get; set; }
           public List<string> Rep2Commission { get; set; }

           public List<string> Rep3Commission { get; set; }
     
   }


   public class Employee
   {
       public int ID { get; set; }
       public string Name { get; set; }
       public int Age { get; set; }
   }
   public class CustomDS
   {
       private static List<Employee> _lstEmployee = null;
       public static List<Employee> GetAllEmployees()
       {
           if (_lstEmployee == null)
           {
               _lstEmployee = new List<Employee>();
               _lstEmployee.Add(new Employee()
               {
                   ID = 1,
                   Name = "Alok",
                   Age = 30
               });

               _lstEmployee.Add(new Employee()
               {
                   ID = 2,
                   Name = "Ashish",
                   Age = 30
               });

               _lstEmployee.Add(new Employee()
               {
                   ID = 3,
                   Name = "Jasdeep",
                   Age = 30
               });

               _lstEmployee.Add(new Employee()
               {
                   ID = 4,
                   Name = "Kamlesh",
                   Age = 31
               });
           }
           return _lstEmployee;
       }


   }

   public class CustomRepDS
   {
       private static MainObject _lstEmployee = null;
       public static MainObject GetAllEmployees()
       {
           if (_lstEmployee == null)
           {
               _lstEmployee = new MainObject();
               _lstEmployee.Date = new List<string>();
               _lstEmployee.Date.Add("Dec-03");
               _lstEmployee.Date.Add("Dec-04");
               _lstEmployee.Date.Add("Dec-05");
               _lstEmployee.Date.Add("Dec-06");

               _lstEmployee.Rep1Commission = new List<string>();
               _lstEmployee.Rep1Commission.Add("300");
               _lstEmployee.Rep1Commission.Add("400");
               _lstEmployee.Rep1Commission.Add("600");
               _lstEmployee.Rep1Commission.Add("700");

               _lstEmployee.Rep2Commission = new List<string>();
               _lstEmployee.Rep2Commission.Add("300");
               _lstEmployee.Rep2Commission.Add("400");
               _lstEmployee.Rep2Commission.Add("600");
               _lstEmployee.Rep2Commission.Add("700");

               _lstEmployee.Rep3Commission = new List<string>();
               _lstEmployee.Rep3Commission.Add("300");
               _lstEmployee.Rep3Commission.Add("600");
               _lstEmployee.Rep3Commission.Add("500");
               _lstEmployee.Rep3Commission.Add("700");
              
           }
           return _lstEmployee;
       }

       

   }
}
