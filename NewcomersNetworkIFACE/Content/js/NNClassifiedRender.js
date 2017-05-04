/**
* NNClassifiedRender - 0.0.1
* Renders Classifieds from Newcomers Network.
* 
* Author: Renan Fragoso
* Copyright (c) 2017 Renan Fragoso. Released under MIT License.
**/

(function ($) {

    var oRender;

    var NNClassifiedRender = function (element, options) {

        this.$element = $(element);

        if (!this.$element.is('div')) {
            $.error('NNClassifiedRender can only be used with a DIV DOM element.');
            return;
        }

        this.options = $.extend(true, {}, $.fn.NNClassifiedRender.defaults, options);
        this.init();
    };

    NNClassifiedRender.prototype = {
        constructor: NNClassifiedRender,
        data: [],
        options: {},
        ApplyFilter: function (oFilter) {
            this.options.oFilter = $.extend(true, {}, $.fn.NNClassifiedRender.defaults.oFilter, oFilter);
            return this;
        },
        init: function () {
            this.resetContents();
            $("#clsspin").hide();
        },
        getClassifieds: function () {

            var $this = this;
            this.resetContents();

            $.ajax({
                url: $this.options.cEndPointURL,
                type: $this.options.cRequestType,
                data: $this.options.oFilter,
                beforeSend: function () { $('#clsspin').show(); },
                complete: function () { $('#clsspin').hide(); },
                success: function (response, textStatus, jqXHR) {
                    $this.data = response[$this.options.cData];
                    $this.render();
                },
                error: function (jqXHR, textStatus, error) { }
            });

        },
        getPage: function (nPage) {
            if (nPage > 0) {
                this.options.oFilter.nPage = nPage;
                this.getClassifieds();
            }
        },
        setPageSize: function (nPageSize) {
            if(nPageSize > 0){
                this.options.oFilter.nPageSize = nPageSize;
                return this;
            }
        },
        render: function () {

            if (this.data != "undefined") {
                var cLayout = "";
                console.log(this.data);
                for (nI = 0; nI < this.data.length ; nI++) {
                    if (typeof this.options.cTemplate == "function") {
                        cLayout += this.options.cTemplate(this.data[nI]);
                    }
                    else {
                        cLayout += this.options.cTemplate.Copy();
                    }
                }

                this.$element.html(cLayout);

                if (typeof this.options.fAfterRender == "function") {
                    this.options.fAfterRender(this.$element, this.data);
                }
            }
        },
        resetContents: function () {
            this.$element.html("");
            var cSpinner = '<div id="clsspin" class="css3-spinner" style="position: absolute; z-index:auto;"><div class="css3-spinner-bounce1"></div><div class="css3-spinner-bounce2"></div><div class="css3-spinner-bounce3"></div></div>';
            this.$element.append(cSpinner);
        },
        destroy: function () {
            this.data = "";
            this.$element.html("");
        }
    };

    $.fn.NNClassifiedRender = function (options) {
        var settings = $.extend( true, {}, $.fn.NNClassifiedRender.defaults, options);
        if (typeof oRender == "undefined") {
            oRender = new NNClassifiedRender(this, options);
        }
        return oRender;
    };

    $.fn.nnClassifiedRender = function () {
        return oRender;
    };

    //Default values
    $.fn.NNClassifiedRender.defaults = {
        cEndPointURL: '',
        cRequestType: 'GET',
        cTemplate: '',
        cAuthToken: '',
        dStartDate: Date.now(),
        dEndDate: Date.now() + 1,
        cData: 'odata',
        fAfterRender: function(){},
        oFilter: {
            nPageSize: 10,  // Classifieds per page
            nPage: 1,       // Page Requested
            cType: "",      // H = Help or N = Needs, Empty = Both
            aCategory: [],  // Service Category - Service Groups ID to filter
            cWord: ""       // Word filter, will be applied to the Title/Text content
        },
    };

}(window.jQuery));