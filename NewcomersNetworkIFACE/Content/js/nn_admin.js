//Function to change the selected sidebar class menu
function menuSelected(cMenuId) {

    console.log("Selecting menu: " + cMenuId);
    if (cMenuId != null) {
        $(".menuitem").removeClass("active");
        $(cMenuId).addClass("active");
        //window.location = oAction.menuLInk;
    }

}