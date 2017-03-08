var DocReady = [];

function RunInit() {

    //console.log("Running admin init.");
    for (i = 0; i < DocReady.length; i++) {
        DocReady[i]();
    }
}

function ShowModal(cModal) {
    console.log("Sowing modal: " + cModal);
    $(cModal).modal("show");
}