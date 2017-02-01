using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Areas.NNAdmin1.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Controllers
{
    [RoutePrefix("NNAdmin1/Users")]
    public class UsersController : Controller
    {

        public Users oUsers = new Users();
        
        // GET: NNAdmin1/Users
        public ActionResult Index()
        {
            this.oUsers.LoadUsers();
            ListPopulate(null);
            return View(oUsers);
        }

        /* Edit selector */
        [Route("EditUserView/{cUserId}")]
        [HttpGet]
        public ActionResult EditUserView(string cUserId)
        {
            User oNewUser = new User { Id = "NULLID", UserName = "NULLNAME" };
            var oUserEdit = oNewUser;

            if (cUserId != null && cUserId.Length > 0)
            {
                //Edit user
                oUserEdit = this.oUsers.oUserList.Find(
                    delegate (User usr) {
                        return usr.Id == cUserId;
                    });

                if(oUserEdit == null)
                {
                    oUserEdit = oNewUser;
                }

            }

            this.ListPopulate(oUserEdit);
            return PartialView("_EditUserForm", oUserEdit);
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            List<User> oUsers;
            oUsers = this.oUsers.GetAllUsers();
            if(oUsers != null)
            {
                return Json(oUsers, JsonRequestBehavior.AllowGet);
            }
            oUsers = new List<User>();
            return Json(oUsers, JsonRequestBehavior.AllowGet);
        }

        /*
        [HttpGet]
        public ActionResult GetUsers()
        {

            return PartialView("_UsersList", this.oUsers.oUserList);
        }
        */

        /* Edit sender */
        [HttpPost]
        public async Task<ActionResult> EditUser(User oUser)
        {
            HttpResponseMessage oResponse = null;

            if (ModelState.IsValid)
            {
                oResponse = oUsers.UpdateUser(oUser);

                //return Json(oResponse, JsonRequestBehavior.AllowGet);
                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = (int)oResponse.StatusCode,
                    response = await oResponse.Content.ReadAsStringAsync() }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    success = false,
                    statuscode = 400,
                    response = "{ \"Message\": \"" + string.Join("; ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)) + "\"}"
                }, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpGet]
        public ActionResult UsersList()
        {
            ListPopulate(null);
            return PartialView("_UsersList", this.oUsers.oUserList);
        }
        
        [HttpDelete]
        public async Task<ActionResult> RemoveUser(string cUserId)
        {
            HttpResponseMessage oResponse = null;
            User oUserRemove;

            if (ModelState.IsValid)
            {

                if (cUserId != null && cUserId.Length > 0)
                {
                    //Edit user
                    oUserRemove = this.oUsers.oUserList.Find(
                                    delegate (User usr)
                                    {
                                        return usr.Id == cUserId;
                                    });

                    if (oUserRemove != null)
                    {
                        oResponse = oUsers.RemoveUser(oUserRemove.Id);
                        string cMessage = await oResponse.Content.ReadAsStringAsync();
                        if (cMessage != null && cMessage.Length == 0)
                        {
                            cMessage = "User removed succefully.";
                        }

                        return Json(new
                        {
                            success = oResponse.IsSuccessStatusCode,
                            statuscode = (int)oResponse.StatusCode,
                            response = "{ \"Message\": \"" + cMessage + "\"}"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            statuscode = 400,
                            response = "{ \"Message\": \"User not found.\"}"
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Malformed request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public async Task<ActionResult> BlockUser(string cUserId)
        {
            HttpResponseMessage oResponse = null;
            User oUserBlock;

            if (ModelState.IsValid)
            {

                if (cUserId != null && cUserId.Length > 0)
                {
                    //Edit user
                    oUserBlock = this.oUsers.oUserList.Find(usr => usr.Id == cUserId);

                    if (oUserBlock != null)
                    {
                        oResponse = oUsers.BlockUser(oUserBlock.Id);
                        string cMessage = await oResponse.Content.ReadAsStringAsync();
                        if (cMessage != null && cMessage.Length == 0)
                        {
                            cMessage = "User Blocked succefully.";
                        }

                        return Json(new
                        {
                            success = oResponse.IsSuccessStatusCode,
                            statuscode = (int)oResponse.StatusCode,
                            response = "{ \"Message\": \"" + cMessage + "\"}"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            statuscode = 400,
                            response = "{ \"Message\": \"User not found.\"}"
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Malformed request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> ActivateUser(string cUserId)
        {
            HttpResponseMessage oResponse = null;
            User oUserActivate;

            if (ModelState.IsValid)
            {

                if (cUserId != null && cUserId.Length > 0)
                {
                    //Edit user
                    oUserActivate = this.oUsers.oUserList.Find(usr => usr.Id == cUserId);

                    if (oUserActivate != null)
                    {
                        oResponse = oUsers.BlockUser(oUserActivate.Id);
                        string cMessage = await oResponse.Content.ReadAsStringAsync();
                        if (cMessage != null && cMessage.Length == 0)
                        {
                            cMessage = "User Blocked succefully.";
                        }

                        return Json(new
                        {
                            success = oResponse.IsSuccessStatusCode,
                            statuscode = (int)oResponse.StatusCode,
                            response = "{ \"Message\": \"" + cMessage + "\"}"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            statuscode = 400,
                            response = "{ \"Message\": \"User not found.\"}"
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
            }

            return Json(new
            {
                success = false,
                statuscode = 400,
                response = "{ \"Message\": \"Malformed request.\"}"
            }, JsonRequestBehavior.AllowGet);
        }
        
        protected void ListPopulate(User oSelectedUser)
        {
            ViewBag.oLists = this.oUsers.oLists;
            ViewBag.AgeRange_List = this.oUsers.oLists.getSelectList("AgeRange", oSelectedUser != null ? oSelectedUser.oDetails.AgeRange : null);
            ViewBag.Education_List = this.oUsers.oLists.getSelectList("Education", oSelectedUser != null ? oSelectedUser.oDetails.Education : null );
            ViewBag.Gender_List = this.oUsers.oLists.getSelectList("Gender", oSelectedUser != null ? oSelectedUser.oDetails.Gender : null);
            ViewBag.MaritalStatus_List = this.oUsers.oLists.getSelectList("MaritalStatus", oSelectedUser != null ? oSelectedUser.oDetails.MaritalStatus : null);
        }


    }
}