using NewcomersNetworkAPI.Models.Image;
using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class ServiceGroup : NNAPIModel
    {
        [Key]
        [Display(Name = "Group Id")]
        public string GroupId { get; set; } = "";

        [Display(Name = "Group Name")]
        [Required(ErrorMessage = "Group Name is required")]
        public string GroupName { get; set; } = "";

        [Display(Name = "Group Description")]
        public string GroupDescription { get; set; } = "";

        [Display(Name = "Group Color")]
        public string GroupColor { get; set; } = "";

        [Display(Name = "Group Icon")]
        public string GroupIcon { get; set; } = "";

        [Display(Name = "Group Status")]
        private string GroupStatus { get; set; } = "";

        public ServiceGroup()
        {
        }

        public ServiceGroup(string cGroupId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cGroupId", cGroupId);
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_GetById", infoParameters).Tables[0];

            if (!oGroupData.HasErrors && oGroupData.Rows.Count > 0)
            {
                this.MapFromTableRow(oGroupData.Rows[0]);
            }

        }
        
        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.GroupId = row["GroupId"].ToString();
                this.GroupName = row["GroupName"].ToString();
                this.GroupDescription = row["GroupDescription"].ToString();
                this.GroupColor = row["GroupColor"].ToString();
                this.GroupIcon = row["GroupIcon"].ToString();
                this.GroupStatus = row["GroupStatus"].ToString();
            }
            catch (Exception e)
            {
                this.isValid = false;
                this.sMsgError.Add("Error:  ServiceGroup.MapFromTableRow().");
                this.sMsgError.Add(e.Message);
                return;
            }

            this.Validate();

        }

        public override bool Validate()
        {
            if (this.GroupId.Length == 0)
            {
                this.isValid = false;
                this.sMsgError.Add("Invalid Group ID.");
                return false;
            }

            this.isValid = true;
            return true;
        }

        public override bool Save()
        {
            DateTime dDateNow = DateTime.Now;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cName", this.GroupName);
            infoParameters.Add("cDescription", this.GroupDescription);
            infoParameters.Add("cColor", this.GroupColor);
            infoParameters.Add("cIcon", this.GroupIcon);
            infoParameters.Add("dAlterDate", dDateNow);
            infoParameters.Add("dCreateDate", dDateNow);
            
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Insert", infoParameters).Tables[0];

            if (!oGroupData.HasErrors && oGroupData.Rows[0]["LAST_SERVICEGROUP"] != null)
            {
                this.GroupId = oGroupData.Rows[0]["LAST_SERVICEGROUP"].ToString();
                return true;
            }
            else
            {
                this.sMsgError.Add("Error Saving Service Group.");
                return false;
            }
        }

        public override bool Update()
        {
            DateTime dDateNow = DateTime.Now;
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();

            infoParameters.Add("cId", this.GroupId);
            infoParameters.Add("cName", this.GroupName);
            infoParameters.Add("cDescription", this.GroupDescription);
            infoParameters.Add("cColor", this.GroupColor);
            infoParameters.Add("cIcon", this.GroupIcon);
            infoParameters.Add("dAlterDate", dDateNow);

            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Update", infoParameters).Tables[0];

            if (oGroupData.HasErrors)
            {
                this.sMsgError.Add(oGroupData.GetErrors().ToString());
                return false;
            }

            return true;
        }

        public override bool Delete()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cGroupId", this.GroupId);
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Delete", infoParameters).Tables[0];

            if (!oGroupData.HasErrors)
            {
                return true;
            }

            return false;
        }

        public bool Deactivate()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cGroupId", this.GroupId);
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Deactivate", infoParameters).Tables[0];

            if (!oGroupData.HasErrors)
            {
                return true;
            }

            return false;
        }

        public bool Activate()
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cGroupId", this.GroupId);
            DataTable oGroupData = DBConn.ExecuteCommand("sp_ServicesGroup_Activate", infoParameters).Tables[0];

            if (!oGroupData.HasErrors)
            {
                return true;
            }

            return false;
        }

    }

    public class SvcGrpSimple
    {
        private string GroupId { get; set; } = "";
        public string GroupName { get; set; } = "";
        public string GroupDescription { get; set; } = "";
        public string GroupColor { get; set; } = "";
        public string GroupIcon { get; set; } = "";
        public string Link { get { return "/services/" + Convert.ToBase64String(Encoding.Default.GetBytes(GroupId)); } }
        public IEnumerable<SimpleService> Services { get; set; } = new List<SimpleService>();
        public string GroupImage { get { return ImageContainer.RetrieveImagePath("servicegroups", this.GroupId); } }

        public SvcGrpSimple()
        {
        }

        public SvcGrpSimple(ServiceGroup group)
        {
            this.GroupId = group.GroupId;
            this.GroupName = group.GroupName;
            this.GroupDescription = group.GroupDescription;
            this.GroupColor = group.GroupColor;
            this.GroupIcon = group.GroupIcon;
        }

        public SvcGrpSimple(string groupId)
        {
            ServiceGroup group = new ServiceGroup(groupId);
            if (group.isValid)
            {
                this.GroupId = groupId;
                this.GroupName = group.GroupName;
                this.GroupDescription = group.GroupDescription;
                this.GroupColor = group.GroupColor;
                this.GroupIcon = group.GroupIcon;
            }
        }

        public void LoadServices()
        {
            this.Services = ServiceGroupsService.GetSimpleServicesByGroup(this.GroupId);
        }
    }

    public static class ServiceGroupsService
    {
        public static IEnumerable<SvcGrpSimple> GetSimpleServiceGroups()
        {
            List<SvcGrpSimple> groups = new List<SvcGrpSimple>();
            try
            {
                DataTable groupsData = DBConn.ExecuteCommand("sp_ServicesGroup_GetAll", null).Tables[0];
                foreach (DataRow row in groupsData.Rows)
                {
                    ServiceGroup group = new ServiceGroup();
                    group.MapFromTableRow(row);
                    groups.Add(new SvcGrpSimple(group));
                }
                return groups;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public static IEnumerable<Service> GetServicesByGroup(string groupId)
        {
            List<Service> services = new List<Service>();
            DataTable serviceData = DBConn.ExecuteCommand( "sp_Services_GetServicesByGroup", 
                                                        new Dictionary<string, object> {
                                                            { "groupId", groupId } }).Tables[0];
            foreach (DataRow row in serviceData.Rows)
            {
                services.Add(new Service(row["ServiceID"].ToString()));
            }

            return services;
        }

        public static IEnumerable<SimpleService> GetSimpleServicesByGroup(string groupId)
        {
            List<SimpleService> services = new List<SimpleService>();

            try
            {
                DataTable serviceData = DBConn.ExecuteCommand(  "sp_Services_GetServicesByGroup",
                                                                new Dictionary<string, object> {
                                                                    { "groupId", groupId }
                                                                }).Tables[0];
                foreach (DataRow row in serviceData.Rows)
                {
                    services.Add(new SimpleService(row));
                }

                return services;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}