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

namespace NewcomersNetworkIFACE.Controllers
{
    public class UserProfileController : NNAPIController
    {
        protected UserAPI oUserAPI = new UserAPI();

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
            return View();
        }

        public ActionResult LogOut()
        {
            //Session.RemoveAll();
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
            base.VerifyCredential();
            return View();
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
        [ValidateJsonAntiForgeryToken]
        [NNAuthorize]
        public ActionResult EditProfile(UserDetails oDetail)
        {
            return RedirectToAction("Index", "UserProfile");
        }

        #endregion

    }
}
