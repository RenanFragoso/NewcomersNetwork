using System.Collections.Generic;
using System.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Threading.Tasks;
using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace NewcomersNetworkAPI.Models
{
    public abstract class NNAPIModel
    {

        [JsonIgnore]
        public bool isValid { get; protected set; } = false;
        [JsonIgnore]
        public List<string> sMsgError { get; set; } = new List<string>();

        public virtual void MapFromTableRow(DataRow row)
        {
        }
        public virtual bool Validate()
        {
            return false;
        }

        public virtual bool Delete()
        {
            return false;
        }
        public virtual bool Save()
        {
            return false;
        }
        public virtual bool Update()
        {
            return false;
        }

        public virtual async Task<bool> SaveAsync()
        {
            return false;
        }

        public virtual async Task<bool> UpdateAsync()
        {
            return false;
        }

        public virtual async Task<bool> DeleteAsync()
        {
            return false;
        }

        public virtual string GetErrors()
        {
            return string.Join(",",this.sMsgError.ToArray());
        }

        public virtual void LoadFullDetails()
        {
        }

        public virtual string AdjustImageFile(string cContainer, string cFile)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(cContainer);
            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob ImageBlob;

            if (cFile != null && cFile.Length > 0)
            {
                ImageBlob = container.GetBlockBlobReference(cFile);
                if (ImageBlob.Exists())
                {
                    return ImageBlob.Uri.ToString();
                }
                else
                {
                    ImageBlob = container.GetBlockBlobReference("square-image.png");
                    return ImageBlob.Uri.ToString();
                }
            }
            else
            {
                ImageBlob = container.GetBlockBlobReference("square-image.png");
                return ImageBlob.Uri.ToString();
            }
        }

        public virtual void UploadImage(string cContainer, string cFileName, byte[] cFileData, string cContentType = "")
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(cContainer);
            CloudBlockBlob ImageBlob = container.GetBlockBlobReference(cFileName);
            if(cContentType != null && cContentType.Length > 0)
            {
                ImageBlob.Properties.ContentType = cContentType;
            }
            ImageBlob.UploadFromByteArray(cFileData, 0, cFileData.Length - 1);
        }
    }
}