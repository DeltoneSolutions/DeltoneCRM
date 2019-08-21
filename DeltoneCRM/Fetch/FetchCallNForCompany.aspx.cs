using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchCallNForCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string comId = Request.QueryString["companyid"].ToString();
            Response.Write(ReturnCallnDataForCompany(comId));
        }

        protected string ReturnCallnDataForCompany(string comId)
        {
            var strOuput = "{\"aaData\":[";

            var callData = new CallNDataHandler();
            var isrec = false;
            var listPhoneNumnbers = GetContactByCompanyId(comId);
            foreach (var item in listPhoneNumnbers)
            {
                var phoneNumber = Regex.Replace(item, @"\s+", "");
                var resultCallNForCompanyList = callData.CallRequestByDestinationNumber(phoneNumber);
                foreach (var itemcallN in resultCallNForCompanyList)
                {
                    isrec = true;
                    decimal durationInMin = 0;
                    //if (itemcallN.durationseconds > 0)
                    //    durationInMin=itemcallN.durationseconds /60;
                    //var minDuration = String.Format("{0:0.00}", durationInMin); 
                    
                       
                    strOuput = strOuput + "[\"" + itemcallN.callername + "\","
                        + "\"" + itemcallN.destinationphonenumber + "\","
                         + "\"" + Convert.ToDateTime(itemcallN.startdatetime).ToString("dd-MMM-yyyy hh:mm:ss") + "\","
                         + "\"" + itemcallN.durationseconds + "\"],";
                       

                }


            }
            if (isrec)
            {
                int length = strOuput.Length;
                strOuput = strOuput.Substring(0,(length-1));
            }
            strOuput = strOuput  +"]}";
            return strOuput;
                
        }



        protected List<string> GetContactByCompanyId(string comId)
        {
            var listNumber=new List<string>();
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var contactDAL = new ContactDAL(conn);

            var contatList = contactDAL.GetContatByCompanyId(comId);
            foreach (var item in contatList)
            {
                if (!string.IsNullOrEmpty(item.DEFAULT_AreaCode) && !string.IsNullOrEmpty(item.DEFAULT_Number))
                {
                    if (item.DEFAULT_AreaCode.Contains('('))
                        item.DEFAULT_AreaCode = item.DEFAULT_AreaCode.Replace("(","");
                    if (item.DEFAULT_AreaCode.Contains(')'))
                        item.DEFAULT_AreaCode = item.DEFAULT_AreaCode.Replace(")","");

                    if (item.DEFAULT_Number.Contains("("))
                        item.DEFAULT_Number = item.DEFAULT_Number.Replace("(","");
                    if (item.DEFAULT_Number.Contains(")"))
                        item.DEFAULT_Number = item.DEFAULT_Number.Replace(")","");

                    if (item.DEFAULT_Number.Contains("-"))
                        item.DEFAULT_Number = item.DEFAULT_Number.Replace("-", "");


                    var phoneNumber = item.DEFAULT_AreaCode + item.DEFAULT_Number;
                   
                    listNumber.Add(phoneNumber);
                }

                else
                {
                    if (string.IsNullOrEmpty(item.DEFAULT_AreaCode) && !string.IsNullOrEmpty(item.DEFAULT_Number))
                    {
                        if (item.DEFAULT_Number.Contains('('))
                            item.DEFAULT_Number = item.DEFAULT_Number.Replace("(","");
                        if (item.DEFAULT_Number.Contains(')'))
                            item.DEFAULT_Number = item.DEFAULT_Number.Replace(")","");
                        if (item.DEFAULT_Number.Contains("-"))
                            item.DEFAULT_Number = item.DEFAULT_Number.Replace("-","");

                        var phoneNumber = item.DEFAULT_Number;
                        listNumber.Add(phoneNumber);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.DEFAULT_AreaCode) && string.IsNullOrEmpty(item.DEFAULT_Number))
                        {
                            if (item.DEFAULT_AreaCode.Contains('('))
                                item.DEFAULT_AreaCode = item.DEFAULT_AreaCode.Replace("(", "");
                            if (item.DEFAULT_AreaCode.Contains(")"))
                                item.DEFAULT_AreaCode = item.DEFAULT_AreaCode.Replace(")","");
                            var phoneNumber = item.DEFAULT_AreaCode;
                            listNumber.Add(phoneNumber);

                        }
                    }
                }


                if (!string.IsNullOrEmpty(item.MOBILE_Number))
                {
                    listNumber.Add(item.MOBILE_Number);
                }
            }

            return listNumber;
        }
    }
}