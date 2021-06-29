var referenceService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("getReferenceList", "ajax", {
            url: '/Reference/GetReferenceList/',
            dataType: "json",
            type: "POST"
        });

        console.log("reference data");
    };

    pub.getReferenceList = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "getReferenceList",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };


    return pub;

}(jQuery));