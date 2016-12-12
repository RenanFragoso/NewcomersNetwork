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
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public int ServiceResponsible { get; set; }

        [JsonIgnore]
        IList<ServicesSchedule> ServiceSchedule;

        [JsonIgnore]
        public bool isValid { get; private set; }
        [JsonIgnore]
        public List<string> sMsgError { get; set; }

        public Service()
        {
            //Default Ctor
        }

        public Service(int nServiceID)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("ServiceIDIn", nServiceID);
            DataTable oServiceDB = DBConn.ExecuteCommand("sp_Services_GetByID", infoParameters).Tables[0];

            if (oServiceDB.Rows.Count > 0)
            {
                this.MapFromTableRow(oServiceDB.Rows[0]);
            }

            if (this.Validate())
            {
                this.isValid = true;
            }
        }

        public bool Validate()
        {
            //Object structure validation
            if (this.ServiceID == 0)
            {
                this.isValid = false;
                this.sMsgError.Add("Invalid EventID.");
                return false;
            }

            this.isValid = true;
            return true;
        }

        public void MapFromTableRow(DataRow row)
        {
            try
            {
                this.ServiceID = (int)row["ServiceID"];
                this.ServiceName = row["ServiceName"].ToString();
                this.ServiceDescription = row["ServiceDescription"].ToString();
                this.ServiceResponsible = (int)row["ServiceResponsible"];

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

        public bool Save()
        {

            DataSet oInsertCMD;
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            if (this.Validate())
            {
                infoParameters.Add("ServiceName", this.ServiceName);
                infoParameters.Add("ServiceDescription", this.ServiceDescription);
                infoParameters.Add("ServiceResponsible", this.ServiceResponsible);

                oInsertCMD = DBConn.ExecuteCommand("sp_Services_Insert", infoParameters);
                oServicesDB = oInsertCMD.Tables[0];

                if (oServicesDB.Rows[0]["LAST_SERVICE"] != null)
                {
                    this.ServiceID = Convert.ToInt32(oServicesDB.Rows[0]["LAST_SERVICE"]);
                    return true;
                }
                else
                {
                    this.sMsgError.Add("Error Saving Service.");
                    return false;
                }

            }
            else
            {
                this.sMsgError.Add("Error Saving Service.");
                return false;
            }

        }

        public bool Update()
        {
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            if (this.Validate())
            {
                infoParameters.Add("ServiceID", this.ServiceID);
                infoParameters.Add("ServiceName", this.ServiceName);
                infoParameters.Add("ServiceDescription", this.ServiceDescription);
                infoParameters.Add("ServiceResponsible", this.ServiceResponsible);

                oServicesDB = DBConn.ExecuteCommand("sp_Services_Update", infoParameters).Tables[0];

                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating event.");
                return false;
            }

        }

        public bool Delete()
        {
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            if (this.Validate())
            {
                infoParameters.Add("ServiceID", this.ServiceID);
                oServicesDB = DBConn.ExecuteCommand("sp_Services_Delete", infoParameters).Tables[0];

                return true;
            }
            else
            {
                this.sMsgError.Add("Error Deleting event.");
                return false;
            }

        }

    }
}