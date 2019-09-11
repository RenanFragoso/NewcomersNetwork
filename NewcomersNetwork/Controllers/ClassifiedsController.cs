using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkAPI.Providers;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.Text;
using System.IO;
using NewcomersNetworkAPI.Models.Image;
using System.Web;
using NewcomersNetworkAPI.Extensions;
using NewcomersNetworkAPI.Exceptions;
using NewcomersNetworkAPI.Models.Widgets;

namespace NewcomersNetworkAPI.Controllers
{

    [RoutePrefix("api/justshare")]
    public class ClassifiedsController : ApiController
    {

        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public ClassifiedsController()
        {
        }

        public ClassifiedsController(ApplicationUserManager userManager,
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
        /*
        public IHttpActionResult Get()
        {
            DataTable oClassDB = DBConn.ExecuteCommand("sp_Classifieds_GetAll", null).Tables[0];
            List<Classified> oClassifieds = new List<Classified>();

            Classified oClassified;

            if (oClassDB.Rows.Count > 0)
            {
                foreach (DataRow row in oClassDB.Rows)
                {
                    oClassified = new Classified();
                    oClassified.MapFromTableRow(row);

                    if (oClassified.isValid)
                    {
                        oClassifieds.Add(oClassified);
                    }
                }
            }
            return Ok(oClassifieds);
        }
        */

        [Route("{classifiedId}")]
        [HttpGet]
        public IHttpActionResult GetClassified(string classifiedId)
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
            var userId = principal.Identity.GetUserId();

            try
            {
                Classified classified = new Classified(Encoding.UTF8.GetString(Convert.FromBase64String(classifiedId)));
                classified.ValidOrBreak();

                classified.oCategory = new ClsGrpSimple(classified.Category);
                classified.CheckOwnership(userId);

                var newJsWidget = new UserJustShareWidget(classified);
                if (userId != null)
                {
                    newJsWidget.LoadMessages(userId);
                }

                return Ok(
                    new
                    {
                        justShareDetails = classified,
                        justShareWidget = newJsWidget
                    });
            }
            catch(InvalidModelException e)
            {
                return BadRequest();
            }
            catch(Exception e)
            {
                return BadRequest();
            }


        }

        [Route("getfiltered")]
        [HttpPost]
        public async Task<IHttpActionResult> GetFiltered(NNClassifiedsFilter filter)
        {
            ClaimsPrincipal principal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            List<Classified> oClassifieds = new List<Classified>();
            Classified oClassified;
            Pagination pagination = new Pagination();
            int nPages = 0;

            // No filters selected, returns right away
            if (filter.Categories.ToList().Count == 0)
            {
                return Ok(new
                {
                    justShareGridData = new
                    {
                        items = oClassifieds,
                        pagination
                    }
                });
            }

            Task<int> tPagination = pagination.GetPagination(filter);

            infoParameters.Add("Page", filter.Page);
            infoParameters.Add("PageSize", filter.PageSize);
            infoParameters.Add("Category", string.Join("|", filter.Categories));
            infoParameters.Add("Type", filter.Type);
            infoParameters.Add("Word", filter.Word);
            DataTable oClassDB = DBConn.ExecuteCommand("sp_Classifieds_GetFiltered", infoParameters).Tables[0];

            if (oClassDB.Rows.Count > 0)
            {
                foreach (DataRow row in oClassDB.Rows)
                {
                    oClassified = new Classified();
                    oClassified.MapFromTableRow(row);

                    if (oClassified.isValid)
                    {
                        oClassified.oAuthor = new UserSimple(oClassified.CreatedBy);
                        oClassified.oCategory = new ClsGrpSimple(oClassified.Category);
                        oClassified.CheckOwnership(principal.Identity.GetUserId());
                        oClassifieds.Add(oClassified);
                    }


                }
            }

            // Wait for the pagination
            nPages = await tPagination;

            return Ok(new
            {
                justShareGridData = new
                {
                    items = oClassifieds,
                    pagination
                }
            });
        }

        /*
        [Route("Pending")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetPending()
        {
            DataTable oClassDB = DBConn.ExecuteCommand("sp_Classifieds_Pending", null).Tables[0];
            List<Classified> oClassifieds = new List<Classified>();

            Classified oClassified;

            if (oClassDB.Rows.Count > 0)
            {
                foreach (DataRow row in oClassDB.Rows)
                {
                    oClassified = new Classified();
                    oClassified.MapFromTableRow(row);

                    if (oClassified.isValid)
                    {
                        oClassifieds.Add(oClassified);
                    }
                }
            }
            return Ok(oClassifieds);
        }

        [Route("pendingtotal")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetPendingNum()
        {
            return Ok(Classified.GetPendingNum());
        }
        */

        [Route("requestid")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult RequestClassifiedId()
        {
            return Ok(new {
                newId = Guid.NewGuid()
            });
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> CreateClassified()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                ClaimsPrincipal principal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
                var root = HttpContext.Current.Server.MapPath("~/App_Data");

                var provider = new MultipartFormDataStreamProvider(root);
                await Request.Content.ReadAsMultipartAsync(provider);
                Directory.CreateDirectory(root);
                var classifiedForm = new ClassifiedForm(provider.FormData)
                {
                    CreatedBy = principal.Identity.GetUserId()
                };

                ModelState.Clear();
                this.Validate<ClassifiedForm>(classifiedForm);

                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState.WithoutFormName());
                }

                var classified = new Classified(classifiedForm);
                classified.ValidOrBreak();
                classified.Save();

                var imageFile = ImageFactory.CreateFullImageModel("classifieds", classified.Id, provider.FileData);
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

        /*
        [HttpPut]
        [Authorize]
        public IHttpActionResult UpdateClassified([FromBody]Classified oClassified)
        {

            if (oClassified != null && oClassified.Update())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oClassified.sMsgError.ToArray()));
            }

        }
        */

        public class JustShareId
        {
            public string justShareId { get; set; }
        }
        
        [Route("finish")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult FinishClassified([FromBody] JustShareId jsinfo)
        {
            if (ModelState.IsValid)
            {
                ClaimsPrincipal claims = (ClaimsPrincipal)Request.GetRequestContext().Principal;
                Classified classified = new Classified(Encoding.UTF8.GetString(Convert.FromBase64String(jsinfo.justShareId)));

                if (classified != null && classified.CreatedBy == claims.Identity.GetUserId())
                {
                    if (classified.Finish())
                    {
                        return Ok();
                    }
                }
                return BadRequest(string.Join(",", classified.sMsgError.ToArray()));
            }
            return BadRequest();
        }
        /*
        [Route("Approve/{cClassifiedId}")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult ApproveClassified(string cClassifiedId)
        {
            Classified oCLassified = new Classified(cClassifiedId);
            if (oCLassified.isValid && oCLassified.Approve())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oCLassified.sMsgError.ToArray()));
            }
        }

        [Route("Reject/{cClassifiedId}")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult RejectClassified(string cClassifiedId)
        {
            Classified oCLassified = new Classified(cClassifiedId);
            if (oCLassified.isValid && oCLassified.Reject())
            {
                return Ok();
            }
            else
            {
                return BadRequest(string.Join(",", oCLassified.sMsgError.ToArray()));
            }
        }
        */
        #region Messages

        [Route("addmessage")]
        [HttpPost]
        [Authorize]
        public IHttpActionResult AddMessage(SecureClassifiedMessage jsMsgForm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.WithoutFormName());
            }

            /*
            if (!Captcha.VerifyResponse(jsMsgForm.Captcha))
            {
                ModelState.AddModelError("jsMsgForm.Captcha", "Captcha failed.");
                return BadRequest(ModelState.WithoutFormName());
            }
            */

            try
            {
                ClaimsPrincipal principal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
                Classified classified = new Classified(jsMsgForm.ClassifiedId);
                classified.ValidOrBreak();

                ClassifiedMessage message = new ClassifiedMessage()
                {
                    Id = 0,
                    Message = jsMsgForm.Message,
                    ClassifiedId = jsMsgForm.ClassifiedId,
                    From = principal.Identity.GetUserId(),
                    To = classified.CreatedBy
                };
                message.ValidOrBreak();
                message.Save();
                //return BadRequest("Error saving message. Try again later.");

                // Send message to JustShare creator if ConsentToContact
                classified.NotifyMessage(message);

                return Ok();
            }
            catch (InvalidModelException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest("Error saving message. Try again later.");
            }
        }

        [Route("messages")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetAllMessages()
        {
            ClaimsPrincipal claims = (ClaimsPrincipal)Request.GetRequestContext().Principal;

            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser oUser = await UserManager.FindByNameAsync(User.Identity.Name);
                if (oUser != null)
                {
                    ClassifiedMessages oMessages = new ClassifiedMessages();
                    oMessages.LoadMessagesFrom(oUser.Id);
                    return Ok(oMessages);

                }
            }
            return BadRequest();
        }

        /*
        [Route("{cClassifiedId}/Messages")]
        [HttpGet]
        [Authorize]
        public async Task<IHttpActionResult> GetFrom(string cClassifiedId)
        {
            string cFrom = "";
            
            //ClaimsPrincipal oPrincipal = (ClaimsPrincipal)Request.GetRequestContext().Principal;
            if(User.Identity.IsAuthenticated)
            {
                ApplicationUser oUser = await UserManager.FindByNameAsync(User.Identity.Name);
                if (oUser != null)
                {
                    cFrom = oUser.Id;
                }
            }

            if ((cClassifiedId != null && cClassifiedId.Length > 0) && cFrom.Length > 0)
            {
                Classified oClassified = new Classified(cClassifiedId);
                //oClassified.LoadMessages(cFrom, oClassified.CreatedBy);
                oClassified.LoadMessagesFrom(cFrom);
                return Ok(oClassified.oMessages);
            }
            return BadRequest();
        }
        */

        /*
        [Route("{classifiedId}/allmessages")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetAll(string classifiedId)
        {
            if (classifiedId != null && classifiedId.Length > 0)
            {
                Classified oClassified = new Classified(classifiedId);
                oClassified.LoadMessages();
                return Ok(oClassified.oMessages);
            }
            return BadRequest();
        }
        */

        #endregion

    }
}
