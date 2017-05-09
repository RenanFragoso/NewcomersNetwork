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
        public UserDetail oSignIn { get; set; } = new UserDetail();
        public string cUserToken { get; set; } = "";
        protected UserToken oUserToken { get; set; }
        public User oUser { get; set; } = new User();

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

        public void setFromDetail(UserDetail oDetail)
        {
            //User Values
            this.oUser.UserName = oDetail.UserName;
            this.oUser.Email = oDetail.Email;
            this.oUser.Password = oDetail.Password;

            //User Details Values
            this.oUser.oDetails.FirstName = oDetail.FirstName;
            this.oUser.oDetails.LastName = oDetail.LastName;
            this.oUser.oDetails.Email = oDetail.Email;
            this.oUser.oDetails.Title = oDetail.Title;
            this.oUser.oDetails.Gender = oDetail.Gender;
            this.oUser.oDetails.MaritalStatus = oDetail.MaritalStatus;
            this.oUser.oDetails.AgeRange = oDetail.AgeRange;
            this.oUser.oDetails.Education = oDetail.Education;
            this.oUser.oDetails.NearestIntersection = oDetail.NearestIntersection;
            this.oUser.oDetails.PostalCode = oDetail.PostalCode;
            this.oUser.oDetails.ConsentToContact = oDetail.ConsentToContact;
            this.oUser.oDetails.IsImmigrant = oDetail.IsImmigrant;
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

                this.oUser = oNNAPICLient.Get<User>("/Users/GetDetails/" + Convert.ToBase64String(Encoding.Unicode.GetBytes(this.oUserToken.userName)));
                //if(this.oUser != null && this.oUser.Email == this.oUserToken.userName)
                if (this.oUser != null && this.oUser.UserName == this.oUserToken.userName)
                {
                    return true;  
                }
            }
            return false;
        }

        public HttpResponseMessage Register()
        {
            //Register User into API (POST NEW USER TO API)
            //Create Session
            //Get Token -> Session
            //Redirect to Profile Page

            HttpResponseMessage oResponse;
            oResponse = this.oNNAPICLient.Post<User>("/Account/Register",this.oUser);

            //HttpResponseMessage oResponse = new HttpResponseMessage();
            //oResponse.StatusCode = System.Net.HttpStatusCode.Created;
            return oResponse;
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
            if(this.oUser != null)
            {
                oSessionUser = new SessionUser(this.oUser);
                return oSessionUser;
            }

            return null;            
        }
        
        public bool getUserData(SessionUser oSessionUsr)
        {
            this.oUser = oNNAPICLient.Get<User>("/Users/GetDetails/" + Convert.ToBase64String(Encoding.Unicode.GetBytes(oSessionUsr.EMail)));
            if (this.oUser != null)
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

        public string Id { get; set; } = "NULLID";

        [Display(Name = "User Name")]
        [UIHint("First Name")]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = "";

        [Display(Name = "Password")]
        [UIHint("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$", ErrorMessage = "Password must be at least 6 characters long, have at least 1 Uppercase, 1 Lowercase and 1 digit (0-9).")]
        public string Password { get; set; } = "";

        [Display(Name = "Confirm Password")]
        [UIHint("Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "The Password and Confirmation Password does not match.")]
        public string ConfirmPassword { get; set; } = "";

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
        [EmailAddress]
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
        public bool ConsentToContact { get; set; } = true;

        [Display(Name = "Status")]
        [UIHint("Status")]
        public string Status { get; set; } = "";

        [Display(Name = "I am a Newcomer")]
        [UIHint("Are you a Newcomer?")]
        public bool IsImmigrant { get; set; } = true;

        [Display(Name = "I accept the Newcomers Network Therms of Use.")]
        [UIHint("Newcomers Network Therms of Use")]
        [Required(ErrorMessage = "Please, read and accept the Newcomers Network Terms of Use to continue.")]
        public bool NNThermsOfUse { get; set; } = false;

        public string Picture { get; set; } = "";

        public List<UsersRoles> oUserRoles { get; set; } = new List<UsersRoles>();

    }


}