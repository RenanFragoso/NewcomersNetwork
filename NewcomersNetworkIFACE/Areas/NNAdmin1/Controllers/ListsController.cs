using System.Collections.Generic;
using System.Web.Mvc;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkAPI.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    public class ListsController : NNAPIController
    {
        public Lists oLists = new Lists();
        
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }

        #region Services Group

        [HttpGet]
        public ActionResult GetGroupIconList()
        {
            OptionsLists oList = this.oLists.getSelectList("servicesgroupicon");
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oList.oOptions
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetGroupIcons()
        {
            OptionsLists oList = this.oLists.getSelectList("groupicons");
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oList.oOptions
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetGroupColors()
        {
            OptionsLists oList = this.oLists.getSelectList("groupcolors");
            return Json(new
            {
                success = true,
                statuscode = 200,
                response = "",
                odata = oList.oOptions
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
