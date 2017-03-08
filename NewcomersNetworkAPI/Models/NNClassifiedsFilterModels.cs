using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class NNClassifiedsFilter
    {
        public int nPage { get; set; } = 1;        //Pagination
        public int nPageSize { get; set; } = 10;   //Pagination item size
        public string cType { get; set; } = "";
        public List<string> aCategory { get; set; } = new List<string>();
        public string cWord { get; set; } = "";

    }
}