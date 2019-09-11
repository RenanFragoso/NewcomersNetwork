using NewcomersNetworkAPI.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NewcomersNetworkAPI.Models.Widgets;

namespace NewcomersNetworkAPI.Models.Settings
{
    public class SettingsBanners
    {
        public List<NNBanner> oBanners = new List<NNBanner>();

        public SettingsBanners(DateTime startDate, DateTime endDate)
        {
            Dictionary<string, object> infoParameters = new Dictionary<string, object>();
            infoParameters.Add("startDate", startDate);
            infoParameters.Add("endDate", endDate);
            DataTable oBannersDB = DBConn.ExecuteCommand("sp_Settings_Banners_Get", infoParameters).Tables[0];
            NNBanner oBanner;

            for ( int nX = 0;  nX < oBannersDB.Rows.Count; nX++ )
            {
                oBanner = new NNBanner();
                oBanner.MapFromTableRow(oBannersDB.Rows[nX]);
                this.oBanners.Add(oBanner);
            }
        }
    }
}