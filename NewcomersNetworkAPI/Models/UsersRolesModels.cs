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
    public class UsersRoles : NNAPIModel
    {
        [Key]
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";

        List<User> oUsers { get; set; } = new List<User>();

        public UsersRoles()
        {
        }

        public UsersRoles(string cRoleId)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cRoleId", cRoleId);
            DataTable oRoleData = DBConn.ExecuteCommand("sp_Roles_Get", infoParameters).Tables[0];

            if (oRoleData.Rows.Count > 0)
            {
                this.MapFromTableRow(oRoleData.Rows[0]);
            }

        }

        public override void MapFromTableRow(DataRow row)
        {
            try
            {
                this.Id = row["Id"].ToString();
                this.Name = row["Name"].ToString();
            }
            catch (Exception e)
            {
                //throw;
                this.isValid = false;
                this.sMsgError.Add("Error:  UsersRoles.MapFromTableRow.");
                this.sMsgError.Add(e.Message);
                return;
            }
            this.Validate();
        }

        public override bool Validate()
        {
            //Object structure validation
            if (this.Id.Length == 0)
            {
                this.isValid = false;
                this.sMsgError.Add("Invalid Role ID.");
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
            this.sMsgError.Add("SAVE Not implemented.");
            return false;
        }
        public override bool Update()
        {
            this.sMsgError.Add("UPDATE Not implemented.");
            return false;
        }

    }
}