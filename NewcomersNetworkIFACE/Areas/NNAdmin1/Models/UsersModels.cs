using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkIFACE.Models;
using NewcomersNetworkAPI.Models;
using System.Web.Http;
using System.Net.Http;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Models
{
    public class Users
    {
        protected NNAPIClient oNNAPICLient = new NNAPIClient();
        public List<User> oUserList { get; protected set; }
        public List<UsersRoles> oUserRoles { get; protected set; }
        public OptionsLists oLists { get; set; }

        public Users()
        {
        }

        public void LoadUsers()
        {
            //Get the users from API
            oUserList = oNNAPICLient.Get<List<User>>("/Users");

            //Get the roles from API
            oUserRoles = oNNAPICLient.Get<List<UsersRoles>>("/UsersRoles");

            //Get the Lists
            oLists = oNNAPICLient.Get<OptionsLists>("/OptionsLists");

        }

        public List<User> GetAllUsers()
        {
            this.LoadUsers();
            return this.oUserList;
        }
        
        public HttpResponseMessage UpdateUser( User oUser )
        {
            HttpResponseMessage response;

            if (oUser != null)
            {

                if(oUser.Id != "NULLID")
                {
                    //Update API
                    response = oNNAPICLient.Put<User>("/Users/Update", oUser);
                    return response;
                }
                else
                {
                    //Create API
                    response = oNNAPICLient.Post<User>("/Users/Create", oUser);
                    return response;

                }

            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Form data.");
                return response;
            }

        }

        public HttpResponseMessage RemoveUser(string cUserId)
        {
            HttpResponseMessage response;

            if (cUserId != null && cUserId.Length > 0)
            {

                //Delete API
                response = oNNAPICLient.Delete("/Users/Delete", cUserId);
                return response;

            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Request.");
                return response;
            }

        }

        public HttpResponseMessage BlockUser(string cUserId)
        {
            HttpResponseMessage response;

            if (cUserId != null && cUserId.Length > 0)
            {

                //Delete API
                response = oNNAPICLient.Post<string>("/Users/Block", cUserId);
                return response;

            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Request.");
                return response;
            }

        }

        public HttpResponseMessage ActivateUser(string cUserId)
        {
            HttpResponseMessage response;

            if (cUserId != null && cUserId.Length > 0)
            {

                //Delete API
                response = oNNAPICLient.Post<string>("/Users/Activate", cUserId);
                return response;

            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid Request.");
                return response;
            }

        }

    }
}