using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkAPI.Providers;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using System.Security.Claims;
using NewcomersNetworkAPI.Exceptions;
using NewcomersNetworkAPI.Extensions;
using NewcomersNetworkAPI.Models.Image;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/users")]
    public partial class UsersController : ApiController
    {
        /// <summary>  
        /// This method need to ensure that the request has been made
        /// from a valid user and this user is the same as the token user
        /// (using ClaimsPrincipal to compare token and get attr value)
        /// </summary> 
        [Route("getdetails")]
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetDetails()
        {
            try
            {
                ClaimsPrincipal principal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
                var userId = principal.Identity.GetUserId();

                var user = new User(userId);
                user.ValidOrBreak();
                user.LoadFullDetails();

                var userDetails = new UserDetailsViewModel(user.oDetails);

                return Ok(new { userDetails });
            }
            catch (InvalidModelException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Route("userdetailsform")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult UserDetailsForm()
        {
            ClaimsPrincipal oPrincipal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
            var userId = oPrincipal.Identity.GetUserId();
            var user = new User(userId);
            if (user.isValid)
            {
                user.LoadFullDetails();
                var userDetailsForm = new UserDetailsForm(user.oDetails);
                return Ok(new
                {
                    userDetailsForm
                });
            }

            return BadRequest();
        }

        [Route("details")]
        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> UpdateDetails(UserDetailsForm userDetailsForm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.WithoutFormName());
            }

            /*
            if (!Captcha.VerifyResponse(userDetailsForm.Captcha))
            {
                ModelState.AddModelError("userDetailsForm.Captcha", "Captcha failed.");
                return BadRequest(ModelState.WithoutFormName());
            }
            */

            try
            {
                ClaimsPrincipal principal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
                var userId = principal.Identity.GetUserId();

                UserDetails userDetails = new UserDetails(userId);
                userDetails.ValidOrBreak();
                userDetails.UpdateFromForm(userDetailsForm);

                return Ok();
            }
            catch (InvalidModelException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}