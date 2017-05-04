using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewcomersNetworkIFACE.Models;
using System.Threading.Tasks;
using NewcomersNetworkIFACE.Filters;
using System.Web.Helpers;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Util;
using System.Configuration;
using System.Net.Http;
using System.IO;

namespace NewcomersNetworkIFACE.Controllers
{
    public class UserProfileController : NNAPIController
    {
        public UserAPI oUserAPI = new UserAPI();

        #region VIEWS

        [NNLoginPersistence]
        [NNAuthorize]
        public ActionResult Index()
        {
            base.VerifyCredential();
            return View();
        }

        public ActionResult Login()
        {
            Login oLogin = new Login();
            return View(oLogin);
        }

        public ActionResult SignIn()
        {
            return View(this.oUserAPI);
        }

        public ActionResult LogOut()
        {
            HttpCookie oCookieTkn;
            HttpCookie oCookieUsr;

            oCookieTkn = Response.Cookies["RememberToken"];
            if(oCookieTkn != null)
            {
                oCookieTkn.Expires = DateTime.Now.AddMinutes(-10);
                oCookieTkn.Value = "";
            }
            oCookieUsr = Response.Cookies["RememberUser"];
            if (oCookieUsr != null)
            {
                oCookieUsr.Expires = DateTime.Now.AddMinutes(-10);
                oCookieUsr.Value = "";
            }

            //Remove Cookies
            Response.Cookies.Clear();
            Response.Cookies.Add(oCookieTkn);
            Response.Cookies.Add(oCookieUsr);

            //Renew Session
            Session.Abandon();

            return RedirectToAction("Index", "Home");
        }

        [NNLoginPersistence]
        [NNAuthorize]
        public ActionResult Edit()
        {
            SessionUser oSessionUsr;
            base.VerifyCredential();

            oSessionUsr = (SessionUser)Session["SessionUser"];
            
            if(oSessionUsr != null)
            {
                this.oUserAPI.setToken(Session["UserToken"].ToString());

                //Get the "user" group Select Lists
                oUserAPI.loadListsGroup("user");
                
                //Get user data
                if (this.oUserAPI.getUserData(oSessionUsr))
                {
                    string NNAF_Token = "";
                    string NNAF_CookieToken = "";
                    AntiForgery.GetTokens(null, out NNAF_CookieToken, out NNAF_Token);
                    ViewBag.NNAF_Token = NNAF_Token;
                    Response.Cookies[AntiForgeryConfig.CookieName].Value = NNAF_CookieToken;
                    return View(this.oUserAPI);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        
        [NNLoginPersistence]
        [NNAuthorize]
        public ActionResult Passwd()
        {
            SessionUser oSessionUsr;
            base.VerifyCredential();

            oSessionUsr = (SessionUser)Session["SessionUser"];

            if (oSessionUsr != null)
            {
                this.oUserAPI.setToken(Session["UserToken"].ToString());

                //Get the "user" group Select Lists
                oUserAPI.loadListsGroup("user");

                //Get user data
                if (this.oUserAPI.getUserData(oSessionUsr))
                {
                    return View(this.oUserAPI);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        
        #endregion

        #region METHODS

        [HttpPost]
        [ValidateJsonAntiForgeryToken]
        public async Task<ActionResult> Authenticate(Login oLogin)
        {
            if (ModelState.IsValid)
            {
                NNCrypt oCrypt;
                SessionUser oSessionUser;
                oLogin.Validate();
                this.oUserAPI.setLogin(oLogin);
                TempData.Clear();
                if (await this.oUserAPI.Authenticate())
                {
                    oSessionUser = this.oUserAPI.getSessionUser();

                    if (oSessionUser != null)
                    {
                        //Authenticated
                        //Update Session
                        Session["SessionUser"] = oSessionUser;
                        //Token -> Session
                        Session["UserToken"] = this.oUserAPI.cUserToken;

                        //Store token/user cookie
                        if (oLogin.StayConnected)
                        {
                            oCrypt = new NNCrypt(ConfigurationManager.AppSettings["CookieKey"], ConfigurationManager.AppSettings["CookieVector"]);
                            HttpCookie cookieTkn = new HttpCookie("RememberToken");
                            HttpCookie cookieUsr = new HttpCookie("RememberUser");

                            //30 days cookie expiration time
                            cookieTkn.Expires = DateTime.Now.AddDays(30);
                            cookieTkn.HttpOnly = true;
                            cookieUsr.Expires = DateTime.Now.AddDays(30);
                            cookieUsr.HttpOnly = true;

                            //Sets the value
                            cookieTkn.Value = this.oUserAPI.cUserToken;
                            cookieUsr.Value = oCrypt.Encrypt(oSessionUser.EMail);
                            
                            //Writes the cookie
                            Response.Cookies.Add(cookieTkn);
                            Response.Cookies.Add(cookieUsr);
                        }
                        
                        //Redirect to User Profile (or URL destination if set)
                        if (oLogin.ReturnUrl != null && oLogin.ReturnUrl.Length > 0)
                        {
                            return Redirect(oLogin.ReturnUrl);
                        }
                        else
                        {
                            return Redirect("/UserProfile");
                        }
                    }
                }
                else
                {
                    TempData["StatusMessage"] = new
                    {
                        success = false,
                        statuscode = 400,
                        response = "Authentication Error.",
                        odata = ""
                    };
                }
            }
            else
            {
                TempData["StatusMessage"] = new
                {
                    success = false,
                    statuscode = 400,
                    response = "Authentication Error.",
                    odata = ""
                };
            }

            return Redirect("/UserProfile/Login");
        }

        [HttpPost]
        [NNLoginPersistence]
        [NNAuthorize]
        [ValidateJsonAntiForgeryToken]
        public async Task<ActionResult> UpdateUserDetail(UserDetail oUserData)
        {
            HttpResponseMessage oResponse;
            this.oUserAPI.setToken(Session["UserToken"].ToString());

            if (ModelState.IsValid)
            {

                oResponse = this.oUserAPI.UpdateUserDetails(oUserData);

                if (oResponse.IsSuccessStatusCode)
                {
                    TempData["StatusMessage"] = new
                    {
                        success = true,
                        statuscode = 200,
                        response = new { Message = "Details updated succefully." },
                        odata = ""
                    };
                    return Json(new
                    {
                        success = true,
                        statuscode = 200,
                        response = new { Message = "Details updated succefully." },
                        odata = new { }
                    }, JsonRequestBehavior.AllowGet);

                }

                return Json(new
                {
                    success = oResponse.IsSuccessStatusCode,
                    statuscode = oResponse.StatusCode,
                    response = oResponse,
                    odata = new { }
                }, JsonRequestBehavior.AllowGet);

            }

            return Json(new
            {
                success = false,
                statuscode = 401,
                response = new { Message = "Bad Request." },
                odata = new { }
            }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [NNLoginPersistence]
        [NNAuthorize]
        //[ValidateJsonAntiForgeryToken]
        public async Task<ActionResult> UpdateProfileImage(HttpPostedFileBase oImage, FormCollection oParams)
        {

            HttpResponseMessage oResponse;
            ImageFile oImageFile = new ImageFile();

            oImageFile.cContainer = oParams.GetValue("cContainer").AttemptedValue;
            oImageFile.cFileName = oParams.GetValue("cFileName").AttemptedValue; 
            oImageFile.cContentType = oParams.GetValue("cContentType").AttemptedValue;

            if (oImage != null)
            {
                byte[] oRead = new byte[oImage.ContentLength];
                int nRead = await oImage.InputStream.ReadAsync(oRead, 0, Request.Files[0].ContentLength);
                oImageFile.setFileData(oRead);
            }

            /*
            oImageFile.cContainer = Request.Form.GetValues("cContainer").GetValue(0).ToString();
            oImageFile.cFileName = Request.Form.GetValues("cFileName").GetValue(0).ToString();
            oImageFile.cContentType = Request.Form.GetValues("cContentType").GetValue(0).ToString();

            if(Request.Files.Count > 0)
            {
                byte[] oRead = new byte[Request.Files[0].ContentLength];
                int nRead = await Request.InputStream.ReadAsync(oRead, 0, Request.Files[0].ContentLength);
                oImageFile.setFileData(oRead);
            }
            */

            //oResponse = oNNAPIClient.Put<ImageFile>("/Image", oImageFile);
            oResponse = await oNNAPIClient.PutImage(oImageFile);

            return Json(new
            {
                success = oResponse.IsSuccessStatusCode,
                statuscode = oResponse.StatusCode,
                response = oResponse,
                odata = new { }
            }, JsonRequestBehavior.AllowGet);

        }

        #endregion

    }
}
