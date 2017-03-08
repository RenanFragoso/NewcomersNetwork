using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewcomersNetworkIFACE.Client;
using NewcomersNetworkAPI.Models;
using System.Net.Http;
using NewcomersNetworkIFACE.Models;
using Newtonsoft.Json;

namespace NewcomersNetworkIFACE.Areas.NNAdmin1.Models
{
    public class Classifieds : NNInterfaceModel
    {
        public List<Classified> oClassifieds = new List<Classified>();
        public List<Classified> oPending = new List<Classified>();

        public Classifieds()
        {
        }

        public void GetClassified(string cClassifiedId)
        {
            this.oClassifieds = oNNAPICLient.Get<List<Classified>>("/Classifieds/" + cClassifiedId);
            if (this.oClassifieds == null)
            {
                this.oClassifieds = new List<Classified>();
            }
        }

        public void LoadFiltered(NNClassifiedsFilter oFilter)
        {
            HttpResponseMessage oResponse = oNNAPICLient.Post("/Classifieds/GetFiltered", oFilter);
            if (oResponse.IsSuccessStatusCode)
            {
                this.oClassifieds = JsonConvert.DeserializeObject<List<Classified>>(oResponse.Content.ReadAsStringAsync().Result);
            }
            
            if (this.oClassifieds == null)
            {
                this.oClassifieds = new List<Classified>();
            }
        }

        public void GetPending()
        {
            this.oPending = oNNAPICLient.Get<List<Classified>>("/Classifieds/Pending");
            if (this.oPending == null)
            {
                this.oPending = new List<Classified>();
            }
        }

        public int GetPendingNum()
        {
            int nPending = 0;
            nPending = oNNAPICLient.Get<int>("/Classifieds/PendingNum");
            return nPending;
        }

        public HttpResponseMessage UpdateClassified(Classified oClassified)
        {
            HttpResponseMessage response;

            if (oClassified != null)
            {
                if (oClassified.Id == "NULLID")
                {
                    //Create Classified
                    response = oNNAPICLient.Post<Classified>("/Classifieds/Create", oClassified);
                    return response;
                }
                else
                {
                    //Update Classified
                    response = oNNAPICLient.Put<Classified>("/Classifieds/Update", oClassified);
                    return response;
                }

            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }
        }
        
        public HttpResponseMessage ApproveClassified(string cClassifiedId)
        {
            HttpResponseMessage response;

            if (cClassifiedId != null && cClassifiedId.Length > 0)
            {
                response = oNNAPICLient.Post<string>("/Classifieds/Approve/" + cClassifiedId, "");
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }
        }

        public HttpResponseMessage RejectClassified(string cClassifiedId)
        {
            HttpResponseMessage response;

            if (cClassifiedId != null && cClassifiedId.Length > 0)
            {
                response = oNNAPICLient.Post<string>("/Classifieds/Reject/" + cClassifiedId, "");
                return response;
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);
                response.Content = new StringContent("Invalid data received.");
                return response;
            }
        }

    }
}