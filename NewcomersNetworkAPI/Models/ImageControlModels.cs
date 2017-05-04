using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace NewcomersNetworkAPI.Models
{
    public class ImageControl : NNAPIModel
    {
        protected CloudStorageAccount storageAccount { get; set; }
        protected CloudBlobClient blobClient { get; set; }
        protected List<string> blobContainers { get; set; } = new List<string>();

        public ImageControl()
        {
            //Read connection string from config file
            this.storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

            //Read containers from config file
            foreach (string container in ConfigurationManager.AppSettings["BlobContainers"].Split(new char[] { ',' }))
            {
                this.blobContainers.Add(container);
            }

            //Create the client
            this.blobClient = this.storageAccount.CreateCloudBlobClient();
        }

        public bool UploadImage(ImageFile oFile)
        {
            CloudBlobContainer oContainer;
            CloudBlockBlob oImageBlob;
            
            if (this.blobContainers.Find( itm => itm == oFile.cContainer) != null)
            {
                if(oFile.cFileName != null && oFile.cFileName.Length > 0)
                {
                    oContainer = this.blobClient.GetContainerReference(oFile.cContainer);
                    oImageBlob = oContainer.GetBlockBlobReference(oFile.cFileName);

                    if (oFile.cContentType != null && oFile.cContentType.Length > 0)
                    {
                        oImageBlob.Properties.ContentType = oFile.cContentType;
                    }

                    try
                    {
                        oImageBlob.UploadFromByteArray(oFile.aFileData, 0, oFile.aFileData.Length - 1);
                    }
                    catch(Exception error)
                    {
                        this.sMsgError.Add(error.Message);
                        return false;
                    }
                    
                    return true;
                    
                }
            }
            return false;
        }

        public string RetrieveImage(string cContainer, string cFile)
        {
            CloudBlobContainer oContainer;
            CloudBlockBlob oImageBlob;

            if(this.blobContainers.Find(itm => itm == cContainer) != null)
            {
                oContainer = this.blobClient.GetContainerReference(cContainer);
                if (cFile != null && cFile.Length > 0)
                {
                    oImageBlob = oContainer.GetBlockBlobReference(cFile);
                    if (oImageBlob.Exists())
                    {
                        return oImageBlob.Uri.ToString();
                    }
                    else
                    {
                        oImageBlob = oContainer.GetBlockBlobReference("square-image.png");
                        return oImageBlob.Uri.ToString();
                    }
                }
                else
                {
                    oImageBlob = oContainer.GetBlockBlobReference("square-image.png");
                    return oImageBlob.Uri.ToString();
                }
            }
            else
            {
                this.sMsgError.Add("Container not found.");
                return "";
            }
        }
    }
}