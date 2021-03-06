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

namespace NewcomersNetworkAPI.Models
{
    public class Classified : NNAPIModel
    {

        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        public string CreatedBy { get; set; } = "";
        public string Category { get; set; } = "";
        public string Type { get; set; } = "";
        public string Image { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime AlterDate { get; set; } = DateTime.Now;

        public ClsGrpSimple oCategory { get; set; }
        public UserSimple oAuthor { get; set; }

        //public Byte[] ImageData { get; set; } = new Byte[0];
        //HttpPostedFileBase ImageData { get; set; }
        public string ImageData { get; set; } = "";

        protected CloudBlockBlob ImageBlob { get; set; }

        public Classified()
        {
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
                this.Image = row["Image"].ToString();
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
            this.Image = oImgCtrl.RetrieveImage("classifieds", this.Image); //base.AdjustImageFile("classifieds", this.Image);
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

        public override bool Save()
        {
            ImageControl oImgCtrl = new ImageControl();
            DateTime dNow = DateTime.Now;
            DataTable oClassDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            
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

                if (!oClassDB.HasErrors && oClassDB.Rows[0]["LAST_SERVICE"] != null)
                {
                    this.Id = oClassDB.Rows[0]["LAST_CLASSIFIED"].ToString();
                    this.AlterDate = dNow;
                    this.CreateDate = dNow;
                    this.Image = oImgCtrl.RetrieveImage("classifieds", this.Image); //base.AdjustImageFile("classifieds", this.Image);
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error Saving Classified.");
                }
            }
            catch (Exception e)
            {
                this.sMsgError.Add("Error Saving Classified.");
                this.sMsgError.Add(e.Message);
            }
            return false;
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

    }
}