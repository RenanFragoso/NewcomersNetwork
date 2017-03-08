/**
* NNEventRender - 0.0.1
* Render events from Newcomers Network.
* 
* Author: Renan Fragoso
* Copyright (c) 2017 Renan Fragoso. Released under MIT License.
**/

(function ($) {

    var NNEventRender = function (element, options) {

        this.$element = $(element);

        if (!this.$element.is('div')) {
            $.error('NNEventRender can only be used with a DIV DOM element.');
            return;
        }

        this.options = $.extend({}, $.fn.NNEventRender.defaults, options);
        this.init();
    };

    NNEventRender.prototype = {

        constructor: NNEventRender,

        data: "",

        init: function () {
            this.getEvents();
        },

        getEvents: function(){

            var $this = this;

            $.ajax({
                url: this.options.cEndPointURL,
                type: this.options.cRequestType,
                success: function (response, textStatus, jqXHR) {
                    $this.data = response[$this.options.cData];
                    $this.render();
                },
                error: function (jqXHR, textStatus, error) { }
            });

        },

        render: function () {

            if (this.data != "undefined") {
                var cLayout = "";
                console.log(this.data);
                for (nI = 0; nI < this.data.length ; nI++) {
                    if (typeof this.options.cEventTemplate == "function") {
                        cLayout += this.options.cEventTemplate(this.data[nI]);
                    }
                    else {
                        cLayout += this.options.cEventTemplate.Copy();
                    }
                }
                this.$element.html(cLayout);
            }
        },

        destroy: function () {
            this.data = "";
            this.$element.html("");
        }
    };

    $.fn.NNEventRender = function (options) {

        var settings = $.extend({}, $.fn.NNEventRender.defaults, options);
        var oRender = new NNEventRender(this,options);

    };

    //Default values
    $.fn.NNEventRender.defaults = {
        cEndPointURL: '',
        cRequestType: 'GET',
        cEventTemplate: '',
        cAuthToken: '',
        dStartDate: Date.now(),
        dEndDate: Date.now() + 1,
        cData: 'odata'
    };

}(window.jQuery));