using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class NNClassifiedsFilter
    {
        public int Page { get; set; } = 1;        //Pagination
        public int PageSize { get; set; } = 10;   //Pagination item size
        public string Type { get; set; } = "";
        public List<string> Categories { get; set; } = new List<string>();
        public string Word { get; set; } = "";

    }
}