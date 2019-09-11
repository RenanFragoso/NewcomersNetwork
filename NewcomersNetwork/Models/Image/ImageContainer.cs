using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace NewcomersNetworkAPI.Models.Image
{
    public static class ImageContainer
    {
        public static IEnumerable<string> GetContainers()
        {
            return ConfigurationManager.AppSettings["BlobContainers"].Split(new char[] { ',' });
        }

        public static void UploadImage(IImageFile imageFile, MultipartFileData file = null)
        {
            try
            {
                if (GetContainers().ToList().Find(itm => itm == imageFile.GetContainer()) != null)
                {
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference(imageFile.GetContainer());
                    CloudBlockBlob imageBlob = container.GetBlockBlobReference(imageFile.GetFileName());

                    if (file != null)
                    {
                        // Data is in local upload folder
                        imageBlob.UploadFromFile(file.LocalFileName, FileMode.Open);
                    }
                    else
                    {
                        // Data is in imageFile FileData
                        imageBlob.UploadFromByteArray(imageFile.GetFileData(), 0, imageFile.GetFileSize() - 1);
                    }
                }
                else
                {
                    throw new Exception("Container not found");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public static void DeleteImageAsync(string container, string fileName)
        {
            if (GetContainers().ToList().Find(itm => itm == container) != null)
            {
                if (fileName != null && fileName.Length > 0)
                {
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer blobContainer = blobClient.GetContainerReference(container);
                    CloudBlockBlob imageBlob = blobContainer.GetBlockBlobReference(fileName);
                    imageBlob.DeleteIfExists();
                }
            }
        }

        public static string RetrieveImagePath(string container, string fileName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer;
            CloudBlockBlob imageBlob;

            if (GetContainers().ToList().Find(itm => itm == container) != null)
            {
                blobContainer = blobClient.GetContainerReference(container);

                if (fileName != null && fileName.Length > 0)
                {
                    imageBlob = blobContainer.GetBlockBlobReference(fileName);

                    if (imageBlob.Exists())
                    {
                        return imageBlob.Uri.ToString();
                    }
                    else
                    {
                        imageBlob = blobContainer.GetBlockBlobReference("square-image.png");
                        return imageBlob.Uri.ToString();
                    }
                }
                else
                {
                    imageBlob = blobContainer.GetBlockBlobReference("square-image.png");
                    return imageBlob.Uri.ToString();
                }
            }
            else
            {
                throw new Exception("Container not found");
            }
        }
    }
}