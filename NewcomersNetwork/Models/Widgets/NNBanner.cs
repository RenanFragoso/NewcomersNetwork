using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Providers;
using System.Data;
using Dapper;

namespace NewcomersNetworkAPI.Models.Widgets
{
    public class NNBanner : NNAPIModel
    {
        public string Id { get; set; } = "";
        public string Link { get; set; } = "";
        public string Title1 { get; set; } = "";
        public string Title2 { get; set; } = "";
        public string Status { get; set; } = "";
        public string Image { get; set; } = "";

        public NNBanner()
        {
        }

        public NNBanner(string id)
        {
            if(id != null)
            {
                this.Id = id;
                this.LoadFromId();
            }
        }

        private void LoadFromId()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>
            {
                { "id", this.Id }
            };

            DataTable bannersRow = DBConn.ExecuteCommand("sp_Widgets_Banners_GetById", infoParameters).Tables[0];
            if(bannersRow.Rows.Count > 0)
            {
                this.MapFromTableRow(bannersRow.Rows[0]);
            }
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
            this.sMsgError.Add("Invalid Banner.");
            return false;

        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = row["Id"].ToString();
                this.Link = row["Link"].ToString();
                this.Title1 = row["Title1"].ToString();
                this.Title2 = row["Title2"].ToString();
                this.Status = row["Status"].ToString();

                ImageControl oImgCtrl = new ImageControl();
                this.Image = oImgCtrl.RetrieveImage("banners", this.Id);

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

        public override bool Save()
        {
            DataSet oInsertCMD;
            DataTable oBannersDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cLink", this.Link);
            infoParameters.Add("cTitle1", this.Title1);
            infoParameters.Add("cTitle2", this.Title2);
            infoParameters.Add("cStatus", this.Status);

            oInsertCMD = DBConn.ExecuteCommand("sp_Settings_Banners_Insert", infoParameters);
            oBannersDB = oInsertCMD.Tables[0];

            if (!oBannersDB.HasErrors && oBannersDB.Rows[0]["LAST_BANNER"] != null)
            {
                this.Id = oBannersDB.Rows[0]["LAST_BANNER"].ToString();
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Saving Banner.");
                return false;
            }
        }

        public override bool Update()
        {
            DateTime dNow = DateTime.Now;
            DataTable oBannersDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            infoParameters.Add("cLink", this.Link);
            infoParameters.Add("cTitle1", this.Title1);
            infoParameters.Add("cTitle2", this.Title2);
            infoParameters.Add("cStatus", this.Status);

            oBannersDB = DBConn.ExecuteCommand("sp_Settings_Banners_Update", infoParameters).Tables[0];

            if (!oBannersDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating Banners.");
                return false;
            }
        }

        public override bool Delete()
        {
            DataTable oBannersDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            oBannersDB = DBConn.ExecuteCommand("sp_Settings_Banners_Delete", infoParameters).Tables[0];

            if (!oBannersDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Deleting Banner.");
                return false;
            }

        }
        public bool Activate()
        {
            DataTable oBannersDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            oBannersDB = DBConn.ExecuteCommand("sp_Settings_Banners_Activate", infoParameters).Tables[0];

            if (!oBannersDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Activating Banner.");
                return false;
            }
        }

        public bool Deactivate()
        {
            DataTable oBannersDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.Id);
            oBannersDB = DBConn.ExecuteCommand("sp_Settings_Banners_Deactivate", infoParameters).Tables[0];

            if (!oBannersDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Deactivating Banner.");
                return false;
            }
        }

    }
}