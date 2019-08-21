using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeltoneCRM;
using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Collections;
using XeroApi.Model;
using XeroApi.Model.Reporting;
using XeroApi;
using XeroApi.Model;
using XeroApi.OAuth;
using System.Security.Cryptography.X509Certificates;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth.Signing;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Core;
using Xero.Api.Example.Applications;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Example.Applications.Public;
//using Xero.Api.Example.TokenStores;
using Xero.Api.Serialization;


namespace Deltonecrm_UnitTest
{
    [TestClass]
    public class XeroTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            
        } 

        /// <summary>
        ///  FindXeroGuidGiveby Company name 
        /// </summary>
        [TestMethod ]
        public void FindByName() 
        {
            XeroIntergration xero = new XeroIntergration();
            Repository repos = xero.CreateRepository();

            String Guid = "4dc4e979-11e5-4994-a6a9-912c102d3a13";// Expected value
            //String companyname = "REGIONAL SERVICES AUSTRALIA PTY LTD";
            String companyname = "Ausjet";
           string ActualValue= xero.findContactByName(repos,companyname); //Actual Value

           Assert.AreEqual(Guid, ActualValue);

        }
        /// <summary>
        /// Create Invoice UnitTest
        /// </summary>
        [TestMethod]
        public void CreateInvoice()
        {

        }

        /// <summary>
        /// Update Invoice UnitTest
        /// </summary>
        [TestMethod]
        public void UpdateInvoice()
        {
        }

        /// <summary>
        /// Delete Invoice UnitTest
        /// </summary>
        [TestMethod]
        public void DeleteInvoice()
        {

        }



        
    }
}
