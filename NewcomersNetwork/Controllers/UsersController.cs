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
    public class UsersController : ApiController
    {
        [Route("")]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult Get()
        {
            DataTable oUsersDB = DBConn.ExecuteCommand("sp_User_GetAll", null).Tables[0];    //Get all Users
            List<User> oUsers = new List<User>();

            if (oUsersDB.Rows.Count > 0)
            {
                foreach (DataRow row in oUsersDB.Rows)
                {
                    User oUser = new User();
                    oUser.MapFromTableRow(row);
                    oUser.getDetails();
                    oUsers.Add(oUser);
                }
            }

            if (oUsers.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return Ok(oUsers);
        }

        [Route("{cUserId}", Name = "GetUser")]
        [HttpGet]
        public IHttpActionResult Get(string cUserId)      //Get a specific User
        {
            User oUser;
            if (cUserId != null && cUserId.Length > 0)
            {
                oUser = new User(cUserId);
                return Ok(oUser);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

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

        [Route("GetMDetails/{cUserMail}")]
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetMDetails(string cUserMail)      //Get a specific User by email
        {
            if (cUserMail != null && cUserMail.Length > 0)
            {
                User oUser = new User();
                oUser.GetByMail(Encoding.Unicode.GetString(Convert.FromBase64String(cUserMail)));
                ClaimsPrincipal oPrincipal = (ClaimsPrincipal)Request.GetRequestContext().Principal;

                if (oPrincipal.Identity.Name == oUser.UserName)
                {
                    return Ok(oUser);
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("Create")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult AddUser([FromBody]User oUser)       //Insert an User
        {
            if (oUser != null && oUser.Save())
            {
                return CreatedAtRoute("GetUser", new { cUserId = oUser.Id }, oUser);
            }
            else
            {
                return BadRequest(string.Join(",", oUser.sMsgError.ToArray()));
            }
        }

        [Route("activate/{userId}", Name = "ActivateUser")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult ActivateUser(string userId)       //Activate an User
        {
            try
            {
                var user = new User(userId);
                user.ValidOrBreak();
                user.Activate();
                return Ok();
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

        [Route("Block/{cUserId}", Name = "BlockUser")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult BlockUser(string cUserId)       //Activate an User
        {
            User oUser;

            if (cUserId != null && cUserId.Length > 0)
            {

                oUser = new User(cUserId);

                if (oUser != null && oUser.isValid && oUser.Block())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(string.Join(",", oUser.sMsgError.ToArray()));
                }

            }

            return BadRequest("Malformed request.");
        }


        [Route("Update")]
        [HttpPut]
        [Authorize]
        public IHttpActionResult UpdateUser([FromBody]User oUser)       //Update an User
        {
            if (oUser != null && oUser.Update())
            {
                return Ok();//CreatedAtRoute("GetUser", new { cUserId = oUser.Id }, oUser);
            }
            else
            {
                return BadRequest(oUser.sMsgError.ToString());
            }

        }

        [Route("Delete/{cUserId}", Name = "DeleteUser")]
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult DeleteUser(string cUserId)       //Delete an User
        {
            User oUser;

            if (cUserId != null && cUserId.Length > 0)
            {

                oUser = new User(cUserId);

                if (oUser != null && oUser.isValid && oUser.Delete())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(string.Join(",", oUser.sMsgError.ToArray()));
                }

            }

            return BadRequest("Malformed request.");

        }

        [Route("GetBatch", Name = "GetBatch")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult GetBatch([FromBody] List<string> oUserIds)
        {

            List<User> oUsers = new List<User>();
            User oUser;

            if (oUserIds != null)
            {

                foreach (string cUserId in oUserIds)
                {

                    if (cUserId != null && cUserId.Length > 0)
                    {
                        oUser = new User(cUserId);
                        oUser.getDetails();
                        oUsers.Add(oUser);
                    }

                }

                return Ok(oUsers);

            }

            return BadRequest("Malformed request.");
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

        [Route("avatar")]
        [HttpPut]
        [Authorize]
        public async Task<IHttpActionResult> UpdateAvatar()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                ClaimsPrincipal oPrincipal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
                var userId = oPrincipal.Identity.GetUserId();
                var userDetails = new UserDetails(userId);
                userDetails.ValidOrBreak();
                var root = HttpContext.Current.Server.MapPath("~/App_Data");
                var provider = new MultipartFormDataStreamProvider(root);
                await Request.Content.ReadAsMultipartAsync(provider);
                var imageFile = ImageFactory.CreateFullImageModel("users", userDetails.PictureName, provider.FileData);
                ImageContainer.UploadImage(imageFile);

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