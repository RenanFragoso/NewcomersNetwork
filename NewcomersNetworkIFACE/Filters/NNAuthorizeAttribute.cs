using NewcomersNetworkAPI.Models;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkIFACE.Models;
using NewcomersNetworkIFACE.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Filters
{
    public class NNAuthorizeAttribute : AuthorizeAttribute
    {
    
        public NNAuthorizeAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
                
        /// <summary>  
        /// Verifies the authenticated token, try to recover information from "Stay Connected"
        /// when available, and verifies permited roles when informed
        /// </summary>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            HttpResponseMessage oResponse;
            SessionUser oSessionUser = (SessionUser)httpContext.Session["SessionUser"];
            User oUser;
            NNCrypt oCrypt;
            NNAPIClient oClient = new NNAPIClient();
            string cUserToken = "";
            HttpCookie oCookie;

            if (httpContext.Session.IsNewSession || oSessionUser == null)
            {
                // New session or expired one, try to read Remember-Login cookie
                oCookie = httpContext.Request.Cookies["RememberToken"];
                if (oCookie != null)
                {
                    cUserToken = oCookie.Value;
                    oClient.setToken(cUserToken);
                    oResponse = oClient.Post<string>("/NNAuth/TokenProbe", cUserToken);
                    if (oResponse.IsSuccessStatusCode)
                    {
                        string cSalt = ConfigurationManager.AppSettings["CryptoSalt"];
                        oCrypt = new NNCrypt(ConfigurationManager.AppSettings["CookieKey"], ConfigurationManager.AppSettings["CookieVector"]);
                        string cUser = oCrypt.Decrypt(httpContext.Request.Cookies["RememberUser"].Value);
                        cUser = cUser.Substring(cSalt.Length);
                        if (cUser != null)
                        {
                            oUser = oClient.Get<User>("/Users/GetDetails/" + Convert.ToBase64String(Encoding.Unicode.GetBytes(cUser)));
                            if (oUser != null)
                            {
                                oSessionUser = new SessionUser(oUser);
                                httpContext.Session["SessionUser"] = oSessionUser;
                                httpContext.Session["UserToken"] = cUserToken;

                                if (this.Roles.Length > 0)
                                {
                                    return IsInRole(oSessionUser.Roles, this.Roles);
                                }

                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            else
            {

                //Need to verify session provided Token validity/expiration into the API
                cUserToken = (string)httpContext.Session["UserToken"];
                oClient.setToken(cUserToken);
                oResponse = oClient.Post<string>("/NNAuth/TokenProbe", cUserToken);

                if (!oResponse.IsSuccessStatusCode)
                {
                    return false;
                }

                if (this.Roles.Length > 0)
                {
                    return IsInRole(oSessionUser.Roles, this.Roles);
                }
                return true;
            }
        }

        private static bool IsInRole(List<string> oUserRoles, string roles)
        {
            int nI = 0;
            bool bFound = false;
            if (oUserRoles.Count > 0)
            {
                while( (nI < oUserRoles.Count) && !bFound)
                {
                    bFound = (roles.IndexOf(oUserRoles[nI], StringComparison.CurrentCultureIgnoreCase) >= 0);
                    nI++;
                }
            }
            return bFound;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            string urlDestination = filterContext.RequestContext.HttpContext.Request.Url.ToString();

            if(urlDestination.IndexOf(NNSiteAreas.NNAdmin,StringComparison.CurrentCultureIgnoreCase) >= 0)
            {
                // Administration Request
                base.HandleUnauthorizedRequest(filterContext);
            }

            else
            {
                // Returns HTTP 401 - see comment in HttpUnauthorizedResult.cs.
                //filterContext.Result = new HttpUnauthorizedResult();
                filterContext.Result = new RedirectToRouteResult(
                                        new System.Web.Routing.RouteValueDictionary
                                        {
                                        { "action", "Login" },
                                        { "controller", "UserProfile"}
                                        });
            }
        }
    }
}