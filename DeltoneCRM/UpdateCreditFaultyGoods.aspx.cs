using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class UpdateCreditFaultyGoods : System.Web.UI.Page
    {
        protected string str_AccountID { get; set; }
        protected string str_CreditId { get; set; }
        protected string str_SuppName { get; set; }
        static String connString;
        CreditNotesDAL creditnotedal;
        protected void Page_Load(object sender, EventArgs e)
        {
            connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            if (Session["LoggedUserID"] == null)
                return;
            creditnotedal = new CreditNotesDAL(connString);
            if (Request["ordID"] != null)
            {
                str_AccountID = Request["ordID"].ToString();

            }
            if (Request["crID"] != null)
                str_CreditId = Request["crID"].ToString();

            if (Request["suNa"] != null)
                str_SuppName = Request["suNa"].ToString();

            if (IsPostBack)
            {
                UploadFile(sender, e);
            }
            if (!IsPostBack)
            {
                var allSupp = creditnotedal.GetSuppliersForCreditNoteById(str_CreditId);
                if (!IsPostBack)
                {
                    foreach (var item in allSupp)
                        suppliernameDrop.Items.Add(new System.Web.UI.WebControls.ListItem(item, item));
                }
            }

            LoadUploadedFiles();


            

           


            // var ttt= string.Format("openTab({0});", "test");
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("dashboard1.aspx");



        }



        protected void UploadFile(object sender, EventArgs e)
        {

            HttpFileCollection fileCollection = Request.Files;
            var fileNamecutom = "";
            if (Request["namechange"] != null)
                fileNamecutom = Request["namechange"].ToString();
            if (fileNamecutom == "null")
                fileNamecutom = "";
            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile upload = fileCollection[i];

                var filDirctory = Server.MapPath("~/CompanyUploads/CreditsNotes/" + str_AccountID + "/" + str_CreditId + "/" + str_SuppName + "/");

                if (!Directory.Exists(filDirctory))
                {
                    Directory.CreateDirectory(filDirctory);
                }


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
                upload.SaveAs(filDirctory + fileName);


            }
        }

        protected void callFileLoad(object sender, EventArgs e)
        {
            LoadUploadedFiles();
        }


        protected void LoadUploadedFiles()
        {
            if (string.IsNullOrEmpty(str_SuppName))
                str_SuppName = suppliernameDrop.SelectedItem.Value;

            if (Directory.Exists(Server.MapPath("~/CompanyUploads/CreditsNotes/" + str_AccountID + "/" + str_CreditId + "/" + str_SuppName )))
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/CompanyUploads/CreditsNotes/" + str_AccountID + "/" + str_CreditId + "/" + str_SuppName ));
                //new DirectoryInfo(@"D:\samples").GetFiles()
                //                              .OrderBy(f => f.LastWriteTime)
                //                              .ToList();
                List<System.Web.UI.WebControls.ListItem> files = new List<System.Web.UI.WebControls.ListItem>();
                DateTime[] creationTimes = new DateTime[filePaths.Length];
                for (int i = 0; i < filePaths.Length; i++)
                    creationTimes[i] = new FileInfo(filePaths[i]).CreationTime;
                Array.Sort(creationTimes, filePaths);
                foreach (string filePath in filePaths)
                {

                    files.Add(new System.Web.UI.WebControls.ListItem(Path.GetFileName(filePath), filePath));
                }
                GridView1.DataSource = files;
                GridView1.DataBind();
            }

            SetFaultyElemants(str_CreditId, str_SuppName);
        }

        private void SetFaultyElemants(string creditId,string suppName )
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var rma = new CreditNoteRMAFaultyGoods();
            rma.CreditNoteId = creditId;
            rma.SupplierName = suppName;
            rma = new CreditNoteRMAHandler(cs).GetRMAFaultyGoods(rma);

            batchnumberfaulty.Value = rma.BatchNumber;
            modelnumberfaulty.Value = rma.ModelNumber;
            errormessagefaulty.Value = rma.ErrorMessage;
            faultyNotes.Value = rma.FaultyNotes;

        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }



        protected void OpenTab(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            var filePathOri = Path.GetFileName(filePath);

            var domainNameWithFile = "http://localhost:65085//CompanyUploads/CreditsNotes/";
            var domainNameWithFileServer = "http://delcrm//CompanyUploads/CreditsNotes/";

            var filePathWithDomain = domainNameWithFileServer + str_AccountID + "/" + str_CreditId + "/" + str_SuppName + "/" + filePathOri;


            string strJS = ("<script type='text/javascript'>window.open('" + filePathWithDomain + "','_blank');</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "strJSAlert", strJS);

            //     var blank="_blank";
            //     string newWin =

            //"window.open('" + lastPath + "','" + blank + "');";
            //     Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", newWin, true);
        }






        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            System.IO.File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        private void SetBkColor()
        {
            GridViewRowCollection rows = GridView1.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                //set the background color for the 2nd row of Gridview
                if (i % 2 == 0)
                {
                    rows[i].BackColor = ColorTranslator.FromHtml("#C0C0C0");
                }
                else
                {
                    rows[i].BackColor = ColorTranslator.FromHtml("#D3D3D3");
                }
            }
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            SetBkColor();
        }
    }
}