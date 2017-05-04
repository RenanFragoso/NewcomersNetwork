
//Customized Checkbox initialization
function NNcheckboxInit() {

    //Customized Checbox initialization
    $.each($(".nncheck"), (index, element) => {
        var $this = $(element);
        var dataChk = "checked";
        var boolChecked;
        var hiddenName = $this.data("name");
        if (hiddenName) {
            boolChecked = $("[name='" + hiddenName + "']").val().toLowerCase() === "true";
        } else {
            boolChecked = $this.data(dataChk).toLowerCase() === "true";
        }

        $this.data(dataChk, boolChecked);
        if (boolChecked) {
            $this.addClass("checked");
        }
        else {
            $this.removeClass("checked");
        }
    });

    //Customized Checbox Click Function
    $(".nncheck").bind('click', (event) => {
        var $this = $(event.currentTarget);
        var dataChk = "checked";
        var hiddenName = $this.data("name");
        if ($this.data(dataChk)) {
            $this.data(dataChk, false);
            $this.removeClass("checked");
            if (hiddenName) {
                $("[name='" + hiddenName + "']").val("False");
            }
        } else {
            $this.data(dataChk, true);
            $this.addClass("checked");
            if (hiddenName) {
                $("[name='" + hiddenName + "']").val("True");
            }
        }
    });
}