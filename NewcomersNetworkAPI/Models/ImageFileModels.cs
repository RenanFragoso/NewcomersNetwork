using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class ImageFile
    {

        public string cContainer { get; set; } = "";
        public string cFileName { get; set; } = "";
        public int nFileSize { get; set; } = 0;
        public byte[] aFileData { get; protected set; }
        public string cContentType { get; set; } = "";
        public string cFileData { get; set; } = "";

        public ImageFile()
        {
        }

        public void setFileData(byte[] newFileData)
        {
            this.aFileData = newFileData;
            this.nFileSize = newFileData.Length;

            //this.cFileData = Encoding.UTF32.GetString(this.aFileData);
        }

    }
}