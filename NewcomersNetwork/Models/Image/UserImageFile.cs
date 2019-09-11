using NewcomersNetworkAPI.Models.Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace NewcomersNetworkAPI.Models.Image
{
    public class UserImageFile : IImageFile
    {
        public string Container { get; set; } = "";
        public string FileName { get; set; } = "";
        public int FileSize { get; set; } = 0;
        public byte[] FileData { get; set; }
        public string ContentType { get; set; } = "";

        public UserImageFile()
        {
            this.Container = "users";
        }

        public UserImageFile(string fileName, int fileSize, string contentType)
        {
            this.Container = "users";
            this.FileName = fileName;
            this.FileSize = fileSize;
            this.ContentType = contentType;
        }

        public void SetFileData(byte[] newFileData)
        {
            this.FileData = newFileData;
            this.FileSize = newFileData.Length;
        }

        public void SetMetadata(string container, string fileName, string contentType, byte[] fileData)
        {
            this.Container = container;
            this.FileName = fileName;
            this.ContentType = contentType;
            this.SetFileData(fileData);
        }

        public string GetContainer()
        {
            return this.Container;
        }

        public string GetFileName()
        {
            return this.FileName;
        }

        public byte[] GetFileData()
        {
            return this.FileData;
        }

        public int GetFileSize()
        {
            return this.FileSize;
        }
    }
}