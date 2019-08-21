using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class UploadQuoteImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var comId = Request.QueryString["comId"];
            var qId = Request.QueryString["quoteId"];
            Response.Write(UploadFile(sender, e, comId, qId));
        }

        protected string UploadFile(object sender, EventArgs e, string comID, string qId)
        {
            var listImageName = new List<string>();
            if (Session["iamgeQuotes"] != null)
                listImageName = Session["iamgeQuotes"] as List<string>;
            HttpFileCollection fileCollection = Request.Files;
            var fileNamecutom = "";
            if (Request["namechange"] != null)
                fileNamecutom = Request["namechange"].ToString();
            if (fileNamecutom == "null")
                fileNamecutom = "";
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile upload = fileCollection[i];

                var filDirctory = Server.MapPath("~/CompanyUploads/Quotes/Temp/" + Session["LoggedUserID"].ToString() + "/");
                if (!string.IsNullOrEmpty(qId))
                {
                    filDirctory = Server.MapPath("~/CompanyUploads/Quotes/" + comID + "/" + qId + "/");
                    Session["quoteIdused"] = "yes";
                }

                if (!Directory.Exists(filDirctory))
                {
                    Directory.CreateDirectory(filDirctory);
                }

                var domainNameWithFile = "http://localhost:65085//CompanyUploads/Quotes/Temp/" + Session["LoggedUserID"].ToString() + "/";
                var domainNameWithFileServer = "http://delcrm//CompanyUploads/CreditsNotes/" + Session["LoggedUserID"].ToString() + "/";
                string fileName = Path.GetFileName(upload.FileName);
                if (fileNamecutom != "")
                {
                    var fileExtension = Path.GetExtension(upload.FileName);
                    var fileextensionContain = Path.GetExtension(fileNamecutom);
                    if (fileextensionContain != "")
                        fileName = fileNamecutom;
                    else
                        fileName = fileNamecutom + fileExtension;
                }
                listImageName.Add(fileName);
                if (string.IsNullOrEmpty(qId))
                {
                    Session["iamgeQuotes"] = listImageName;
                }
                upload.SaveAs(filDirctory + fileName);

               // var imUrl = "http://localhost:65085//CompanyUploads/Quotes/" + comID + "/" + qId + "/" + fileName;

                return "OK";
            }

            return string.Empty;
        }

    }
}