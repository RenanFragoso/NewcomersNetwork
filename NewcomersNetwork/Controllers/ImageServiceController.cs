using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewcomersNetworkAPI.Models;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/image")]
    public class ImageServiceController : ApiController
    {
        protected ImageControl imageControl = new ImageControl();

        [HttpPut]
        [Authorize]
        [Route("useravatar")]
        public async Task<IHttpActionResult> UpdateUserAvatar()
        {
            string captcha = "";
            var avatarFile = new byte[0];
            var provider = new MultipartMemoryStreamProvider();
            ImageFile imageFile = new ImageFile("users", "", 0, "");
            ClaimsPrincipal oPrincipal = (ClaimsPrincipal)Request.GetRequestContext().Principal;

            var userId = oPrincipal.Identity.GetUserId();

            if (userId == null)
            {
                return BadRequest();
            }

            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (var item in provider.Contents)
            {
                switch (item.Headers.ContentDisposition.Name.Replace("\"", ""))
                {
                    case "Captcha":
                        captcha = await item.ReadAsStringAsync();
                        break;
                    case "AvatarFile":
                        imageFile.FileData = await item.ReadAsByteArrayAsync();
                        imageFile.ContentType = item.Headers.ContentType.ToString();
                        break;
                    default:
                        break;
                }
            }

            imageFile.FileName = Guid.NewGuid().ToString("N");
            imageFile.FileSize = imageFile.FileData.Length;

            if(Captcha.VerifyResponse(captcha) && imageFile.FileData.Length > 0)
            {
                UserDetails userDetails = new UserDetails(userId);
                if(userDetails.isValid)
                {
                    if(await userDetails.UpdateAvatar(imageFile))
                    {
                        return Ok();
                    }
                }
            }

            return BadRequest();
        }

        [HttpPut]
        [Authorize]
        [Route("justshareimage")]
        public async Task<IHttpActionResult> UpdateJustshareImage()
        {
            string captcha = "";
            var avatarFile = new byte[0];
            var provider = new MultipartMemoryStreamProvider();
            ImageFile imageFile = new ImageFile("classifieds", "", 0, "");
            ImageControl ImgCtrl = new ImageControl();
            ClaimsPrincipal oPrincipal = (ClaimsPrincipal)Request.GetRequestContext().Principal;

            var userId = oPrincipal.Identity.GetUserId();

            if (userId == null)
            {
                return BadRequest();
            }

            await Request.Content.ReadAsMultipartAsync(provider);

            foreach (var item in provider.Contents)
            {
                switch (item.Headers.ContentDisposition.Name.Replace("\"", ""))
                {
                    case "Id":
                        imageFile.FileName = await item.ReadAsStringAsync();
                        break;
                    case "Captcha":
                        captcha = await item.ReadAsStringAsync();
                        break;
                    case "ImageFile":
                        imageFile.FileData = await item.ReadAsByteArrayAsync();
                        imageFile.ContentType = item.Headers.ContentType.ToString();
                        break;
                    default:
                        break;
                }
            }

            imageFile.FileSize = imageFile.FileData.Length;

            if (Captcha.VerifyResponse(captcha) && imageFile.FileData.Length > 0)
            {
                if(ImgCtrl.UploadImage(imageFile))
                {
                    Ok();
                }
            }

            return BadRequest();
        }
    }

    public class ImageSent
    {
        public string cContainer { get; set; } = "";
        public string cFileName { get; set; } = "";
        public string aFileData { get; set; }
        public string cContentType { get; set; } = "";
        public string cFileData { get; set; } = "";
    }
}

