using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;
namespace DeltoneCRM_DAL
{
    public class CallNDataHandler
    {
        //private String CONNSTRING;

        //public CallNDataHandler(String connString)
        //{
        //    CONNSTRING = connString;
        //}

        public List<CallNResponse> GetCallNData(CallNRequest obj)
        {
            //  var serviceUrl = "https://api.calln.com/api/call?datetimemin=2017-05-01&datetimemax=2017-06-01";
            var serviceUrl = "https://api.calln.com/api/call?datetimemin={0}&datetimemax={1}&maxresults=1000";
            // var urlMethod = "call?datetimemin={0}&datetimemax={1}";
            var username = "info@deltonesolutions.com.au@deltonesolutions.calln.com"; // username info@deltonesolutions.com.au and @ company name deltonesolutions.calln.com
            var userPassword = "Deltone123";



            if (obj.IsDateFilterApplied)
                serviceUrl = "https://api.calln.com/api/call?datetimemin={0}&datetimemax={1}&maxresults=1000&callername=" + obj.RepName;
            serviceUrl = string.Format(serviceUrl, obj.StartDate, obj.EndDate);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
                string.Format("{0}:{1}", username, userPassword))));

            client.BaseAddress = new Uri(serviceUrl);
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            return CallResult(serviceUrl, client);

        }

        public IList<CallNResponse> CallRequestByDestinationNumber(string destinatioNumber)
        {
            var listres = new List<CallNResponse>();
            ILog _logger = LogManager.GetLogger(typeof(CallNDataHandler));


            var serviceUrl = "https://api.calln.com/api/call?destinationphonenumber={0}&maxresults=1000";
            serviceUrl = string.Format(serviceUrl, destinatioNumber);
            var userName="info@deltonesolutions.com.au@deltonesolutions.calln.com";
            var password="Deltone123";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",userName,password))));
            client.BaseAddress = new Uri(serviceUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {

                var response = client.GetAsync(serviceUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    listres = JsonConvert.DeserializeObject<List<CallNResponse>>(jsonString);

                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error Calln API from company info page." + ex);
                return listres;
            }


            return listres;


        }


        private List<CallNResponse> CallResult(string serviceUrl, HttpClient client)
        {
            var listObj = new List<CallNResponse>();
            ILog _logger = LogManager.GetLogger(typeof(CallNDataHandler));

            
            try
            {
                var response = client.GetAsync(serviceUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    //   jsonString.Wait();
                    listObj = JsonConvert.DeserializeObject<List<CallNResponse>>(jsonString);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Error occurred while accessing CALLN API" + ex);
                return listObj;
            }

            return listObj;
        }

        public void TestFileDownload()
        {
            //  var serviceUrl = "https://api.calln.com/api/call?datetimemin=2017-05-01&datetimemax=2017-06-01";
            var serviceUrl = "https://api.calln.com/api/call?datetimemin={0}&datetimemax={1}&maxresults=1000";
            // var urlMethod = "call?datetimemin={0}&datetimemax={1}";
            var username = "info@deltonesolutions.com.au@deltonesolutions.calln.com"; // username info@deltonesolutions.com.au and @ company name deltonesolutions.calln.com
            var userPassword = "Deltone123";



          
                serviceUrl = "https://api.calln.com/api/callaudio/78580349";
           

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue(
        "Basic",
        Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
                string.Format("{0}:{1}", username, userPassword))));

            client.BaseAddress = new Uri(serviceUrl);
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("audio/mpeg"));//ACCEPT header

            try
            {
                var response = client.GetAsync(serviceUrl).Result;
                int total=0;
                var dateTic = DateTime.Now.Ticks;
                var filll = "testmm" + dateTic + ".mp3";
                if (response.IsSuccessStatusCode)
                {
                  //  var jsonString = response.Content.ReadAsStreamAsync().Result;
                  //  var fileName = "c:/temp/" + filll;
                  //var  fs =  new FileStream(fileName, FileMode.Create);

                  //  byte[] buffer = new byte[4096];

                  //  while (jsonString.CanRead)
                  //  {
                  //      Array.Clear(buffer, 0, buffer.Length);
                  //      total += jsonString.Read(buffer, 0, buffer.Length);
                  //      fs.Write(buffer, 0, buffer.Length);
                  //  }

                    //   jsonString.Wait();
                   
                }
            }
            catch (Exception ex)
            {
                //ogger.Error("Error occurred while accessing CALLN API" + ex);
               // return listObj;
            }
        }

        private IList<string> GetUserNames()
        {
            var listUsers = new List<string>();

            return listUsers;
        }
    }

    public class CallNRequest
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool IsDateFilterApplied { get; set; }
        public string RepName { get; set; }
    }

    public class CallNResponse
    {
        public int id { get; set; }
        public string startdatetime { get; set; }
        public string startdatetimeutc { get; set; }
        public int answerdelayms { get; set; }
        public int durationseconds { get; set; }
        public string directiontype { get; set; }
        public string callerphonenumber { get; set; }
        public string callername { get; set; }
        public string destinationphonenumber { get; set; }
        public string subject { get; set; }
        public string notes { get; set; }
        public string rating { get; set; }
        public string audiomd5hash { get; set; }
        public string audiolengthbytes { get; set; }
        public string[] tags { get; set; }
    }
}
