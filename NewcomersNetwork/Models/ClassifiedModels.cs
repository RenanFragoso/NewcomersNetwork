using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Providers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using NewcomersNetworkAPI.Exceptions;
using NewcomersNetworkAPI.ValidationAttributes;

namespace NewcomersNetworkAPI.Models
{
    public class Classified : NNAPIModel
    {

        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        public string CreatedBy { get; set; } = "";
        public string Category { get; set; } = "";
        private string type;
        public string Type {
            get { return this.type; }
            set { this.type = value.ToUpper(); }
        }
        public string Image { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime AlterDate { get; set; } = DateTime.Now;

        //public List<ClassifiedMessage> oMessages { get; set; } = new List<ClassifiedMessage>();
        public ClassifiedMessages oMessages { get; set; } = new ClassifiedMessages();

        public ClsGrpSimple oCategory { get; set; } = new ClsGrpSimple();
        public UserSimple oAuthor { get; set; } = new UserSimple();

        //public Byte[] ImageData { get; set; } = new Byte[0];
        //HttpPostedFileBase ImageData { get; set; }
        public string ImageData { get; set; } = "";

        public bool SameOwnership { get; set; } = false;

        protected CloudBlockBlob ImageBlob { get; set; }

        public Classified()
        {
        }

        public Classified(ClassifiedForm form)
        {
            this.Id = form.Id;
            this.Title = form.Title;
            this.Text = form.Text;
            this.CreatedBy = form.CreatedBy;
            this.Category = form.Category;
            this.Type = form.Type;
            this.Image = form.Id;

            this.Status = "B";
            this.CreateDate = DateTime.Now;
            this.AlterDate = DateTime.Now;
        }

        public Classified(string cId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cId", cId);
            DataTable oClassDB = DBConn.ExecuteCommand("sp_Classifieds_GetByID", infoParameters).Tables[0];

            if (oClassDB.Rows.Count > 0)
            {
                this.MapFromTableRow(oClassDB.Rows[0]);
            }
        }

        public override void MapFromTableRow(DataRow row)
        {
            ImageControl oImgCtrl = new ImageControl();

            try
            {
                this.Id = row["Id"].ToString();
                this.Title = row["Title"].ToString();
                this.Text = row["Text"].ToString();
                this.CreatedBy = row["CreatedBy"].ToString();
                this.Category = row["Category"].ToString();
                this.Type = row["Type"].ToString();
                this.Image = row["Id"].ToString();
                this.Status = row["Status"].ToString();
                this.CreateDate = (DateTime) row["CreateDate"];
                this.AlterDate = (DateTime) row["AlterDate"];
            }
            catch (Exception e)
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  Event.MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                return;
            }

            this.Validate();
            this.Image = oImgCtrl.RetrieveImage("classifieds", this.Image);
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
            this.sMsgError.Add("Invalid Classified Id.");
            return false;
        }

        public override async Task<bool> SaveAsync()
        {
            return this.Save();
        }

        public override bool Save()
        {
            DateTime dNow = DateTime.Now;
            DataTable oClassDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("Id", this.Id);
            infoParameters.Add("cTitle", this.Title);
            infoParameters.Add("cText", this.Text);
            infoParameters.Add("cCreatedBy", this.CreatedBy);
            infoParameters.Add("cCategory", this.Category);
            infoParameters.Add("cType", this.Type);
            infoParameters.Add("cImage", this.Image);
            infoParameters.Add("dCreateDate", dNow);

            try
            {
                oClassDB = DBConn.ExecuteCommand("sp_Classifieds_Insert", infoParameters).Tables[0];

                if (!oClassDB.HasErrors && oClassDB.Rows[0]["LAST_CLASSIFIED"] != null)
                {
                    this.Id = oClassDB.Rows[0]["LAST_CLASSIFIED"].ToString();
                    this.AlterDate = dNow;
                    this.CreateDate = dNow;
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error Saving JustShare.");
                    throw new Exception("Error Saving JustShare.");
                }
            }
            catch (Exception e)
            {
                this.sMsgError.Add("Error Saving JustShare.");
                this.sMsgError.Add(e.Message);
                throw new Exception("Error Saving JustShare.");
            }
        }

        public override bool Update()
        {
            DateTime dNow = DateTime.Now;
            DataTable oClassDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            Byte[] oImageBytes;
            int nIndex = 0;
            string cRegexp = @"\/(.*)";
            string contentType = "image/";
            Match oMatch;
            ImageControl oImgCtrl = new ImageControl();
            ImageFile oImage;

            if (this.ImageData != null && this.ImageData.Length > 0)
            {
                //Image upload to BLOB Azure Storage
                nIndex = this.ImageData.IndexOf(";base64,");
                if(nIndex >= 0)
                {
                    try
                    {
                        oMatch = Regex.Match(this.ImageData.Substring(0,nIndex),cRegexp);
                        oImageBytes = Convert.FromBase64String(this.ImageData.Substring(nIndex + 8));
                        contentType += oMatch.Value.ToString().Replace("/", "").Replace(";", "");

                        //oImgCtrl = new ImageControl();
                        //oImgCtrl.UploadImage("classifieds",this.Id, oImageBytes, contentType);

                        infoParameters.Add("cImage", this.Id);
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
                else
                {
                    infoParameters.Add("cImage", "square-image.png"); //Default Image for Classifieds
                }
            }
            else
            {
                infoParameters.Add("cImage", "square-image.png"); //Default Image for Classifieds
            }

            infoParameters.Add("cId", this.Id);
            infoParameters.Add("cTitle", this.Title);
            infoParameters.Add("cText", this.Text);
            infoParameters.Add("cCreatedBy", this.CreatedBy);
            infoParameters.Add("cCategory", this.Category);
            infoParameters.Add("cType", this.Type);
            infoParameters.Add("dAlterDate", dNow);

            try
            {
                oClassDB = DBConn.ExecuteCommand("sp_Classifieds_Update", infoParameters).Tables[0];
                if (!oClassDB.HasErrors)
                {
                    if (!oClassDB.HasErrors)
                    {
                        this.AlterDate = dNow;
                        //this.Image = this.AdjustImageFile("classifieds", this.Image);
                        this.Image = oImgCtrl.RetrieveImage("classifieds", this.Image);
                        return true;
                    }
                    else
                    {
                        this.sMsgError.Add("Error Updating Classified.");
                    }
                }
            }
            catch (Exception e)
            {
                this.sMsgError.Add("Error Updating Classified.");
                this.sMsgError.Add(e.Message);
            }
            return false;
        }
        public override bool Delete()
        {
            DateTime dNow = DateTime.Now;
            DataTable oClassDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            try
            {
                oClassDB = DBConn.ExecuteCommand("sp_Classifieds_Delete", infoParameters).Tables[0];
                if (!oClassDB.HasErrors)
                {
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error Deleting Classified.");
                }

            }
            catch (Exception e)
            {
                this.sMsgError.Add("Error Deleting Classified.");
                this.sMsgError.Add(e.Message);
            }
            return false;
        }

        public bool Approve()
        {
            DateTime dNow = DateTime.Now;
            DataTable oClassDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            infoParameters.Add("dAlterDate", dNow);

            try
            {
                oClassDB = DBConn.ExecuteCommand("sp_Classifieds_Approve", infoParameters).Tables[0];
                if (!oClassDB.HasErrors)
                {
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error on Classified Approval.");
                }

            }
            catch (Exception e)
            {
                this.sMsgError.Add("Error on Classified Approval.");
                this.sMsgError.Add(e.Message);
            }
            return false;
        }

        public bool Reject()
        {
            DateTime dNow = DateTime.Now;
            DataTable oClassDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            infoParameters.Add("dAlterDate", dNow);

            try
            {
                oClassDB = DBConn.ExecuteCommand("sp_Classifieds_Reject", infoParameters).Tables[0];
                if (!oClassDB.HasErrors)
                {
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error on Classified Rejection.");
                }

            }
            catch (Exception e)
            {
                this.sMsgError.Add("Error on Classified Rejection.");
                this.sMsgError.Add(e.Message);
            }
            return false;
        }

        public void LoadMessages(string cFrom = "", string cTo = "")
        {
            string cProcedure = "";
            ClassifiedMessage oMessage;
            DataTable oMessagesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cClassifiedId", this.Id);

            if (cFrom.Length == 0 && cTo.Length == 0)
            {
                cProcedure = "sp_Messages_GetAll";
            }
            else if (cFrom.Length == 0 && cTo.Length > 0)
            {
                cProcedure = "sp_Messages_GetClsTo";
                infoParameters.Add("cTo", cTo);
            }
            else if (cFrom.Length > 0 && cTo.Length == 0)
            {
                cProcedure = "sp_Messages_GetClsFrom";
                infoParameters.Add("cFrom", cFrom);
            }
            else
            {
                cProcedure = "sp_Messages_GetClsFromTo";
                infoParameters.Add("cFrom", cFrom);
                infoParameters.Add("cTo", cTo);
            }

            try
            {
                oMessagesDB = DBConn.ExecuteCommand(cProcedure, infoParameters).Tables[0];
                if (!oMessagesDB.HasErrors)
                {
                    foreach (DataRow oRow in oMessagesDB.Rows)
                    {
                        oMessage = new ClassifiedMessage();
                        oMessage.MapFromTableRow(oRow);
                        if (oMessage.isValid)
                        {
                            this.oMessages.Add(oMessage);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                this.sMsgError.Add("Error getting messages.");
                this.sMsgError.Add(e.Message);
            }
        }
        
        public void LoadMessagesFrom(string cFrom) 
        {
            ClassifiedMessage oMessage;
            DataTable oMessagesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cClassifiedId", this.Id);
            infoParameters.Add("cFrom", cFrom);

            try
            {
                oMessagesDB = DBConn.ExecuteCommand("sp_Messages_GetClsFrom", infoParameters).Tables[0];
                if (!oMessagesDB.HasErrors)
                {
                    foreach (DataRow oRow in oMessagesDB.Rows)
                    {
                        oMessage = new ClassifiedMessage();
                        oMessage.MapFromTableRow(oRow);
                        if (oMessage.isValid)
                        {
                            if(oMessage.ReplyTo > 0)
                            {
                                this.oMessages.AddReplyTo(oMessage);
                            }
                            else
                            {
                                this.oMessages.Add(oMessage);
                            }
                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                this.sMsgError.Add("Error getting messages.");
                this.sMsgError.Add(e.Message);
            }
        }
        
        public static int GetPendingNum()
        {
            DataTable oClassDB;
            try
            {
                oClassDB = DBConn.ExecuteCommand("sp_Classifieds_PendingNum", null).Tables[0];
                if (!oClassDB.HasErrors)
                {
                    return (int) oClassDB.Rows[0]["PENDING_NUM"];
                }

            }
            catch (Exception e)
            {
                return 0;
            }
            return 0;
        }

        public bool Finish()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("Id", this.Id);

            var procResult = DBConn.ExecuteCommand("sp_Classifieds_Finish", infoParameters).Tables[0];
            return !procResult.HasErrors;
        }

        public override void ValidOrBreak()
        {
            if (!this.Validate())
            {
                throw new InvalidModelException(sMsgError.ToArray());
            }
        }

        public async Task NotifyMessage(ClassifiedMessage message)
        {
            if(this.isValid)
            {
                var user = new User(this.CreatedBy);
                if(user.oDetails.ConsentToContact)
                {
                    //Send mail
                }
            }
        }

        public void CheckOwnership(string userId)
        {
            this.SameOwnership = this.CreatedBy == userId;
        }

    }

    public class ClassifiedForm
    {
        [Required]
        public string Id { get; set; } = "";
        [Required]
        [CheckBlacklistedWords]
        public string Title { get; set; } = "";
        [Required]
        [CheckBlacklistedWords]
        public string Text { get; set; } = "";
        [Required]
        public string Category { get; set; } = "";
        [Required]
        public string CreatedBy { get; set; } = "";
        [Required]
        public string Type { get; set; } = "";

        public ClassifiedForm()
        {
        }

        public ClassifiedForm(NameValueCollection formData)
        {
            foreach (var key in formData.AllKeys)
            {
                switch (key.ToString())
                {
                    case "Id":
                        this.Id = formData[key].ToString();
                        break;

                    case "Title":
                        this.Title = formData[key].ToString();
                        break;

                    case "Text":
                        this.Text = formData[key].ToString();
                        break;

                    case "Category":
                        this.Category = formData[key].ToString();
                        break;

                    case "CreatedBy":
                        this.CreatedBy = formData[key].ToString();
                        break;

                    case "Type":
                        this.Type = formData[key].ToString();
                        break;
                }
            }
        }
    }
}