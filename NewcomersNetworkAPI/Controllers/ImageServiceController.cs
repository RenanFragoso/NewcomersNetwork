using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewcomersNetworkAPI.Models;
using System.Threading.Tasks;
using System.Globalization;
using MultipartDataMediaFormatter.Infrastructure;
using MultipartDataMediaFormatter.Infrastructure.Logger;
using MultipartDataMediaFormatter.Converters;

namespace NewcomersNetworkAPI.Controllers
{
    [RoutePrefix("api/Image")]
    public class ImageServiceController : ApiController
    {
        protected ImageControl oImageControl = new ImageControl();

        private MultipartFormatterSettings Settings = new MultipartFormatterSettings()
        {
            SerializeByteArrayAsHttpFile = true,
            CultureInfo = CultureInfo.GetCultureInfo("en-US"),
            ValidateNonNullableMissedProperty = true
        };

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> UpdateImage(testSend oTeste)
        {
            try
            {
                ImageControl oImageControl = new ImageControl();

                var oFormTst = await Request.Content.ReadAsMultipartAsync();
                var content = oFormTst.Contents.FirstOrDefault();
                if (content != null)
                {
                    var result = await content.ReadAsStringAsync();
                }

                //FormDataConverterLogger logger = new FormDataConverterLogger();
                //FormDataToObjectConverter dataToObjectConverter = new FormDataToObjectConverter(formData, logger, Settings);
                //ImageFile oImage = (ImageFile)dataToObjectConverter.Convert(typeof(ImageFile));

                //formData.TryGetValue("aFileData", CultureInfo.CurrentCulture, out oImgData);
                //oImageControl.UploadImage(oImage);

                return Ok();
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
            
            return BadRequest(oImageControl.GetErrors());

        }
    }

    public class ImageSent
    {
        public string cContainer { get; set; } = "";
        public string cFileName { get; set; } = "";
        public HttpFile aFileData { get; set; }
        public string cContentType { get; set; } = "";
        public string cFileData { get; set; } = "";
    }

    public class testSend
    {
        public string test1 = "";
        public string test2 = "";
    }

}
