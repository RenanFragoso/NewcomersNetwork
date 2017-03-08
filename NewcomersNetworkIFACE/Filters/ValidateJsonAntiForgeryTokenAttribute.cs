using System;
using System.Web.Helpers;
using System.Web.Mvc;

namespace NewcomersNetworkIFACE.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ValidateJsonAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var httpContext = filterContext.HttpContext;
            var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            string cCookieToken = (cookie != null ? cookie.Value : null);

            string cToken = httpContext.Request.Headers["__RequestVerificationToken"];
            if (cToken == null)
            {
                //If Header Token not set, try form token
                cToken = httpContext.Request.Form.Get("__RequestVerificationToken");
            }
            
            try
            {
                AntiForgery.Validate(cCookieToken, cToken);
            }
            catch (Exception e)
            {
                throw;
            }            
        }
    }
}



