using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text;
using System.Threading.Tasks;
using NewcomersNetworkIFACE.Models;
using NewcomersNetworkAPI.Models;
using System.IO;
using MultipartDataMediaFormatter;
using MultipartDataMediaFormatter.Infrastructure;
using System.Globalization;

namespace NewcomersNetworkIFACE.Client
{
    public class NNAPIClient
    {

        protected readonly string cNNAPIAddress = ConfigurationManager.AppSettings["ApiEndPoint"];
        protected string cClientToken { get; set; }
        protected HttpClient oApiClient;

        public NNAPIClient()
        {
            oApiClient = new HttpClient();
            oApiClient.MaxResponseContentBufferSize = 1024000;
            oApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            /*
            if (Request.Cookies["UserSettings"] != null) { 
                this.cClientToken = Request.Cookies["UserSettings"]["NNAPIUserToken"];
                oApiClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + this.cClientToken);
            }
            */
        }

        protected void restoreHeaders()
        {
            oApiClient.MaxResponseContentBufferSize = 1024000;
            oApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.setToken(this.cClientToken);
        }

        public void setToken(string cToken)
        {
            this.cClientToken = cToken;
            this.oApiClient.DefaultRequestHeaders.Remove("Authorization");
            this.oApiClient.DefaultRequestHeaders.Add("Authorization", "bearer " + this.cClientToken);
        }

        public T Get<T>(string cAction, string cParam = "", bool bIgnoreApiAddress = false)
        {

            oApiClient.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response;

            if (bIgnoreApiAddress)
            {
                response = oApiClient.GetAsync(cAction + cParam).Result;
            } else
            {
                response = oApiClient.GetAsync(this.cNNAPIAddress + cAction + cParam).Result;
            }
            
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>("");
            }
        }

        public HttpResponseMessage Post<T>(string cAction, T data, bool bIgnoreApiAddress = false)
        {

            oApiClient.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response;
            if (bIgnoreApiAddress)
            {
                response = oApiClient.PostAsJsonAsync(cAction, data).Result;
            }
            else
            {
                response = oApiClient.PostAsJsonAsync(this.cNNAPIAddress + cAction, data).Result;
            }
            return response;
        }

        public HttpResponseMessage Put<T>(string cAction, T data, bool bIgnoreApiAddress = false)
        {
            oApiClient.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response;
            if (bIgnoreApiAddress)
            {
                response = oApiClient.PutAsJsonAsync(cAction, data).Result;
            }
            else
            {
                response = oApiClient.PutAsJsonAsync(this.cNNAPIAddress + cAction, data).Result;
            }
            return response;
        }

        public HttpResponseMessage Delete(string cAction, string cParam, bool bIgnoreApiAddress = false)
        {
            oApiClient.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response;
            if (bIgnoreApiAddress)
            {
                response = oApiClient.DeleteAsync(cAction + "/" + cParam).Result;
            }
            else
            {
                response = oApiClient.DeleteAsync(this.cNNAPIAddress + cAction + "/" + cParam).Result;
            }


            return response;
        }

        public async Task<HttpResponseMessage> RequestToken(Login data)
        {
            oApiClient.DefaultRequestHeaders.Accept.Clear();

            HttpResponseMessage response;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, ConfigurationManager.AppSettings["TokenEndPoint"]);

            var formValues = new List<KeyValuePair<string, string>>();

            formValues.Add(new KeyValuePair<string, string>("grant_type", "password"));
            formValues.Add(new KeyValuePair<string, string>("username", data.UserName));
            formValues.Add(new KeyValuePair<string, string>("password", data.Password));

            request.Content = new FormUrlEncodedContent(formValues);
            response = await oApiClient.SendAsync(request);
            
            return response;
        }

        public async Task<HttpResponseMessage> PostImage(ImageFile oFile)
        {
            //oApiClient.DefaultRequestHeaders.Accept.Clear();

            HttpResponseMessage response;
            /*
            var formValues = new List<KeyValuePair<string, string>>();
            formValues.Add(new KeyValuePair<string, string>("grant_type", "password"));
            formValues.Add(new KeyValuePair<string, string>("username", data.UserName));
            formValues.Add(new KeyValuePair<string, string>("password", data.Password));
            */
            //request.Content = new FormUrlEncodedContent(formValues);
            HttpContent cContainer = new StringContent(oFile.cContainer);
            HttpContent cFileName = new StringContent(oFile.cFileName);
            HttpContent cContentType = new StringContent(oFile.cContentType);
            HttpContent nFileSize = new StringContent(oFile.nFileSize.ToString());
            HttpContent aFileData = new ByteArrayContent(oFile.aFileData);

            MultipartFormDataContent oFormData = new MultipartFormDataContent();
            oFormData.Add(cContainer);
            oFormData.Add(cFileName);
            oFormData.Add(cContentType);
            oFormData.Add(nFileSize);
            oFormData.Add(aFileData);
            
            response = await oApiClient.PutAsync(this.cNNAPIAddress + "/Image", oFormData);

            return response;
        }

        public async Task<HttpResponseMessage> PutImage(ImageFile oImage) {

            HttpResponseMessage oResponse;
            /*
            HttpContent cContainer = new StringContent(oImage.cContainer);
            HttpContent cFileName = new StringContent(oImage.cFileName);
            HttpContent cContentType = new StringContent(oImage.cContentType);
            HttpContent aFileData = new ByteArrayContent(oImage.aFileData);
            
            MultipartFormDataContent oFormData = new MultipartFormDataContent();
            oFormData.Add(cContainer, "cContainer");
            oFormData.Add(cFileName, "cFileName");
            oFormData.Add(cContentType, "cContentType");
            oFormData.Add(aFileData, "aFileData");
            */
            HttpClient oImgApiClient = new HttpClient();
            //oImgApiClient.DefaultRequestHeaders.Add("Content-Type", "multipart/form-data");
            //oImgApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));

            //HttpRequestMessage oReq = new HttpRequestMessage(HttpMethod.Post, this.cNNAPIAddress + "/Image");
            //oReq.Content = oFormData;
            //oReq.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("multipart/form-data");

            var mediaTypeFormatter = new FormMultipartEncodedMediaTypeFormatter(new MultipartFormatterSettings()
            {
                SerializeByteArrayAsHttpFile = true,
                CultureInfo = CultureInfo.GetCultureInfo("en-US"),
                ValidateNonNullableMissedProperty = true
            });

            testSend oTest = new testSend();

            //oResponse = await oImgApiClient.PutAsync(this.cNNAPIAddress + "/Image", oFormData);
            oResponse = await oImgApiClient.PutAsync(this.cNNAPIAddress + "/Image", oTest, mediaTypeFormatter);
            //oResponse = await oImgApiClient.SendAsync(oReq);

            //this.restoreHeaders();
            return oResponse;
        }

    }


    public class testSend
    {
        public string test1 = "LOL TEST 1";
        public string test2 = "LOL TEST 2";
    }
    


}