using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NewcomersNetworkAPI.Providers;
using System.Data;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class UserDetails : NNAPIModel
    {
        [Key]
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

        [Display(Name = "Create Date")]
        [UIHint("Create Date")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Display(Name = "Last Modified")]
        [UIHint("Last Modified")]
        public DateTime LastModified { get; set; } = DateTime.Now;

        [Display(Name = "Status")]
        [UIHint("_Status")]
        public string Status { get; set; } = "";

        [Display(Name = "I Am a Newcomer")]
        [UIHint("Are you a newcomer?")]
        public bool IsImmigrant { get; set; } = false;
        public string Picture { get; set; } = "";

        public List<UsersRoles> oUserRoles { get; set; } = new List<UsersRoles>();

        public UserDetails()
        {
        }

        public UserDetails(string cUserID)
        {
            UsersRoles oRole;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cUserId", cUserID);
            DataTable oUserData = DBConn.ExecuteCommand("sp_UserDetails_Get", infoParameters).Tables[0];
            
            //Gets the user details from the auxiliary table
            if (oUserData.Rows.Count > 0)
            {
                this.MapFromTableRow(oUserData.Rows[0]);
            }

            //Tries to get the Roles for the user
            if (this.isValid)
            {
                infoParameters.Clear();
                infoParameters.Add("cUserId", this.Id.ToString());
                oUserData = DBConn.ExecuteCommand("sp_Roles_GetUsrRoles", infoParameters).Tables[0];

                if (oUserData.Rows.Count > 0)
                {
                    foreach (DataRow row in oUserData.Rows)
                    {
                        oRole = new UsersRoles(row["RoleId"].ToString());
                        if (oRole.isValid)
                        {
                            this.oUserRoles.Add(oRole);
                        }
                    }
                }
            }
            
        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = row["Id"].ToString();
                this.FirstName = row["FirstName"].ToString();
                this.LastName = row["LastName"].ToString();
                this.Email = row["Email"].ToString();
                this.Gender = row["Gender"].ToString();
                this.MaritalStatus = row["MaritalStatus"].ToString();
	            this.AgeRange = row["AgeRange"].ToString();
                this.Education = row["Education"].ToString();
                this.NearestIntersection = row["NearestIntersection"].ToString();
                this.PostalCode = row["PostalCode"].ToString();
                this.ConsentToContact = (bool) row["ConsentToContact"];
                this.DateCreated = (DateTime) row["DateCreated"];
                this.LastModified = (DateTime) row["LastModified"];
                this.Status = row["Status"].ToString();
                this.IsImmigrant = (bool) row["IsImmigrant"];
            }
            catch (Exception e)
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  UserDetails.MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                return;
            }

            ImageControl oImgCtrl = new ImageControl();
            //this.Picture = this.AdjustImageFile("users", this.Id);
            this.Picture = oImgCtrl.RetrieveImage("users", this.Id);
            this.Validate();
            
        }
        public override bool Validate()
        {
            //Object structure validation
            if (this.Id.Length == 0 || this.Id == null)
            {
                this.isValid = false;
                this.sMsgError.Add("Invalid User ID or details not found.");
                return false;
            }

            this.isValid = true;
            return true;
        }
        public override bool Delete()
        {
            this.sMsgError.Add("DELETE Not implemented.");
            return false;
        }
        public override bool Save()
        {

            DateTime dDateNow = DateTime.Now;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            infoParameters.Add("cFirstName", this.FirstName);
            infoParameters.Add("cLastName", this.LastName);
            infoParameters.Add("cEmail", this.Email);
            infoParameters.Add("cTitle", this.Title);
            infoParameters.Add("cGender", this.Gender);
            infoParameters.Add("cMaritalStatus", this.MaritalStatus);
            infoParameters.Add("cAgeRange", this.AgeRange);
            infoParameters.Add("cEducation", this.Education);
            infoParameters.Add("cNearestIntersection", this.NearestIntersection);
            infoParameters.Add("cPostalCode", this.PostalCode);
            infoParameters.Add("bConsentToContact", this.ConsentToContact);
            infoParameters.Add("dCreate", dDateNow);
            infoParameters.Add("dModify", dDateNow);
            infoParameters.Add("bIsImmigrant", this.IsImmigrant);
            infoParameters.Add("cStatus", "O");

            DataTable oUserData = DBConn.ExecuteCommand("sp_UserDetails_Create", infoParameters).Tables[0];

            if (oUserData.HasErrors)
            {
                this.sMsgError.Add(oUserData.GetErrors().ToString());
                return false;
            }
            
            return true;
        }
        public override bool Update()
        {
            DateTime dDateNow = DateTime.Now;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            infoParameters.Add("cFirstName", this.FirstName);
            infoParameters.Add("cLastName", this.LastName);
            infoParameters.Add("cEmail", this.Email);
            infoParameters.Add("cTitle", this.Title);
            infoParameters.Add("cGender", this.Gender);
            infoParameters.Add("cMaritalStatus", this.MaritalStatus);
            infoParameters.Add("cAgeRange", this.AgeRange);
            infoParameters.Add("cEducation", this.Education);
            infoParameters.Add("cNearestIntersection", this.NearestIntersection);
            infoParameters.Add("cPostalCode", this.PostalCode);
            infoParameters.Add("bConsentToContact", this.ConsentToContact);
            infoParameters.Add("dModify", dDateNow);
            infoParameters.Add("bIsImmigrant", this.IsImmigrant);

            try
            {
                DataTable oUserData = DBConn.ExecuteCommand("sp_UserDetails_Update", infoParameters).Tables[0];
                if (oUserData.HasErrors)
                {
                    this.sMsgError.Add(oUserData.GetErrors().ToString());
                    return false;
                }

                return true;
            }
            catch (Exception error)
            {
                this.sMsgError.Add(error.Message);
                return false;
            }

        }
        
        public string getFullName()
        {
            return this.FirstName + " " + this.LastName;
        }

    }

    public class UserSimple
    {
        public string cName { get; set; } = "";
        public string cTitle { get; set; } = "";

        public UserSimple()
        {
        }

        public UserSimple(string cUserId)
        {
            UserDetails oDetails = new UserDetails(cUserId);
            if (oDetails.isValid)
            {
                this.cName = oDetails.getFullName();
                this.cTitle = oDetails.Title;
            }
        }
    }


}