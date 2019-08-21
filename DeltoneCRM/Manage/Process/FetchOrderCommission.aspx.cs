using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class FetchOrderCommission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String itemId = Request["itemId"].ToString();
            Response.Write(ReturnAllItems(itemId));
        }

        protected string ReturnAllItems(string orderId)
        {
            String strOutput = "{\"aaData\":[";

            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            OrderDAL repDayOffDal = new OrderDAL(connString);

            var targetConfigs = repDayOffDal.GetOrderCommissionLIst(orderId);

            foreach (var item in targetConfigs)
            {
                //String strEdit = "<img src='../../Images/Edit.png'  onclick='Delete(" + item.Id + ")'/>";
                strOutput = strOutput + "[\"" + item.CommId + "\","
                      + "\"" + item.UserId + "\","
                      + "\"" + item.RepName + "\","
                        + "\"" + item.Amount + "\","

                     + "\"" + item.OrderId + "\"],";
            }

            if (targetConfigs.Count() > 0)
            {
                int Length = strOutput.Length;
                strOutput = strOutput.Substring(0, (Length - 1));
            }
            strOutput = strOutput + "]}";



            return strOutput;
        }
    }
}