using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public class ImageFile
    {

        public string Container { get; set; } = "";
        public string FileName { get; set; } = "";
        public int FileSize { get; set; } = 0;
        public byte[] FileData { get; set; }
        public string ContentType { get; set; } = "";
        //public FileStream fileStream;

        public ImageFile()
        {
        }

        public ImageFile(string container, string fileName, int fileSize, string contentType)
        {
            this.Container = container;
            this.FileName = fileName;
            this.FileSize = fileSize;
            this.ContentType = contentType;
        }

        public void SetFileData(byte[] newFileData)
        {
            this.FileData = newFileData;
            this.FileSize = newFileData.Length;
        }

    }
}