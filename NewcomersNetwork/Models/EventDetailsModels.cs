using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Providers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NewcomersNetworkAPI.Models
{
    public class EventDetails : NNAPIModel
    {

        [Key]
        public string Id { get; set; } = "";
        public string EventId { get; set; } = "";
        public string Title { get; set; } = "";
        public string SubTitle { get; set; } = "";
        public string Text1 { get; set; } = "";
        public string Text2 { get; set; } = "";
        public string Footer { get; set; } = "";
        public string HeadImg { get; set; } = "";
        public string Location { get; set; } = "";

        public EventDetails()
        {
        }

        public EventDetails(string cEventId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cEventId", cEventId);
            DataTable oDetailDB = DBConn.ExecuteCommand("sp_EventDetail_GetByEvntID", infoParameters).Tables[0];

            if (oDetailDB.Rows.Count > 0)
            {
                this.MapFromTableRow(oDetailDB.Rows[0]);
            }

        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = row["Id"].ToString();
                this.EventId = row["EventId"].ToString();
                this.Title = row["Title"].ToString();
                this.SubTitle = row["SubTitle"].ToString();
                this.Text1 = row["Text1"].ToString();
                this.Text2 = row["Text2"].ToString();
                this.Footer = row["Footer"].ToString();
                this.HeadImg = row["HeadImg"].ToString();
                this.Location = row["Location"].ToString();
            }
            catch (Exception e)
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  MapFromTableRow.");
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
                if (this.EventId != null && this.EventId.Length > 0)
                {
                    this.isValid = true;
                    return true;
                }
                else
                {
                    this.isValid = false;
                    this.sMsgError.Add("Invalid Event Id.");
                }
            }

            this.isValid = false;
            this.sMsgError.Add("Invalid Id.");
            return false;
            
        }

        public override bool Save()
        {

            DataSet oInsertCMD;
            DataTable oDetailDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            if (this.Validate())
            {
                infoParameters.Add("cEventId", this.EventId);
                infoParameters.Add("cTitle", this.Title);
                infoParameters.Add("cSubTitle", this.SubTitle);
                infoParameters.Add("cText1", this.Text1);
                infoParameters.Add("cText2", this.Text2);
                infoParameters.Add("cFooter", this.Footer);
                infoParameters.Add("cHeadImg", this.HeadImg);
                infoParameters.Add("cLocation", this.Location);


                oInsertCMD = DBConn.ExecuteCommand("sp_EventDetail_Insert", infoParameters);
                oDetailDB = oInsertCMD.Tables[0];

                if (oDetailDB.Rows[0]["LAST_DETAIL"] != null)
                {
                    this.Id = oDetailDB.Rows[0]["LAST_DETAIL"].ToString();
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error saving Detail.");
                    return false;
                }

            }
            else
            {
                this.sMsgError.Add("Error saving Detail.");
                return false;
            }

        }

        public override bool Update()
        {
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            if (this.Validate())
            {
                infoParameters.Add("cId", this.Id);
                infoParameters.Add("cEventId", this.EventId);
                infoParameters.Add("cTitle", this.Title);
                infoParameters.Add("cSubTitle", this.SubTitle);
                infoParameters.Add("cText1", this.Text1);
                infoParameters.Add("cText2", this.Text2);
                infoParameters.Add("cFooter", this.Footer);
                infoParameters.Add("cHeadImg", this.HeadImg);
                infoParameters.Add("cLocation", this.Location);
                oServicesDB = DBConn.ExecuteCommand("sp_EventDetail_Update", infoParameters).Tables[0];

                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating Detail.");
                return false;
            }

        }

        public override bool Delete()
        {
            DataTable oDetailDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            if (this.Validate())
            {
                infoParameters.Add("cId", this.Id);
                oDetailDB = DBConn.ExecuteCommand("sp_EventDetail_Delete", infoParameters).Tables[0];

                return true;
            }
            else
            {
                this.sMsgError.Add("Error Deleting Detail.");
                return false;
            }
        }
    }

    public class EventDetailsView
    {
        private string Id { get; set; } = "";
        private string EventId { get; set; } = "";
        public string Title { get; set; } = "";
        public string SubTitle { get; set; } = "";
        public string Text1 { get; set; } = "";
        public string Text2 { get; set; } = "";
        public string Footer { get; set; } = "";
        public string HeadImg { get; set; } = "";
        public string Location { get; set; } = "";

        public EventDetailsView()
        {
        }

        public EventDetailsView(DataRow detailsRow)
        {
            this.Id = detailsRow["Id"].ToString();
            this.EventId = detailsRow["EventId"].ToString();
            this.Title = detailsRow["Title"].ToString();
            this.SubTitle = detailsRow["SubTitle"].ToString();
            this.Text1 = detailsRow["Text1"].ToString();
            this.Text2 = detailsRow["Text2"].ToString();
            this.Footer = detailsRow["Footer"].ToString();
            this.HeadImg = detailsRow["HeadImg"].ToString();
            this.Location = detailsRow["Location"].ToString();
        }

        public EventDetailsView(EventDetails eventDetails)
        {
            this.Id = eventDetails.Id;
            this.EventId = eventDetails.EventId;
            this.Title = eventDetails.Title;
            this.SubTitle = eventDetails.SubTitle;
            this.Text1 = eventDetails.Text1;
            this.Text2 = eventDetails.Text2;
            this.Footer = eventDetails.Footer;
            this.HeadImg = eventDetails.HeadImg;
            this.Location = eventDetails.Location;
        }
    }
}