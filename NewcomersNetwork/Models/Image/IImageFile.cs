using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NewcomersNetworkAPI.Models.Image
{
    public interface IImageFile
    {

        string GetContainer();
        string GetFileName();
        byte[] GetFileData();
        int GetFileSize();
        void SetFileData(byte[] newFileData);
        void SetMetadata(string container, string fileName, string contentType, byte[] fileData );
    }
}
