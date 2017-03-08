using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Providers;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace NewcomersNetworkAPI.Models
{
    public class ClassifiedEvents : NNAPIModel
    {

        public string ClassifiedId { get; set; } = "";
        public string EventType { get; set; } = "";
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string From { get; set; } = "";
        public string To { get; set; } = "";
        public string Contents { get; set; } = "";
        
        public ClassifiedEvents()
        {
        }

        public ClassifiedEvents(string cClassifiedId)
        {
        }

    }
}