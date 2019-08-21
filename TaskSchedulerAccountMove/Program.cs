using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskSchedulerAccountMove
{
    class Program
    {
        static void Main(string[] args)
        {
            RunMoveAccoutnHouse();
            Console.ReadKey();
        }

        public static void RunMoveAccoutnHouse(){
            ILog _logger = LogManager.GetLogger(typeof(Program));
            _logger.Info("scheduler started");
            WriteErrorLog("scheduler started");
            try
            {
                var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                new AccountMoveHouseDAL(connString).MoveRepAccounttoHouseAccount();
            }
            catch(Exception ex)
            {
                _logger.Error("Error occurred at TaskSchedulerAccountMove method : RunMoveAccoutnHouse" + ex);
            }

        }

        public static void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
    }
}
