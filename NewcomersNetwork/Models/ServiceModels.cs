using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Providers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using NewcomersNetworkAPI.Models.Image;
using System.Text;

namespace NewcomersNetworkAPI.Models
{
    public class Service : NNAPIModel
    {
        [Key]
        public string ServiceId { get; set; } = "";
        public string ServiceName { get; set; } = "";
        public string ServiceDescription { get; set; } = "";
        [JsonIgnore]
        public string ServiceResponsible { get; set; } = "";
        public string ServiceGroup { get; set; } = "";
        public DateTime ServiceCreateDate { get; set; } = DateTime.Now;
        public DateTime ServiceAlterDate { get; set; } = DateTime.Now;
        public List<ServicesSchedule> ServiceSchedule { get; set; } = new List<ServicesSchedule>();
        public string ServiceStatus { get; set; } = "";
        public ServiceGroup oServiceGroup { get; set; } = new ServiceGroup();

        public Service()
        {
        }

        public Service(string cServiceId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cServiceId", cServiceId);
            DataTable oServiceDB = DBConn.ExecuteCommand("sp_Services_GetByID", infoParameters).Tables[0];

            if (oServiceDB.Rows.Count > 0)
            {
                this.MapFromTableRow(oServiceDB.Rows[0]);
            }

        }

        public override bool Validate()
        {
            //Object structure validation
            if (this.ServiceId != null && this.ServiceId.Length > 0)
            {
                this.isValid = true;
                return true;
            }

            this.isValid = false;
            this.sMsgError.Add("Invalid ServiceId.");
            return false;

        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.ServiceId = row["ServiceID"].ToString();
                this.ServiceName = row["ServiceName"].ToString();
                this.ServiceDescription = row["ServiceDescription"].ToString();
                this.ServiceResponsible = row["ServiceResponsible"].ToString();
                this.ServiceGroup = row["ServiceGroup"].ToString();
                this.ServiceStatus = row["ServiceStatus"].ToString();
                this.ServiceCreateDate = (DateTime) row["ServiceCreateDate"];
                this.ServiceAlterDate = (DateTime) row["ServiceAlterDate"];


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
            DateTime dNow = DateTime.Now;
            DataSet oInsertCMD;
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cServiceName", this.ServiceName);
            infoParameters.Add("cServiceDescription", this.ServiceDescription);
            infoParameters.Add("cServiceGroup", this.ServiceGroup);
            infoParameters.Add("dServiceCreateDate", dNow);


            oInsertCMD = DBConn.ExecuteCommand("sp_Services_Insert", infoParameters);
            oServicesDB = oInsertCMD.Tables[0];

            if (!oServicesDB.HasErrors && oServicesDB.Rows[0]["LAST_SERVICE"] != null)
            {
                this.ServiceId = oServicesDB.Rows[0]["LAST_SERVICE"].ToString();
                this.ServiceAlterDate = dNow;
                this.ServiceCreateDate = dNow;
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Saving Service.");
                return false;
            }
        }

        public override bool Update()
        {
            DateTime dNow = DateTime.Now;
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cServiceId", this.ServiceId);
            infoParameters.Add("cServiceName", this.ServiceName);
            infoParameters.Add("cServiceDescription", this.ServiceDescription);
            infoParameters.Add("cServiceGroup", this.ServiceGroup);
            infoParameters.Add("dServiceAlterDate", dNow);

            oServicesDB = DBConn.ExecuteCommand("sp_Services_Update", infoParameters).Tables[0];

            if (!oServicesDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Updating Service.");
                return false;
            }
        }

        public override bool Delete()
        {
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cServiceId", this.ServiceId);
            oServicesDB = DBConn.ExecuteCommand("sp_Services_Delete", infoParameters).Tables[0];

            if (!oServicesDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Deleting Service.");
                return false;
            }

        }
        public bool Activate()
        {
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cServiceId", this.ServiceId);
            oServicesDB = DBConn.ExecuteCommand("sp_Services_Activate", infoParameters).Tables[0];

            if (!oServicesDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Activating Service.");
                return false;
            }
        }

        public bool Deactivate()
        {
            DataTable oServicesDB;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cServiceId", this.ServiceId);
            oServicesDB = DBConn.ExecuteCommand("sp_Services_Deactivate", infoParameters).Tables[0];

            if (!oServicesDB.HasErrors)
            {
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Deactivating Service.");
                return false;
            }
        }

        public void LoadSchedule(bool bLoadItens = false)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cServiceId", this.ServiceId);
            DataTable oSchedDB = DBConn.ExecuteCommand("sp_Schedule_GetAll", infoParameters).Tables[0];

            if (oSchedDB.Rows.Count > 0)
            {
                ServicesSchedule oSchedule;
                foreach (DataRow row in oSchedDB.Rows)
                {
                    oSchedule = new ServicesSchedule();
                    oSchedule.MapFromTableRow(row);
                    if (oSchedule.isValid)
                    {
                        if (bLoadItens)
                        {
                            oSchedule.LoadFullDetails();
                        }
                        this.ServiceSchedule.Add(oSchedule);
                    }
                }
            }
        }

        public void LoadScheduleFull()
        {
            this.LoadSchedule(true);
        }

        public void LoadGroup()
        {
            if (this.ServiceGroup.Length > 0)
            {
                this.oServiceGroup = new ServiceGroup(this.ServiceGroup);
            }
        }

        public override void LoadFullDetails()
        {
            this.LoadGroup();
            this.LoadScheduleFull();
        }

    }


    public class SimpleService
    {
        private string ServiceId { get; set; } = "";
        public string ServiceName { get; set; } = "";
        public string ServiceDescription { get; set; } = "";
        public string ServiceGroup { get; set; } = "";
        public string ServiceImage { get { return ImageContainer.RetrieveImagePath("services", this.ServiceId); } }
        public string Link { get { return "/service/" + Convert.ToBase64String(Encoding.Default.GetBytes(this.ServiceId)); } }

        public SimpleService()
        {
        }

        public SimpleService(Service service)
        {
            this.ServiceId = service.ServiceId;
            this.ServiceName = service.ServiceName;
            this.ServiceDescription = service.ServiceDescription;
            this.ServiceGroup = service.ServiceGroup;
        }

        public SimpleService(DataRow row)
        {
            this.ServiceId = row["ServiceID"].ToString();
            this.ServiceName = row["ServiceName"].ToString();
            this.ServiceDescription = row["ServiceDescription"].ToString();
            this.ServiceGroup = row["ServiceGroup"].ToString();
        }
    }

    public static class ServiceService
    {
        public static SimpleService GetSimpleService(string serviceId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cServiceId", serviceId);

            try
            {
                DataTable serviceData = DBConn.ExecuteCommand("sp_Services_GetByID", infoParameters).Tables[0];
                if(serviceData.HasErrors || serviceData.Rows.Count <= 0)
                {
                    throw new Exception("Service not found.");    
                }

                return new SimpleService(serviceData.Rows[0]);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}