using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;
using NewcomersNetworkIFACE.Client;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web.Helpers;
using NewcomersNetworkIFACE.Filters;
using NewcomersNetworkIFACE.Util;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    [RoutePrefix("NNAdmin1/Classifieds")]
    public class ClassifiedsController : NNAPIController
    {
        public Classifieds oClassifieds { get; set; } = new Classifieds();

        #region VIEWS

        // GET: NNAdmin/Classifieds
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NeedsPending()
        {
            string NNAdmin_Token = "";
            string NNAdmin_CookieToken = "";
            AntiForgery.GetTokens(null, out NNAdmin_CookieToken, out NNAdmin_Token);
            ViewBag.NNAdmin_Token = NNAdmin_Token;
            Response.Cookies[AntiForgeryConfig.CookieName].Value = NNAdmin_CookieToken;
            return View();
        }

        public ActionResult HelpPending()
        {
            return View();
        }

        #endregion

        #region CLASSIFIEDS

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        [NNAuthorize]
        public ActionResult TestToken()
        {
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = ""
            }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult GetClassified(string cClassifiedId)
        {
            if (cClassifiedId != null && cClassifiedId.Length > 0)
            {
                this.oClassifieds.GetClassified(cClassifiedId);
                return Json(new
                {
                    success = true,
                    statuscode = 200,
                    response = "",
                    odata = this.oClassifieds.oClassifieds
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPending()
        {
            this.oClassifieds.GetPending();
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = this.oClassifieds.oPending
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PendingNum()
        {
            int nPending = this.oClassifieds.GetPendingNum();
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = (nPending > 0) ? nPending.ToString() : ""
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetNewClassified()
        {
            Classified oClassified = new Classified();
            oClassified.Id = "NULLID";
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oClassified
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateClassified(Classified oClassified)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oClassifieds.UpdateClassified(oClassified);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> ApproveClassified(string cClassifiedId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oClassifieds.ApproveClassified(cClassifiedId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> RejectClassified(string cClassifiedId)
        {
            HttpResponseMessage oResponse;
            if (ModelState.IsValid)
            {
                oResponse = this.oClassifieds.RejectClassified(cClassifiedId);
                string cMessage = await oResponse.Content.ReadAsStringAsync();
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = cMessage
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Bad Request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region CLASSIFIED EVENTS

        [HttpGet]
        public ActionResult GetNewClassifiedEvent(string cClassifiedId)
        {
            ClassifiedEvents oClsEvt = new ClassifiedEvents();
            if(cClassifiedId != null && cClassifiedId.Length > 0)
            {
                oClsEvt.ClassifiedId = cClassifiedId;
            }
            else
            {
                oClsEvt.ClassifiedId = "NULLID";
            }

            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oClsEvt
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}