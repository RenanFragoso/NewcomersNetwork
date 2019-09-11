using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class Pagination
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalItems { get; set; } = 0;
        public int TotalPages { get; set; } = 1;

        public Pagination()
        {
        }

        public async Task<int> GetPagination(NNClassifiedsFilter filter)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("cCategory", string.Join("|", filter.Categories));
            infoParameters.Add("cType", filter.Type);
            infoParameters.Add("cWord", filter.Word);
            DataTable oCountDB = DBConn.ExecuteCommand("sp_Classifieds_GetFilteredCount", infoParameters).Tables[0];

            this.TotalItems = (int) oCountDB.Rows[0]["items"];
            this.TotalPages = (int) Math.Ceiling( (double) this.TotalItems / filter.PageSize );
            this.CurrentPage = filter.Page;

            return this.TotalPages;
        }
    }
}