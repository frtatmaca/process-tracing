var systemService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("system.clearcache", "ajax", {
            url: '/System/ClearCache',
            dataType: "json",
            type: "POST"
        });
    };

    pub.clearCache = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "system.clearcache",
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