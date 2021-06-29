var helperValuesService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("helperValues.getall", "ajax", {
            url: '/HelperValues/GetAll',
            dataType: "text",
            type: "POST"
        });
        amplify.request.define("helperValues.deletebyid", "ajax", {
            url: '/HelperValues/Delete',
            dataType: "json",
            type: "POST"
        });
    };

    pub.getall = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "helperValues.getall",
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
    pub.deletebyid = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "helperValues.deletebyid",
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