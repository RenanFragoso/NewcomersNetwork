using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

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

        public bool UploadImage(ImageFile File)
        {
            return UploadImg(File);
        }

        public async Task<bool> UploadImageAsync(ImageFile File)
        {
            return UploadImg(File);
        }

        private bool UploadImg(ImageFile File)
        {
            CloudBlobContainer Container;
            CloudBlockBlob ImageBlob;

            if (this.blobContainers.Find(itm => itm == File.Container) != null)
            {
                if (File.FileName != null && File.FileName.Length > 0)
                {
                    Container = this.blobClient.GetContainerReference(File.Container);
                    ImageBlob = Container.GetBlockBlobReference(File.FileName);

                    if (File.ContentType != null && File.ContentType.Length > 0)
                    {
                        ImageBlob.Properties.ContentType = File.ContentType;
                    }

                    try
                    {
                        ImageBlob.UploadFromByteArray(File.FileData, 0, File.FileSize - 1);
                    }
                    catch (Exception error)
                    {
                        this.sMsgError.Add(error.Message);
                        return false;
                    }

                    return true;

                }
            }
            return false;
        }
        
        public async Task<bool> DeleteImageAsync(string container, string fileName)
        {
            CloudBlobContainer Container;
            CloudBlockBlob ImageBlob;

            if (this.blobContainers.Find(itm => itm == container) != null)
            {
                if (fileName != null && fileName.Length > 0)
                {
                    Container = this.blobClient.GetContainerReference(container);
                    ImageBlob = Container.GetBlockBlobReference(fileName);

                    try
                    {
                        ImageBlob.DeleteIfExists();
                    }
                    catch (Exception error)
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

    public static class ImageControlStatic
    {
        public static string RetrieveImage(string cContainer, string cFile)
        {
            CloudStorageAccount storageAccount;
            CloudBlobClient blobClient;
            CloudBlobContainer container;
            CloudBlockBlob imageBlob;
            List<string> blobContainers = new List<string>();

            storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            blobClient = storageAccount.CreateCloudBlobClient();

            foreach (string containerName in ConfigurationManager.AppSettings["BlobContainers"].Split(new char[] { ',' }))
            {
                blobContainers.Add(containerName);
            }

            if (blobContainers.Find(itm => itm == cContainer) != null)
            {
                container = blobClient.GetContainerReference(cContainer);
                if (cFile != null && cFile.Length > 0)
                {
                    imageBlob = container.GetBlockBlobReference(cFile);
                    if (imageBlob.Exists())
                    {
                        return imageBlob.Uri.ToString();
                    }
                    else
                    {
                        imageBlob = container.GetBlockBlobReference("square-image.png");
                        return imageBlob.Uri.ToString();
                    }
                }
                else
                {
                    imageBlob = container.GetBlockBlobReference("square-image.png");
                    return imageBlob.Uri.ToString();
                }
            }
            else
            {
                return "";
            }
        }

    }

}