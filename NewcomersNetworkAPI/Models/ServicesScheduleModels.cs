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
    public class ServicesSchedule
    {
        [Key]
        public int ServiceScheduleID { get; set; }
        public int ServiceScheduleServiceID { get; set; }
        public DateTime ServiceShceduleStartDate { get; set; }
        public DateTime ServiceShceduleEndDate { get; set; }
        public string ServiceScheduleDoW { get; set; }
        public bool ServiceScheduleEnable { get; set; }
        public string ServiceScheduleDescription { get; set; }
        public string ServiceScheduleSlotsPerDoW { get; set; }

    }
}