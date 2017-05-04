using System.Data;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Providers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NewcomersNetworkAPI.Models
{
    public class User : NNAPIModel
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Id { get; set; } = "";

        [Display(Name = "E-Mail")]
        [UIHint("E-mail")]
        [Required(ErrorMessage = "E-mail is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                   @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                   @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                   ErrorMessage = "Please, enter a valid E-Mail.")]
        public string Email { get; set; } = "";

        [Display(Name = "User Name")]
        [UIHint("User Name")]
        public string UserName { get; set; } = "";

        [Display(Name = "Password")]
        [UIHint("Password")]
        public string Password { get; set; } = "";

        public UserDetails oDetails { get; set; } = new UserDetails();

        public User()
        {
        }
        public User(string cUserID)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cUserId", cUserID);
            DataTable oUserData = DBConn.ExecuteCommand("sp_User_Get", infoParameters).Tables[0];

            if (oUserData.Rows.Count > 0)
            {
                this.MapFromTableRow(oUserData.Rows[0]);
            }
            
            if (this.isValid)
            {
                this.oDetails = new UserDetails(this.Id);
            }
            
        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = row["Id"].ToString();
                this.Email = row["Email"].ToString();
                this.UserName = row["UserName"].ToString();
            }
            catch (Exception e)
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  User.MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                return;
            }
            this.Validate();
        }
        public override bool Validate()
        {
            //Object structure validation
            if (this.Id != null && this.Id.Length > 0)
            {
                this.isValid = true;
                return true;
            }

            this.isValid = false;
            this.sMsgError.Add("Invalid User ID.");
            return false;
        }

        public void getDetails()
        {

            UserDetails oDet;
            if (this.isValid)
            {
                oDet = new UserDetails(this.Id);
                if (oDet.isValid)
                {
                    this.oDetails = oDet;
                }
            }
        }

        public bool Activate()
        {
            return false;
        }

        public bool Block()
        {
            return false;
        }
        
        public override bool Save()
        {

            /* Create OWIN user */
            UserManager<IdentityUser> oUsrManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            IdentityUser oUserOWin = new IdentityUser() { UserName = this.Email, Email = this.Email };
            IdentityResult result = oUsrManager.Create(oUserOWin, this.Password);

            // Save the user details if exists
            if (result.Succeeded)
            {
                if (this.oDetails != null)
                {
                    this.Id = oUserOWin.Id;
                    this.oDetails.Id = this.Id;
                    this.oDetails.Email = this.Email;
                    this.oDetails.Save();
                }
            }
            else
            {
                this.sMsgError.Add(string.Join(",", result.Errors.ToArray()));
                return false;
            }

            return true;
        }

        
        public override bool Update()
        {

            /* Update OWIN user */
            UserManager<IdentityUser> oUsrManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            IdentityResult result;
            var oUser = oUsrManager.FindById(this.Id);
            if (oUser != null)
            {
                // Update Password
                if (this.Password != null && this.Password.Length > 0)
                {
                    oUsrManager.RemovePassword(this.Id);
                    result = oUsrManager.AddPassword(this.Id, this.Password);

                    if (!result.Succeeded)
                    {
                        this.sMsgError.Add(string.Join(",", result.Errors.ToArray()));
                        return false;
                    }

                }

                oUser.Email = this.Email;
                oUser.UserName = this.Email;    //Keeping the default for OWIN: username = email

                // Update OWIN user
                result = oUsrManager.Update(oUser);

                /* Save the UserDetails */
                if (result.Succeeded)
                {
                    this.Id = oUser.Id;
                    if (this.oDetails != null)
                    {
                        if (!this.oDetails.Update())
                        {
                            this.sMsgError.Add(oDetails.GetErrors());
                            return false;
                        }

                        return true;
                    }
                    else
                    {
                        this.sMsgError.Add("UserDetails not present.");
                        return false;
                    }

                }
                else
                {
                    this.sMsgError.Add(string.Join(",", result.Errors.ToArray()));
                    return false;
                }

            }

            this.sMsgError.Add("User not found.");            
            return false;

        }
        
        public override bool Delete()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            DataTable oUserData;

            var oUsrManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
            var oUser = oUsrManager.FindById(this.Id);
            IdentityResult result = oUsrManager.Delete(oUser);

            /* Save the UserDetails */
            if (result.Succeeded)
            {
                infoParameters.Add("cUserId", this.Id);
                oUserData = DBConn.ExecuteCommand("sp_User_Delete", infoParameters).Tables[0];

                if (oUserData.HasErrors)
                {
                    this.sMsgError.Add(oUserData.GetErrors().ToString());
                    return false;
                }

                return true;

            }

            return false;
        }

        public string getFullName()
        {
            if (this.oDetails != null)
            {
                return this.oDetails.FirstName + " " + this.oDetails.LastName;
            }

            return "";
        }

        public void GetByMail(string cUserMail)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cUserName", cUserMail);
            DataTable oUserData = DBConn.ExecuteCommand("sp_User_GetByUsername", infoParameters).Tables[0];

            if (oUserData.Rows.Count > 0)
            {
                this.MapFromTableRow(oUserData.Rows[0]);
            }

            if (this.isValid)
            {
                this.oDetails = new UserDetails(this.Id);
            }
        }
        
    }
}