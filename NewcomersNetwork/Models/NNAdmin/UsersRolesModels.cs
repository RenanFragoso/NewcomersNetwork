using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Providers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NewcomersNetworkAPI.Models.NNAdmin
{
    public class RoleView
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";

        public RoleView()
        {
        }

        public RoleView(DataRow row)
        {
            this.Id = row["Id"].ToString();
            this.Name = row["Name"].ToString();
        }
    }

    public static class UsersRolesServices
    {
        public static IEnumerable<RoleView> GetAllRoles()
        {
            DataTable rolesDB = DBConn.ExecuteCommand("sp_Roles_GetAll", null).Tables[0];
            var roles = new List<RoleView>();

            if (rolesDB.Rows.Count > 0)
            {
                foreach (DataRow row in rolesDB.Rows)
                {
                    roles.Add(new RoleView(row));
                }
            }

            return roles;
        }
    }
}