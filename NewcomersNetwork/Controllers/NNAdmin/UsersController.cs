using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using NewcomersNetworkAPI.Models;
using NewcomersNetworkAPI.Providers;
using System.Data;
using NewcomersNetworkAPI.Exceptions;
using NewcomersNetworkAPI.Models.NNAdmin;
using System.Threading.Tasks;

namespace NewcomersNetworkAPI.Controllers
{
    public partial class UsersController : ApiController
    {
        [Route("")]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [Route("")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IHttpActionResult> GetFilteredListAsync([FromBody] UsersListFilter filter)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                IEnumerable<UsersListView> usersList = new List<UsersListView>();
                Task<Pagination> taskPagination = UserService.GetUsersPagination(filter);
                usersList = UserService.GetAllUsers(filter);
                Pagination pagination = await taskPagination;
                return Ok( 
                    new {
                        usersList,
                        pagination
                    });
            }
            catch (InvalidModelException e)
            {
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("create", Name ="CreateUser")]
        [Authorize(Roles = "Administrator")]
        //public IHttpActionResult AddUser([FromBody]CreateUserForm userForm)
        public IHttpActionResult CreateUser([FromBody]User oUser)
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

        [HttpGet]
        [Route("{userId}", Name = "GetUser")]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult Get(string userId)
        {
            try
            {
                User user = new User(userId);
                user.ValidOrBreak();
                return Ok(user);
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

        [HttpPost]
        [Route("{userId}/activate", Name = "ActivateUser")]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult ActivateUser(string userId)
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

        [Route("{userId}/block", Name = "BlockUser")]
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult BlockUser(string userId)
        {
            try
            {
                var user = new User(userId);
                user.ValidOrBreak();
                user.Block();
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

        [HttpDelete]
        [Route("{userId}", Name = "DeleteUser")]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult DeleteUser(string userId)
        {
            try
            {
                var user = new User(userId);
                user.ValidOrBreak();
                user.Delete();
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

        [HttpPut]
        [Route("{userId}", Name = "UpdateUser")]
        [Authorize(Roles = "Administrator")]
        public IHttpActionResult UpdateUser([FromBody]User oUser)
        {
            if (oUser != null && oUser.Update())
            {
                return Ok();
            }
            else
            {
                return BadRequest(oUser.sMsgError.ToString());
            }

        }
    }
}