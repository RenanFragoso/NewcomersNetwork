using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkAPI.Providers;
using NewcomersNetworkAPI.Results;
using System.Configuration;
using System.Text;
using System.IO;
using System.Web.Hosting;
using System.Data;
using System.Linq;
using NewcomersNetworkAPI.Extensions;

namespace NewcomersNetworkAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        [Route("logout")]
        public IHttpActionResult Logout()
        {
            Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        [Route("changepassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel changePasswordForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.WithoutFormName());
            }

            /*
            if (!Captcha.VerifyResponse(changePasswordForm.Captcha))
            {
                ModelState.AddModelError("changePasswordForm.Captcha", "Captcha failed.");
                return BadRequest(ModelState.WithoutFormName());
            }
            */
            try
            {
                IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), changePasswordForm.OldPassword, changePasswordForm.Password);
                if (!result.Succeeded)
                {
                    foreach (var resError in result.Errors)
                    {
                        if (resError == "Incorrect password.")
                        {
                            ModelState.AddModelError("changePasswordForm.OldPassword", resError.ToString());
                        }
                    }

                    return BadRequest(ModelState.WithoutFormName());
                }
            }
            catch (Exception e)
            {

            }

            return Ok();
        }

        
        [Route("forgotsend")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ForgotSend([FromBody] ForgotPasswordForm forgotPwdForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.WithoutFormName());
            }

            /*
            if (!Captcha.VerifyResponse(forgotPwdForm.Captcha))
            {
                return BadRequest("Captcha validation fail.");
            }
            */
            ApplicationUser user = await UserManager.FindByEmailAsync(forgotPwdForm.Email);
            if(user != null)
            {
                string changeToken = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                try
                {
                    //Read the layout file
                    string mailBody = File.ReadAllText(HostingEnvironment.MapPath(@"~/EMailLayouts/UserProfile/_ForgotPassword.cshtml"));
                    string NNWebsiteURL = ConfigurationManager.AppSettings["NNWebsiteURL"].ToString();
                    
                    //Replace tokens
                    mailBody = mailBody.Replace("!NN_WSLINK!", NNWebsiteURL);
                    mailBody = mailBody.Replace("!NN_TOPIMAGE!", NNWebsiteURL + "/Content/Images/tpc-logo.png");
                    mailBody = mailBody.Replace("!CONFIRM_LINK!", NNWebsiteURL + "/resetpassword/" + Convert.ToBase64String(Encoding.Default.GetBytes(changeToken)));
                    mailBody = mailBody.Replace("!NN_MAILTO!", "newcomersnetworkws@newcomers.network");
                    mailBody = mailBody.Replace("!NN_CONTACT_LINK!", NNWebsiteURL + "/contact");

                    //Sending Confirmation E-Mail
                    NNSMTPSender.Instance.SendMail(mailBody, "Forgot Password Request", user.Email, "", true);
                }
                catch (Exception error)
                {
                    return BadRequest();
                }
            }

            return Ok();
        }


        [Route("resetpassword")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ResetPassword(ResetPasswordViewModel resetForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.WithoutFormName());
            }

            if (!Captcha.VerifyResponse(resetForm.Captcha))
            {
                ModelState.AddModelError("resetForm.Captcha", "Captcha failed.");
                return BadRequest(ModelState.WithoutFormName());
            }

            string token = Encoding.Default.GetString(Convert.FromBase64String(resetForm.Code));

            ApplicationUser user = await UserManager.FindByEmailAsync(resetForm.Email);

            if (user != null)
            {
                IdentityResult result = UserManager.ResetPassword(user.Id, token, resetForm.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return GetErrorResult(result);
                }
            }

            return BadRequest();            

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> Register(SignupForm signupForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.WithoutFormName());
            }

            /*
            if (!Captcha.VerifyResponse(signupForm.Captcha))
            {
                ModelState.AddModelError("signupForm.Captcha", "Captcha failed.");
                return BadRequest(ModelState.WithoutFormName());
            }
            */

            var user = new ApplicationUser() { UserName = signupForm.Username, Email = signupForm.Email };
            IdentityResult result = await UserManager.CreateAsync(user, signupForm.Password);

            if (!result.Succeeded)
            {
                if(string.Join(";", result.Errors).Substring(0,4) == "Name")
                {
                    ModelState.AddModelError("signupForm.Username", "User Name already taken.");
                    return BadRequest(ModelState.WithoutFormName());
                }

                if (string.Join(";", result.Errors).Substring(0, 5) == "Email")
                {
                    ModelState.AddModelError("signupForm.Email", "Email already taken.");
                    return BadRequest(ModelState.WithoutFormName());
                }

                return GetErrorResult(result);
            }

            User nnUser = new User();

            try
            {
                //Adding the user details to the database
                nnUser.SetUserDetails(
                    new UserDetails()
                    {
                        Id = user.Id,
                        Status = "C",                       // Waiting Confirmation
                        Email = signupForm.Email,
                        FirstName = signupForm.FirstName,
                        LastName = signupForm.LastName,
                        IsImmigrant = signupForm.IsNewcomer,
                        ConsentToContact = signupForm.ConsentToContact
                    });

                nnUser.oDetails.Save();

                //Read the layout file
                string cMailBody = File.ReadAllText(HostingEnvironment.MapPath(@"~/EMailLayouts/UserProfile/_ConfirmEmailLayout.cshtml"));
                string NNWebsiteURL = ConfigurationManager.AppSettings["NNWebsiteURL"];

                //Replace tokens
                cMailBody = cMailBody.Replace("!NN_WSLINK!", NNWebsiteURL);
                cMailBody = cMailBody.Replace("!NN_TOPIMAGE!", NNWebsiteURL + "/Content/images/tpc-logo.png");
                cMailBody = cMailBody.Replace("!CONFIRM_LINK!", NNWebsiteURL + "/confirmaccount/" + Convert.ToBase64String(Encoding.Unicode.GetBytes(user.UserName + "#" + user.SecurityStamp)));
                cMailBody = cMailBody.Replace("!NN_MAILTO!", "newcomersnetworkws@newcomers.network");
                cMailBody = cMailBody.Replace("!NN_CONTACT_LINK!", NNWebsiteURL + "/contact");

                //Sending Confirmation E-Mail
                NNSMTPSender.Instance.SendMail(cMailBody, "E-Mail Confirmation", nnUser.oDetails.Email, "", true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e);
                return BadRequest(ModelState.WithoutFormName());
            }
                        
            return Ok(new {
                confirmMail = nnUser.oDetails.Email
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("confirmation")]
        public IHttpActionResult ConfirmEmail([FromBody] ConfirmAccount confirmAccount)
        {
            if (ModelState.IsValid)
            {
                string[] userData = Encoding.Unicode.GetString(Convert.FromBase64String(confirmAccount.ConfirmationData)).Split(new char[] { '#', '#' }, 2);

                if((userData != null) && (userData[0] != null) && (userData[1] != null))
                {
                    User user = new User();
                    user.GetByUserName(userData[0]);
                    if (user.SecurityStamp == userData[1])
                    {
                        Dictionary<string, object> infoParameters = new Dictionary<string, object>();
                        infoParameters.Add("cUserName", userData[0]);
                        infoParameters.Add("SecurityStamp", userData[1]);
                        DataTable oUserData = DBConn.ExecuteCommand("sp_User_ConfirmLogin", infoParameters).Tables[0];

                        if (!oUserData.HasErrors) {
                            return Ok();
                        }
                    }
                }
            }

            return BadRequest("Invalid confirmation data.");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("resendemail")]
        public IHttpActionResult ResendEmail([FromBody]string cResendEmail)
        {
            if (cResendEmail != null && cResendEmail.Length > 0)
            {
                User oUser = new User();
                oUser.GetByMail(cResendEmail);

                if (oUser.isValid && oUser.oDetails.Status == "C") //Valid user and Waiting Confirmation
                {
                    //Read the layout file
                    string cMailBody = File.ReadAllText(HostingEnvironment.MapPath(@"~/EMailLayouts/UserProfile/_ConfirmEmailLayout.cshtml"));
                    string NNWebsiteURL = ConfigurationManager.AppSettings["NNWebsiteURL"];

                    //Replace tokens
                    cMailBody = cMailBody.Replace("!NN_WSLINK!", NNWebsiteURL);
                    cMailBody = cMailBody.Replace("!NN_TOPIMAGE!", NNWebsiteURL + "/Content/images/tpc-logo.png");
                    cMailBody = cMailBody.Replace("!CONFIRM_LINK!", NNWebsiteURL + "/confirmaccount/" + Convert.ToBase64String(Encoding.Unicode.GetBytes(oUser.UserName + "#" + oUser.SecurityStamp)));
                    cMailBody = cMailBody.Replace("!NN_MAILTO!", "newcomersnetworkws@newcomers.network");
                    cMailBody = cMailBody.Replace("!NN_CONTACT_LINK!", NNWebsiteURL + "/contact");

                    //Sending Confirmation E-Mail
                    NNSMTPSender.Instance.SendMail(cMailBody, "E-Mail Confirmation", oUser.oDetails.Email, "", true);
                }

                //If success, user not found or not waiting confirmation, don't say anything, just exit
                return Ok();

            }
            return BadRequest("Malformed Request.");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
