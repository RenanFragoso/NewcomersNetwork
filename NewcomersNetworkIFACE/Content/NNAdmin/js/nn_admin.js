// ====== Security =======
var cAntiForgeryToken;
var cCookieToken;

//Function to change the selected sidebar class menu
function menuSelected(cMenuId) {

    console.log("Selecting menu: " + cMenuId);
    if (cMenuId != null) {
        $(".menuitem").removeClass("active");
        $(cMenuId).addClass("active");
        //window.location = oAction.menuLInk;
    }

}

// ====== Validation Functions =======
function _CleanTags(strToClean) {
    return strToClean.replace(/<[^>]*>?/g, '');
}

function notEmpty(value) {
    if ($.trim(value) == '') {
        return 'This field is required';
    }
}

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
        //        contentType: "application/json; charset=utf-8",
        headers: headers,
        data: oData,
        success: function (response, textStatus, jqXHR) {
            var cMessage = (response.response? $.parseJSON(response.response).Message : "");
            if (response.success) {
                if (typeof fSuccess == "function")
                    fSuccess(response, textStatus);

                if(!bSilent){
                    $.gritter.add({
                        title: "Success",
                        text: (cMessage ? cMessage : (aMessage[0]? aMessage[0] + " Success." : "Success")),
                        sticky: false,
                        time: 5000
                    });
                }
            }
            else {
                if (!bSilent) {
                    $.gritter.add({
                        title: "Error",
                        text: (cMessage ? cMessage : (aMessage[0] ? aMessage[0] + " Error." : "Error")),
                        sticky: true,
                        time: 5000
                    })
                }
            }
        },
        error: function (jqXHR, textStatus, error) {
            if (typeof fError == "function")
                fError(jqXHR, textStatus, error);
            if (!bSilent) {
                $.gritter.add({
                    title: "Error",
                    text: (aMessage[0] ? aMessage[0] + " Request Error. Try Again Later" : "Error. Try Again Later"),
                    sticky: true,
                    time: 3000
                })
            }
        }
    });
    
}