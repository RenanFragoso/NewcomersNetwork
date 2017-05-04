using NewcomersNetworkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Client
{
    public class NNAPIController : Controller
    {
        protected NNAPIClient oNNAPIClient = new NNAPIClient();

        public virtual void setNNAPIToken(string cToken)
        {
            this.oNNAPIClient.setToken(cToken);
        }

        protected virtual void VerifyCredential()
        {
            ViewBag.SessionUser = Session["SessionUser"];
        }
    }
}