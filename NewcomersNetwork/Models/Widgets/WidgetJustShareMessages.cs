using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NewcomersNetworkAPI.Models.Widgets
{
    public class WidgetJustShareMessages : NNAPIModel
    {
        public int NewMessages { get; set; } = 0;
        public int TotalMessages { get; set; } = 0;
        public List<SecureClassifiedMessage> Messages { get; set; } = new List<SecureClassifiedMessage>();


        public WidgetJustShareMessages()
        {
        }

        public WidgetJustShareMessages(string userId)
        {
            
        }

    }
}