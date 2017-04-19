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
using NewcomersNetworkIFACE.Models;
using System.Threading.Tasks;

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
            //oApiClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["TokenEndPoint"]);
            response = await oApiClient.SendAsync(request);
            
            return response;
        }

    }
}