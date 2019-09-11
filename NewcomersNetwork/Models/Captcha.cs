﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace NewcomersNetworkAPI.Models
{
    public static class Captcha
    {
        public static bool VerifyResponse(string response)
        {
            //secret that was generated in key value pair
            string secret = ConfigurationManager.AppSettings["reCAPTCHASecretKey"];

            var client = new WebClient();
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));

            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            string errorMessage = string.Empty;
            
            //when response is false check for the error message
            if (!captchaResponse.Success)
            {
                if (captchaResponse.ErrorCodes.Count <= 0) return captchaResponse.Success;

                var error = captchaResponse.ErrorCodes[0].ToLower();
                switch (error)
                {
                    case ("missing-input-secret"):
                        errorMessage = "The secret parameter is missing.";
                        break;
                    case ("invalid-input-secret"):
                        errorMessage = "The secret parameter is invalid or malformed.";
                        break;
                    case ("missing-input-response"):
                        errorMessage = "The response parameter is missing.";
                        break;
                    case ("invalid-input-response"):
                        errorMessage = "The response parameter is invalid or malformed.";
                        break;
                    default:
                        errorMessage = "Error occured. Please try again";
                        break;
                }
            }

            return captchaResponse.Success;
        }

        internal class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
    }
}