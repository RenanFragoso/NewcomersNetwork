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
            oApiClient.MaxResponseContentBufferSize = 256000;
            oApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            /*
            if (Request.Cookies["UserSettings"] != null) { 
                cClientToken = Request.Cookies["UserSettings"]["Font"];
            }
            */
        }

        public void setToken(string cToken)
        {
            this.cClientToken = cToken;
            //oApiClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + cClientToken);
        }

        public T Get<T>(string cAction, string cParam = "")
        {

            oApiClient.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = oApiClient.GetAsync(this.cNNAPIAddress + cAction + cParam).Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>("");
            }
        }

        public HttpResponseMessage Post<T>(string cAction, T data)
        {

            oApiClient.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = oApiClient.PostAsJsonAsync(this.cNNAPIAddress + cAction, data).Result;
            return response;
        }

        public HttpResponseMessage Put<T>(string cAction, T data)
        {
            oApiClient.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = oApiClient.PutAsJsonAsync(this.cNNAPIAddress + cAction, data).Result;
            return response;
        }

        public HttpResponseMessage Delete(string cAction, string cParam)
        {
            oApiClient.DefaultRequestHeaders.Accept.Clear();
            HttpResponseMessage response = oApiClient.DeleteAsync(this.cNNAPIAddress + cAction + "/" + cParam).Result;
            return response;
        }
    }
}