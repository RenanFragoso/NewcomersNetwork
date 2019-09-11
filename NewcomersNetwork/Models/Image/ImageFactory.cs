using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace NewcomersNetworkAPI.Models.Image
{
    public static class ImageFactory
    {
        public static IImageFile CreateImageModel(string container)
        {
            switch (container)
            {
                case "users":
                    return new UserImageFile();
                case "classifieds":
                    return new UserImageFile();
                default:
                    return new ImageFile();
            }
        }

        public static IImageFile CreateImageModel(string container, string fileName, int fileSize, string contentType)
        {
            switch(container)
            {
                case "users":
                    return new UserImageFile(fileName, fileSize, contentType);
                case "classifieds":
                    return new ClassifiedImageFile(fileName, fileSize, contentType);
                default:
                    return new ImageFile(fileName, fileSize, contentType);
            }
        }

        public static async Task<IImageFile> CreateImageFromRequest(string container, string fileName, HttpRequestMessage request)
        {
            var provider = new MultipartMemoryStreamProvider();
            await request.Content.ReadAsMultipartAsync(provider);

            IImageFile imageFile = CreateImageModel(container);
            foreach (var item in provider.Contents)
            {
                switch (item.Headers.ContentDisposition.Name.Replace("\"", ""))
                {
                    case "AvatarFile":
                        var fileData = await item.ReadAsByteArrayAsync();
                        var contentType = item.Headers.ContentType.ToString();
                        var fileSize = fileData.Length;
                        imageFile.SetMetadata(container, fileName, contentType, fileData);
                        break;
                }
            }

            return imageFile;
        }

        public static IImageFile CreateImageModel(string container, string fileName, Collection<MultipartFileData> fileData)
        {
            MultipartFileData file = fileData[0];
            return CreateImageModel(container, fileName, (int) file.Headers.ContentLength, file.Headers.ContentType.ToString());
        }


        public static IImageFile CreateFullImageModel(string container, string fileName, Collection<MultipartFileData> fileData)
        {
            MultipartFileData file = fileData[0];
            var fileBytes = File.ReadAllBytes(file.LocalFileName.ToString());
            var fileSize = fileBytes.Length;
            var imageFile = CreateImageModel(container, fileName, fileSize, file.Headers.ContentType.ToString());

            imageFile.SetFileData(fileBytes);

            return imageFile;
        }
    }
}