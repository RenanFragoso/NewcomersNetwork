var DocReady = [];

function RunInit() {
    for (i = 0; i < DocReady.length; i++) {
        DocReady[i]();
    }
}