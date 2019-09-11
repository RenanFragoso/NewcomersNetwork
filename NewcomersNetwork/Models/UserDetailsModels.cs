using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NewcomersNetworkAPI.Providers;
using System.Data;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using NewcomersNetworkAPI.Models.Image;
using NewcomersNetworkAPI.Exceptions;
using System.Collections.Specialized;

namespace NewcomersNetworkAPI.Models
{
    public class UserDetails : NNAPIModel
    {
        [Key]
        public string Id { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Title { get; set; } = "";
        public string Gender { get; set; } = "";
        public string MaritalStatus { get; set; } = "";
        public string AgeRange { get; set; } = "";
        public string Education { get; set; } = "";
        public string NearestIntersection { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public bool ConsentToContact { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime LastModified { get; set; } = DateTime.Now;
        public string Status { get; set; } = "";
        public bool IsImmigrant { get; set; } = false;
        public string Picture { get; set; } = "";
        public string PictureName { get; set; } = "";
        public List<UsersRoles> UserRoles { get; set; } = new List<UsersRoles>();
        public IImageFile Avatar { get; set; }

        public UserDetails()
        {
        }

        public UserDetails(NameValueCollection multipartData)
        {
            Type userDetailsType = typeof(UserDetails);
            foreach(var formField in multipartData.Keys)
            {
                /*
                var property = userDetailsType.GetProperty(formField.ToString());
                if(property != null && property.CanWrite && property.GetSetMethod(true).IsPublic)
                {
                    property.SetValue(this, multipartData.Get(formField.ToString())[0]);
                }
                */
                switch(formField.ToString())
                {
                    case "Id":
                        this.Id = multipartData.Get(formField.ToString())[0].ToString();
                        break;
                    case "FirstName":
                        this.FirstName = multipartData.Get(formField.ToString())[0].ToString();
                        break;
                    case "LastName":
                        this.LastName = multipartData.Get(formField.ToString())[0].ToString();
                        break;
                    case "Email":
                        this.Email = multipartData.Get(formField.ToString())[0].ToString();
                        break;
                    case "Gender":
                        this.Gender = multipartData.Get(formField.ToString())[0].ToString();
                        break;
                    case "MaritalStatus":
                        this.MaritalStatus = multipartData.Get(formField.ToString())[0].ToString();
                        break;
                    case "AgeRange":
                        this.AgeRange = multipartData.Get(formField.ToString())[0].ToString();
                        break;
                    case "Education":
                        this.Education = multipartData.Get(formField.ToString())[0].ToString();
                        break;
                    case "NearestIntersection":
                        this.NearestIntersection = multipartData.Get(formField.ToString())[0].ToString();
                        break;
                    case "PostalCode":
                        this.PostalCode = multipartData.Get(formField.ToString())[0].ToString();
                        break;
                    case "ConsentToContact":
                        this.ConsentToContact = (multipartData.Get(formField.ToString())[0]).Equals(true);
                        break;
                    case "IsImmigrant":
                        this.ConsentToContact = (multipartData.Get(formField.ToString())[0]).Equals(true);
                        break;
                }
            }

            this.Validate();
        }


        public UserDetails(string userID)
        {
            UsersRoles oRole;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cUserId", userID);
            DataTable userData = DBConn.ExecuteCommand("sp_UserDetails_Get", infoParameters).Tables[0];
            
            //Gets the user details from the auxiliary table
            if (userData.Rows.Count > 0)
            {
                this.MapFromTableRow(userData.Rows[0]);
            }

            //Tries to get the Roles for the user
            if (this.isValid)
            {
                infoParameters.Clear();
                infoParameters.Add("cUserId", this.Id.ToString());
                userData = DBConn.ExecuteCommand("sp_Roles_GetUsrRoles", infoParameters).Tables[0];

                if (userData.Rows.Count > 0)
                {
                    foreach (DataRow row in userData.Rows)
                    {
                        oRole = new UsersRoles(row["RoleId"].ToString());
                        if (oRole.isValid)
                        {
                            this.UserRoles.Add(oRole);
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
                this.Picture = row["Picture"].ToString();
                this.PictureName = row["Picture"].ToString();
            }
            catch (Exception e)
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  UserDetails.MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                return;
            }

            ImageControl ImgCtrl = new ImageControl();
            //this.Picture = this.AdjustImageFile("users", this.Id);
            this.Picture = ImageContainer.RetrieveImagePath("users", this.Picture);
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
            infoParameters.Add("cStatus", (this.Status != "" ? this.Status : "O"));

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

        public bool UpdateFromForm(UserDetailsForm updateForm)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("Id", this.Id);
            infoParameters.Add("FirstName", updateForm.FirstName);
            infoParameters.Add("LastName", updateForm.LastName);
            infoParameters.Add("Email", updateForm.Email);
            infoParameters.Add("Gender", updateForm.Gender);
            infoParameters.Add("MaritalStatus", updateForm.MaritalStatus);
            infoParameters.Add("AgeRange", updateForm.AgeRange);
            infoParameters.Add("Education", updateForm.Education);
            infoParameters.Add("NearestIntersection", updateForm.NearestIntersection);
            infoParameters.Add("PostalCode", updateForm.PostalCode);
            infoParameters.Add("ConsentToContact", updateForm.ConsentToContact);
            infoParameters.Add("IsImmigrant", updateForm.IsImmigrant);

            try
            {
                DataTable oUserData = DBConn.ExecuteCommand("sp_UserDetails_Update", infoParameters).Tables[0];
                if (oUserData.HasErrors)
                {
                    this.sMsgError.Add(oUserData.GetErrors().ToString());
                    throw new Exception(oUserData.GetErrors().ToString());
                }

                return true;
            }
            catch (Exception error)
            {
                this.sMsgError.Add(error.Message);
                throw error;
            }
        }

        public async Task<bool> UpdateAvatar(ImageFile imageFile)
        {
            DateTime dDateNow = DateTime.Now;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            ImageControl ImgCtrl = new ImageControl();

            infoParameters.Add("Id", this.Id);
            infoParameters.Add("pictureName", imageFile.FileName);

            var imDelTsk = ImgCtrl.DeleteImageAsync("users", this.Picture);
            var imgUpdTsk = ImgCtrl.UploadImageAsync(imageFile);
            var imgUpd = false;
            var imgDel = false;
            var tableUpd = false;

            try
            {
                DataTable oUserData = DBConn.ExecuteCommand("sp_UserDetails_UpdateAvatar", infoParameters).Tables[0];
                if (oUserData.HasErrors)
                {
                    this.sMsgError.Add(oUserData.GetErrors().ToString());
                    tableUpd=false;
                }

                tableUpd = true;
                imgDel = await imDelTsk;
                imgUpd = await imgUpdTsk;
            }
            catch (Exception error)
            {
                this.sMsgError.Add(error.Message);
                return false;
            }

            return (imgUpd && tableUpd);
        }


        public string getFullName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public override void ValidOrBreak()
        {
            if (!this.Validate())
            {
                throw new InvalidModelException(sMsgError.ToArray());
            }
        }
    }

    public class UserSimple
    {
        public string Id { get; set; } = "";
        public string cName { get; set; } = "";
        public string cTitle { get; set; } = "";
        public string cPicture { get; set; } = "";

        public UserSimple()
        {
        }

        public UserSimple(string cUserId)
        {
            UserDetails oDetails = new UserDetails(cUserId);
            if (oDetails.isValid)
            {
                this.Id = oDetails.Id;
                this.cName = oDetails.getFullName();
                this.cTitle = oDetails.Title;
                this.cPicture = oDetails.Picture;
            }
        }
    }

    public class RegisterDetail : UserDetails
    {
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class UserDetailsForm
    {
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string AgeRange { get; set; }
        public string Education { get; set; }
        public string NearestIntersection { get; set; }
        public string PostalCode { get; set; }
        public bool ConsentToContact { get; set; }
        public bool IsImmigrant { get; set; }
        public string Captcha { get; set; } = "";

        public UserDetailsForm()
        {
        }

        public UserDetailsForm(UserDetails userDetails)
        {
            this.Id = userDetails.Id;
            this.FirstName = userDetails.FirstName;
            this.LastName = userDetails.LastName;
            this.Email = userDetails.Email;
            this.Gender = userDetails.Gender;
            this.MaritalStatus = userDetails.MaritalStatus;
            this.AgeRange = userDetails.AgeRange;
            this.Education = userDetails.Education;
            this.NearestIntersection = userDetails.NearestIntersection;
            this.PostalCode = userDetails.PostalCode;
            this.ConsentToContact = userDetails.ConsentToContact;
            this.IsImmigrant = userDetails.IsImmigrant;
        }

    }

    public class UserDetailsViewModel
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Gender { get; set; } = "";
        public string MaritalStatus { get; set; } = "";
        public string AgeRange { get; set; } = "";
        public string Education { get; set; } = "";
        public string NearestIntersection { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public bool ConsentToContact { get; set; } = false;
        public bool IsImmigrant { get; set; } = false;

        public UserDetailsViewModel()
        {
        }

        public UserDetailsViewModel(UserDetails details)
        {
            OptionsLists options = new OptionsLists();
            options.LoadListGroup("user");

            this.FirstName = details.FirstName;
            this.LastName = details.LastName;
            this.Email = details.Email;
            this.NearestIntersection = details.NearestIntersection;
            this.PostalCode = details.PostalCode;
            this.ConsentToContact = details.ConsentToContact;
            this.IsImmigrant = details.IsImmigrant;

            this.Gender = options.getValue("gender", details.Gender);
            this.MaritalStatus = options.getValue("maritalstatus", details.MaritalStatus);
            this.AgeRange = options.getValue("agerange", details.AgeRange);
            this.Education = options.getValue("education", details.Education);
        }
    }

    public static class UserDetailsService
    {
        public static string RetFullName(string userId)
        {
            var user = new UserDetails(userId);
            return user.getFullName();
        }
    }
}