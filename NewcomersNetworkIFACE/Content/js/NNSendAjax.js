function NN_SendAjax(cType, cURL, oData, aMessage, fSuccess, fError, bSilent) {

    var headers = {};
    //Try to add the authentication tokens
    //AntiForgery
    if (typeof cAntiForgeryToken != "undefined") {
        headers['__RequestVerificationToken'] = cAntiForgeryToken;
    }
    //Authentication Token
    if (typeof cAuthToken != "undefined") {
        headers['Authentication'] = 'bearer ' + cAuthToken;
    }
    //UserCookie
    if (typeof cUserCookie != "undefined") {
        headers['User'] = cUserCookie;
    }

    $.ajax({
        url: cURL,
        type: cType,
        headers: headers,
        data: oData,
        success: function (response, textStatus, jqXHR) {
            if (typeof response.response == "object") {
                var cMessage = "";
                if (response.response.Message)
                    cMessage = response.response.Message;
            } else {
                var cMessage = (response.response ? $.parseJSON(response.response).Message : "");
            }
            
            if (response.success) {
                if (typeof fSuccess == "function")
                    fSuccess(response, textStatus);
                if (!bSilent) {
                    toastr.success(cMessage);
                }
            }
            else {
                if (!bSilent) {
                    toastr.error(cMessage);
                }
            }
        },
        error: function (jqXHR, textStatus, error) {
            if (typeof fError == "function")
                fError(jqXHR, textStatus, error);
            if (!bSilent) {
                toastr.error(error);
            }
        }
    });
    
}