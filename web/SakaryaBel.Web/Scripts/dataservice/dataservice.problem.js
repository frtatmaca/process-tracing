var problemService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("getProblemList", "ajax", {
            url: '/Problem/GetProblemList/',
            dataType: "json",
            type: "POST"
        });              
    };

    pub.getProblemLists = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "getProblemList",
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