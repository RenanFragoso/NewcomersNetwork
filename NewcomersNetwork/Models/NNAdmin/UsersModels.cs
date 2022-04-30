using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NewcomersNetworkAPI.Models.NNAdmin
{
    public class UsersListFilter
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Word { get; set; } = "";
    }

    public class UsersListView
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string AgeRange { get; set; }
        public string Education { get; set; }
        public string NearestIntersection { get; set; }
        public string PostalCode { get; set; }
        public bool ConsentToContact { get; set; }
        public bool IsImmigrant { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModified { get; set; }
        public string Status { get; set; }
        public List<string> UserRoles { get; set; }

        public UsersListView()
        {
        }

        public UsersListView(DataRow row)
        {
            this.Id = row["Id"].ToString();
            this.UserName = row["UserName"].ToString();
            this.FirstName = row["FirstName"].ToString();
            this.LastName = row["LastName"].ToString();
            this.Email = row["Email"].ToString();
            this.EmailConfirmed = (bool) row["EmailConfirmed"];
            this.Gender = row["Gender"].ToString();
            this.MaritalStatus = row["MaritalStatus"].ToString();
            this.AgeRange = row["AgeRange"].ToString();
            this.Education = row["Education"].ToString();
            this.NearestIntersection= row["NearestIntersection"].ToString();
            this.PostalCode = row["PostalCode"].ToString();
            this.ConsentToContact = (bool) row["ConsentToContact"];
            this.IsImmigrant = (bool) row["IsImmigrant"];
            this.Title = row["Title"].ToString();
            this.CreateDate = (DateTime) row["DateCreated"];
            this.LastModified = (DateTime) row["LastModified"];
            this.Status = row["Status"].ToString();
            this.UserRoles = row["Roles"].ToString().Split(',').ToList();
        }
    }

    public static class UserService
    {
        public static IEnumerable<UsersListView> GetAllUsers(UsersListFilter filter)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("Page", filter.Page);
                parameters.Add("PageSize", filter.PageSize);
                parameters.Add("Word", filter.Word);

                DataTable usersDB = DBConn.ExecuteCommand("sp_User_GetAll", parameters).Tables[0];
                List<UsersListView> users = new List<UsersListView>();
                foreach (DataRow user in usersDB.Rows)
                {
                    users.Add(new UsersListView(user));
                }

                return users;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<Pagination> GetUsersPagination(UsersListFilter filter)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>
            {
                { "Word", filter.Word }
            };

            DataTable oCountDB = DBConn.ExecuteCommand("sp_User_GetAllCount", infoParameters).Tables[0];
            Pagination pagination = new Pagination
            {
                TotalItems = (int)oCountDB.Rows[0]["totalUsers"],
                TotalPages = (int)Math.Ceiling((double)((int)oCountDB.Rows[0]["totalUsers"] / filter.PageSize)),
                CurrentPage = filter.Page
            };

            return pagination;
        }
    }
}