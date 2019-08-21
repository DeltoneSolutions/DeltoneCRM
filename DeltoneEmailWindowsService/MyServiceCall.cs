using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DeltoneEmailWindowsService
{
 public   class MyServiceCall
    {

     public void WriteLog(string message)
     {
         StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\logfile.txt", true);
         sw.WriteLine(DateTime.Now.ToString() + ":" + message);
         sw.Flush();
         sw.Close();
     }
    }
}
