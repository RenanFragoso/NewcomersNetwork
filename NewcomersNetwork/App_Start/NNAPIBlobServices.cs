using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Auth;

namespace NewcomersNetworkAPI
{
    public class NNAPIBlobServices
    {

        public static void SetConfiguration()
        {
            //StorageCredentials credentials = new StorageCredentials(accountName, accountKey);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            //CloudStorageAccount storageAccount = new CloudStorageAccount(credentials, false);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            CloudBlobContainer cloudBlobContainerBanners = blobClient.GetContainerReference("banners");
            CloudBlobContainer cloudBlobContainerEvents = blobClient.GetContainerReference("events");
            CloudBlobContainer cloudBlobContainerClassifieds = blobClient.GetContainerReference("classifieds");
            CloudBlobContainer cloudBlobContainerUsers = blobClient.GetContainerReference("users");
            CloudBlobContainer cloudBlobContainerServices = blobClient.GetContainerReference("services");
            CloudBlobContainer cloudBlobContainerServiceGroups = blobClient.GetContainerReference("servicegroups");

            BlobContainerPermissions permissions = new BlobContainerPermissions();

            permissions.PublicAccess = BlobContainerPublicAccessType.Blob;

            cloudBlobContainerBanners.CreateIfNotExists();
            cloudBlobContainerEvents.CreateIfNotExists();
            cloudBlobContainerClassifieds.CreateIfNotExists();
            cloudBlobContainerUsers.CreateIfNotExists();
            cloudBlobContainerServices.CreateIfNotExists();
            cloudBlobContainerServiceGroups.CreateIfNotExists();

            //set access level to "blob", which means user can access the blob 
            //but not look through the whole container
            //this means the user must have a URL to the blob to access it
            cloudBlobContainerBanners.SetPermissions(permissions);
            cloudBlobContainerEvents.SetPermissions(permissions);
            cloudBlobContainerClassifieds.SetPermissions(permissions);
            cloudBlobContainerUsers.SetPermissions(permissions);
            cloudBlobContainerServices.SetPermissions(permissions);
            cloudBlobContainerServiceGroups.SetPermissions(permissions);
        }
    }
}