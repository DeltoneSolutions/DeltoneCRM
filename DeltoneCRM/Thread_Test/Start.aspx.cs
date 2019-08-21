using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;

namespace DeltoneCRM.Thread_Test
{
    public partial class Start : System.Web.UI.Page
    {
        protected Guid id;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnStart_Click(object sender, EventArgs e)
        {
            // assign an unique ID
            id = Guid.NewGuid();
            // start a new thread
            ThreadStart ts = new ThreadStart(LongRunningProcess);
            Thread th = new Thread(ts);
            th.Start();
            // redirect to waiting page
            Response.Redirect("Status.aspx?ID=" + id.ToString());
        }

        // this is a stub for a asynchronous process
        protected void LongRunningProcess()
        {
            // do nothing actually, but there should be real code
            // for instance, there could be a call for a remote web service
            Thread.Sleep(9000);
            // add result to the controller
            SimpleProcessCollection.Add(id, "Some result.");
        }

    }
}