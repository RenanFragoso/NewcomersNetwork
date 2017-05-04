using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkIFACE.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Controllers
{
    [NNLoginPersistence]
    public class ClassifiedsController : NNAPIController
    {
        protected Classifieds oClassifieds = new Classifieds();

        #region VIEWS

        public ActionResult Index()
        {
            base.VerifyCredential();

            //Get the "classified" group Select Lists
            oClassifieds.loadListsGroup("classified");
            return View();
        }

        #endregion
        
        [HttpPost]
        public ActionResult GetClassifieds(NNClassifiedsFilter oFilter)
        {

            if (ModelState.IsValid)
            {
                oClassifieds.LoadFiltered(oFilter);
            }

            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oClassifieds.oClassifieds
            }, JsonRequestBehavior.AllowGet);
        }
        
    }
}
