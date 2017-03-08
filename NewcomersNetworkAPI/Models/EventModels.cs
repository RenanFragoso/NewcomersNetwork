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
    public class Event : NNAPIModel
    {
        [Key]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Description { get; set; } = "";
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public bool Published { get; set; } = false;
        public DateTime StartPublishDate { get; set; } = DateTime.Now;
        public DateTime EndPublishDate { get; set; } = DateTime.Now;
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime AlterDate { get; set; } = DateTime.Now;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Image { get; set; } = "";
        public bool Finished { get; set; } = false;
        public int MaxSlots { get; set; } = 0;
        public int CurSlots { get; set; } = 0;
        public string CreatedBy { get; set; } = "";
        public int Type { get; set; } = 0;
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ExternalLink { get; set; } = "";
        public string Status { get; set; } = "";

        //Details
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SubTitle { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text1 { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Text2 { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Footer { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string HeadImg { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Location { get; set; } = "";

        public Byte[] ImageFile { get; set; } = new Byte[0];

        //public EventDetails oDetails { get; set; } = new EventDetails();

        public Event()
        {
        }

        public Event(string cEventID)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cId", cEventID);
            DataTable oEventDB = DBConn.ExecuteCommand("sp_Events_GetByID", infoParameters).Tables[0];

            if (oEventDB.Rows.Count > 0)
            {
                this.MapFromTableRow(oEventDB.Rows[0]);
            }

        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = row["Id"].ToString();
                this.Name = row["Name"].ToString();
                this.Description = row["Description"].ToString();
                this.StartDate = (DateTime) row["StartDate"];
                this.EndDate = (DateTime) row["EndDate"];
                this.Published = (bool) row["Published"];
                this.StartPublishDate = (DateTime) row["StartPublishDate"];
                this.EndPublishDate = (DateTime) row["EndPublishDate"];
                this.Image = row["Image"].ToString();
                this.Finished = (bool) row["Finished"];
                this.MaxSlots = (int) row["MaxSlots"];
                this.CurSlots = (int) row["CurSlots"];
                this.CreatedBy = row["CreatedBy"].ToString();
                this.Type = (int) row["Type"];
                this.ExternalLink = row["ExternalLink"].ToString();

                this.Title = row["Title"].ToString();
                this.SubTitle = row["SubTitle"].ToString();
                this.Text1 = row["Text1"].ToString();
                this.Text2 = row["Text2"].ToString();
                this.Footer = row["Footer"].ToString();
                this.HeadImg = row["HeadImg"].ToString();
                this.Location = row["Location"].ToString();

                this.CreateDate = (DateTime) row["CreateDate"];
                this.AlterDate = (DateTime) row["AlterDate"];
                this.Status = row["Status"].ToString();
            }
            catch ( Exception e )
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  Event.MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                return;
            }

            this.Validate();
            this.Image = this.AdjustImageFile("events", this.Image);
            this.HeadImg = this.AdjustImageFile("events", this.HeadImg);
        }

        public override bool Validate()
        {
            //Object structure validation
            if(this.Id != null && this.Id.Length > 0)
            {
                this.isValid = true;
                return true;
            }

            this.isValid = false;
            this.sMsgError.Add("Invalid EventID.");
            return false;
        }

        public override bool Save()
        {
            DataSet oInsertCMD;
            DataTable oEventDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            DateTime dNow = DateTime.Now;
            
            infoParameters.Add("cName", this.Name);
            infoParameters.Add("cDescription", this.Description);
            infoParameters.Add("dStartDate", this.StartDate);
            infoParameters.Add("dEndDate", this.EndDate);
            infoParameters.Add("bPublished", this.Published);
            infoParameters.Add("dStartPublishDate", this.StartPublishDate);
            infoParameters.Add("dEndPublishDate", this.EndPublishDate);
            infoParameters.Add("bFinished", this.Finished);
            infoParameters.Add("nMaxSlots", this.MaxSlots);
            infoParameters.Add("nCurSlots", this.CurSlots);
            infoParameters.Add("cImage", this.Image);
            //infoParameters.Add("cCreatedBy", this.CreatedBy);
            infoParameters.Add("cCreatedBy", "ce209094-2d37-489d-9eb5-db570b06fab5");
            infoParameters.Add("nType", this.Type);

            infoParameters.Add("cTitle", this.Title);
            infoParameters.Add("cSubTitle", this.SubTitle);
            infoParameters.Add("cText1", this.Text1);
            infoParameters.Add("cText2", this.Text2);
            infoParameters.Add("cFooter", this.Footer);
            infoParameters.Add("cHeadImg", this.HeadImg);
            infoParameters.Add("cLocation", this.Location);
            
            infoParameters.Add("dCreateDate", dNow);

            try
            {
                oInsertCMD = DBConn.ExecuteCommand("sp_Events_Insert", infoParameters);
                oEventDB = oInsertCMD.Tables[0];

                if (oEventDB.Rows[0]["LAST_EVENT"] != null)
                {
                    this.Id = oEventDB.Rows[0]["LAST_EVENT"].ToString();
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error Saving event.");
                    return false;
                }
            } 
            catch (Exception e)
            {
                this.sMsgError.Add(e.Message);
                return false;
            }
        }

        public override bool Update()
        {
            DataTable oEventsDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            infoParameters.Add("cName", this.Name);
            infoParameters.Add("cDescription", this.Description);
            infoParameters.Add("dStartDate", this.StartDate);
            infoParameters.Add("dEndDate", this.EndDate);
            infoParameters.Add("bPublished", this.Published);
            infoParameters.Add("dStartPublishDate", this.StartPublishDate);
            infoParameters.Add("dEndPublishDate", this.EndPublishDate);
            infoParameters.Add("bFinished", this.Finished);
            infoParameters.Add("nMaxSlots", this.MaxSlots);
            infoParameters.Add("nCurSlots", this.CurSlots);
            infoParameters.Add("cImage", this.Image);
            infoParameters.Add("cCreatedBy", this.CreatedBy);
            infoParameters.Add("nType", this.Type);
            infoParameters.Add("cExternalLink", this.ExternalLink);
            infoParameters.Add("cStatus", this.Status);
            infoParameters.Add("cTitle", this.Title);
            infoParameters.Add("cSubTitle", this.SubTitle);
            infoParameters.Add("cText1", this.Text1);
            infoParameters.Add("cText2", this.Text2);
            infoParameters.Add("cFooter", this.Footer);
            infoParameters.Add("cHeadImg", this.HeadImg);
            infoParameters.Add("cLocation", this.Location);

            infoParameters.Add("dAlterDate", DateTime.Now);

            try
            {
                oEventsDB = DBConn.ExecuteCommand("sp_Events_Update", infoParameters).Tables[0];
            }
            catch (Exception e)
            {
                this.sMsgError.Add(e.Message);
                return false;
            }
            
            /*
            if(this.oDetails != null && (this.oDetails.GetType() == typeof(EventDetails)))
            {
                this.oDetails.Update();
            }
            */
            return true;
        }


        public override bool Delete()
        {
            DataTable oEventDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            oEventDB = DBConn.ExecuteCommand("sp_Events_Delete", infoParameters).Tables[0];

            if (!oEventDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Deleting Event.");
                return false;
            }

        }
        public bool Activate()
        {
            DataTable oEventDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            oEventDB = DBConn.ExecuteCommand("sp_Events_Activate", infoParameters).Tables[0];

            if (!oEventDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Activating Event.");
                return false;
            }
        }

        public bool Deactivate()
        {
            DataTable oEventDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            oEventDB = DBConn.ExecuteCommand("sp_Events_Deactivate", infoParameters).Tables[0];

            if (!oEventDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Deactivating Event.");
                return false;
            }
        }


        public bool EventSign()
        {

            if(this.Status == "O" && this.CurSlots > 0)
            {
                this.CurSlots -= 1;
                this.Update();
                return true;
            }
            else
            {
                return false;
            }
            
        }
        
        public bool LoadDetails()
        {
            /*
            EventDetails oDetails;
            if (this.isValid)
            {
                oDetails = new EventDetails(this.Id);
                if (oDetails.isValid)
                {
                    this.oDetails = oDetails;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            */
            return false;
        }

    }
}