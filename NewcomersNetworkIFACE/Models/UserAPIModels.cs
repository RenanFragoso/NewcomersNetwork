using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkAPI.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace NewcomersNetworkIFACE.Models
{
    public class UserAPI : NNInterfaceModel
    {
        protected Login oLogin { get; set; } = new Login();
        protected SignIn oSignIn { get; set; } = new SignIn();
        public string cUserToken { get; set; } = "";
        protected UserToken oUserToken { get; set; }
        public User oUserDetails { get; set; } = new User();

        public UserDetail oEmptyDetail = new UserDetail();

        public UserAPI()
        {
        }

        public void setLogin(Login oLogin)
        {
            if (oLogin.isValid)
            {
                this.oLogin = oLogin;
            }
        }

        public void setSignIn(SignIn oSignIn)
        {
            if (oSignIn.isValid)
            {
                this.oLogin = oLogin;
            }
        }

        public async Task<bool> Authenticate()
        {
            //Try to authenticate into TOKEN API (it must be a form request, not JSON)
            //Get Token 
            UserToken oUserToken;
            LoginSendForm oLoginForm = new LoginSendForm(this.oLogin);
            HttpResponseMessage oResponse = await this.oNNAPICLient.RequestToken(this.oLogin);
            if (oResponse.IsSuccessStatusCode)
            {
                oUserToken = JsonConvert.DeserializeObject<UserToken>(oResponse.Content.ReadAsStringAsync().Result);
                this.oUserToken = oUserToken;
                this.cUserToken = oUserToken.access_token;
                this.oNNAPICLient.setToken(this.cUserToken);

                this.oUserDetails = oNNAPICLient.Get<User>("/Users/GetDetails/" + Convert.ToBase64String(Encoding.Unicode.GetBytes(this.oUserToken.userName)));
                if(this.oUserDetails != null && this.oUserDetails.Email == this.oUserToken.userName)
                {
                    return true;  
                }
            }
            return false;
        }

        public bool Register()
        {
            //Register User into API (POST NEW USER TO API)
            //Create Session
            //Get Token -> Session
            //Redirect to Profile Page
            return false;
        }

        public HttpResponseMessage UpdateUserDetails(UserDetail oDetails)
        {
            HttpResponseMessage oResponse = this.oNNAPICLient.Put<UserDetail>("/Users/Details",oDetails);
            return oResponse;
        }

        public void LogOut()
        {
            //Call Logout API
            //Remove the session
            //Remove Token
            
        }
                
        public SessionUser getSessionUser()
        {
            SessionUser oSessionUser;
            if(this.oUserDetails != null)
            {
                oSessionUser = new SessionUser(this.oUserDetails);
                return oSessionUser;
            }

            return null;            
        }
        
        public bool getUserData(SessionUser oSessionUsr)
        {
            this.oUserDetails = oNNAPICLient.Get<User>("/Users/GetDetails/" + Convert.ToBase64String(Encoding.Unicode.GetBytes(oSessionUsr.EMail)));
            if (this.oUserDetails != null)
            {
                return true;
            }

            return false;
        }

        public void setToken(string cToken)
        {
            this.cUserToken = cToken;
            this.oNNAPICLient.setToken(this.cUserToken);
        }

        public OptionsLists getLists()
        {
            //Get the Lists - user Group
            OptionsLists oLists = oNNAPICLient.Get<OptionsLists>("/OptionsLists/Group/user");
            return oLists;
        }


    }

    public class LoginSendForm
    {
        public string grant_type { get; set; } = "password";
        public string username { get; set; } = "";
        public string password { get; set; } = "";

        public LoginSendForm()
        {
        }

        public LoginSendForm(Login oLogin)
        {
            if (oLogin != null && oLogin.isValid)
            {
                this.username = oLogin.UserName;
                this.password = oLogin.Password;
            }
        }
    }

    public class SessionUser
    {
        public string Name { get; set; } = "";
        public string EMail { get; set; } = "";
        public string Token { get; set; } = "";
        public string Picture { get; set; } = "";
        public List<string> Roles { get; set; } = new List<string>();

        public SessionUser()
        {
        }

        public SessionUser(User oUser)
        {
            if (oUser != null && oUser.oDetails != null)
            {
                this.Name = oUser.getFullName();
                this.EMail = oUser.Email;
                this.Picture = oUser.oDetails.Picture;
                foreach (var role in oUser.oDetails.oUserRoles)
                {
                    this.Roles.Add(role.Name);
                }
            }
        }

        public bool HasRole(string cRole)
        {
            if( this.Roles.Find(role => role == cRole) != null)
            {
                return true;
            }
            return false;
        }
    }

    public class UserToken
    {
        public string access_token { get; set; } = "";
        public string token_type { get; set; } = "";
        public string expires_in { get; set; } = "";
        public string userName { get; set; } = "";
    }

    public class UserDetail : NNInterfaceModel
    {

        [Key]
        [ScaffoldColumn(false)]
        public string Id { get; set; } = "";

        [Display(Name = "First Name")]
        [UIHint("First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = "";

        [Display(Name = "Last Name")]
        [UIHint("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = "";

        [Display(Name = "E-Mail")]
        [UIHint("_Email")]
        [Required(ErrorMessage = "E-Mail is required")]
        public string Email { get; set; } = "";

        [Display(Name = "Title")]
        [UIHint("Title")]
        public string Title { get; set; } = "";

        [Display(Name = "Gender")]
        [UIHint("Gender")]
        public string Gender { get; set; } = "";

        [Display(Name = "Marital Status")]
        [UIHint("Marital Status")]
        public string MaritalStatus { get; set; } = "";

        [Display(Name = "Age Range")]
        [UIHint("Age Range")]
        public string AgeRange { get; set; } = "";

        [Display(Name = "Education")]
        [UIHint("Education")]
        public string Education { get; set; } = "";

        [Display(Name = "Nearest Intersection")]
        [UIHint("Nearest Intersection")]
        public string NearestIntersection { get; set; } = "";

        [Display(Name = "Postal Code")]
        [UIHint("Postal Code")]
        public string PostalCode { get; set; } = "";

        [Display(Name = "I do authorize Newcomers Network to send me e-mails.")]
        [UIHint("Do you consent further e-mail contacts?")]
        public bool ConsentToContact { get; set; } = false;

        [Display(Name = "Status")]
        [UIHint("Status")]
        public string Status { get; set; } = "";

        [Display(Name = "I am a Newcomer")]
        [UIHint("Are you a Newcomer?")]
        public bool IsImmigrant { get; set; } = false;
        public string Picture { get; set; } = "";

        public List<UsersRoles> oUserRoles { get; set; } = new List<UsersRoles>();

    }


}