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

    $.ajax({
        url: cURL,
        type: cType,
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
                fError(response, textStatus, error);
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