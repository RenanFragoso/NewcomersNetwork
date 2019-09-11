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
    public class Testimonial : NNAPIModel
    {
        [Key]
        public string Id { get; set; } = "";
        public string Author { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string AuthorTitle { get; set; } = "";
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Content { get; set; } = "";
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime AlterDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "B";

        public Testimonial()
        {
        }

        public Testimonial(string testimonialId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("testimonialId", testimonialId);
            DataTable oEventDB = DBConn.ExecuteCommand("sp_Testimonials_GetByID", infoParameters).Tables[0];

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
                this.Author = row["Author"].ToString();
                this.AuthorTitle = row["AuthorTitle"].ToString();
                this.Content = row["Content"].ToString();
                this.CreateDate = (DateTime)row["StartDate"];
                this.AlterDate = (DateTime)row["StartDate"];
                this.Status = row["Status"].ToString();
            }
            catch ( Exception e )
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  Testimonial.MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                return;
            }
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
            this.sMsgError.Add("Invalid TestimonialId.");
            return false;
        }

        public override bool Save()
        {
            DataSet insertCmd;
            DataTable testimonialDb;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            DateTime dNow = DateTime.Now;

            infoParameters.Add("Author", this.Author);
            infoParameters.Add("AuthorTitle", this.AuthorTitle);
            infoParameters.Add("Content", this.Content);
            infoParameters.Add("Status", this.Status);
            infoParameters.Add("CreateDate", dNow);

            try
            {
                insertCmd = DBConn.ExecuteCommand("sp_Testimonial_Insert", infoParameters);
                testimonialDb = insertCmd.Tables[0];

                if (testimonialDb.Rows[0]["LAST_TESTIMONIAL"] != null)
                {
                    this.Id = testimonialDb.Rows[0]["LAST_TESTIMONIAL"].ToString();
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error Saving Testimonial.");
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
            DataSet updateCmd;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            DateTime dNow = DateTime.Now;

            infoParameters.Add("Id", this.Id);
            infoParameters.Add("Author", this.Author);
            infoParameters.Add("AuthorTitle", this.AuthorTitle);
            infoParameters.Add("Content", this.Content);
            infoParameters.Add("Status", this.Status);
            infoParameters.Add("AlterDate", dNow);

            try
            {
                updateCmd = DBConn.ExecuteCommand("sp_Testimonial_Update", infoParameters);
                return true;
            }
            catch (Exception e)
            {
                this.sMsgError.Add(e.Message);
                return false;
            }
        }


        public override bool Delete()
        {
            DataSet deleteCmd;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("Id", this.Id);

            try
            {
                deleteCmd = DBConn.ExecuteCommand("sp_Testimonial_Delete", infoParameters);
                return true;
            }
            catch (Exception e)
            {
                this.sMsgError.Add(e.Message);
                return false;
            }

        }
        public bool Activate()
        {
            DataSet activateCmd;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("Id", this.Id);
            try
            {
                activateCmd = DBConn.ExecuteCommand("sp_Testimonial_Activate", infoParameters);
                return true;
            }
            catch (Exception e)
            {
                this.sMsgError.Add(e.Message);
                return false;
            }
        }

        public bool Deactivate()
        {
            DataSet deactivateCmd;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("Id", this.Id);
            try
            {
                deactivateCmd = DBConn.ExecuteCommand("sp_Testimonial_Deactivate", infoParameters);
                return true;
            }
            catch (Exception e)
            {
                this.sMsgError.Add(e.Message);
                return false;
            }
        }
    }
}