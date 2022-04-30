'use strict';

const authConfig = () => {
    return {
        headers: { Authorization: "Bearer " + sessionStorage.getItem('nntoken') }
    }
};

var apiProtocol = "";
var apiAddress = "";
var apiSufix = "";
var apiUrl = "";
var apiUrlNoSufix = "";
var recaptchaSiteKey = "";
var admApiSufix = "";
var admApiUrl = "";
var admApiUrlNoSufix = "";
var gmapsKey = "";

if(process.env.NODE_ENV === "production") {
    apiProtocol = "https";
    apiAddress = "newcomers-network.azurewebsites.net";
    apiSufix = "api";
    apiUrl = apiProtocol + "://" + apiAddress + "/" + apiSufix;
    apiUrlNoSufix = apiProtocol + "://" + apiAddress;
    admApiSufix = "adminapi";
    admApiUrl = apiProtocol + "://" + apiAddress + "/" + admApiSufix;
    admApiUrlNoSufix = apiProtocol + "://" + apiAddress;
    recaptchaSiteKey = "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI";
    gmapsKey = "AIzaSyAVLKAGG5m2bxDbDFnYFUomfkbPOIjXA-E";
} 
else {
    apiProtocol = "http";
    apiAddress = "localhost:56739";
    apiSufix = "api";
    apiUrl = apiProtocol + "://" + apiAddress + "/" + apiSufix;
    apiUrlNoSufix = apiProtocol + "://" + apiAddress;
    admApiSufix = "adminapi";
    admApiUrl = apiProtocol + "://" + apiAddress + "/" + admApiSufix;
    admApiUrlNoSufix = apiProtocol + "://" + apiAddress;
    recaptchaSiteKey = "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI";
    gmapsKey = "";
}

const apiConfig = {
    apiProtocol,
    apiAddress,
    apiSufix,
    apiUrl,
    apiUrlNoSufix,
    admApiSufix,
    admApiUrl,
    admApiUrlNoSufix,
    recaptchaSiteKey,
    authConfig,
    gmapsKey
};

export default apiConfig;