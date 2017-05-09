
//Customized Checkbox initialization
function NNcheckboxInit() {

    //Customized Checbox initialization
    $.each($(".nncheck"), (index, element) => {
        var $this = $(element);
        var dataChk = "checked";
        var boolChecked;
        var hiddenName = $this.data("name");

        if (hiddenName) {
            var hiddenVal = $("[name='" + hiddenName + "']").val();
            if (typeof hiddenVal === "boolean") {
                boolChecked = hiddenVal;
            } else {
                boolChecked = hiddenVal.toLowerCase() === "true";
            }
        } else {
            var hiddenVal = $this.data(dataChk);
            if (typeof hiddenVal === "boolean") {
                boolChecked = hiddenVal;
            } else {
                boolChecked = boolChecked.toLowerCase() === "true";
            }
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
        var override = $this.data("override");

        var $id = $this[0].id;

        if ($id.length == 0) {
            $id = hiddenName;
        }

        if (override) {
            var bValue = eval(override + "(" + $this.data(dataChk) ? false : true + ")");
        }
        else {
            var bValue = $this.data(dataChk);
        }

        if (bValue) {
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

        $.event.trigger({
            type: $id + "Change",
            time: new Date()
        });

    });
}