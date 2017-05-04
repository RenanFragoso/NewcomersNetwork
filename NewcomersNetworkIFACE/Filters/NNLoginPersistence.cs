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
    public class NNLoginPersistenceAttribute : AuthorizeAttribute
    {
        /// <summary>  
        /// Permissive Filter responsible for getting the token/user information
        /// when "Stay Connected" available. 
        /// This filter doesn't block anything.
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
                // New session or expired one, try to read Remember Login cookie
                oCookie = httpContext.Request.Cookies["RememberToken"];
                if (oCookie != null)
                {
                    cUserToken = oCookie.Value;
                    oClient.setToken(cUserToken);
                    oResponse = oClient.Post<string>("/NNAuth/TokenProbe", cUserToken);
                    if (oResponse.IsSuccessStatusCode)
                    {
                        oCrypt = new NNCrypt(ConfigurationManager.AppSettings["CookieKey"], ConfigurationManager.AppSettings["CookieVector"]);
                        oCookie = httpContext.Request.Cookies["RememberUser"];
                        if(oCookie != null)
                        {
                            try
                            {
                                string cUser = oCrypt.Decrypt(oCookie.Value);
                                if (cUser != null)
                                {
                                    oUser = oClient.Get<User>("/Users/GetDetails/" + Convert.ToBase64String(Encoding.Unicode.GetBytes(cUser)));
                                    if (oUser != null)
                                    {
                                        oSessionUser = new SessionUser(oUser);
                                        httpContext.Session["SessionUser"] = oSessionUser;
                                        httpContext.Session["UserToken"] = cUserToken;
                                    }
                                }
                            }
                            catch (Exception error)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}