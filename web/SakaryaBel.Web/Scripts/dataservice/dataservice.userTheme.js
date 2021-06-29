var userThemeService = (function ($) {
    var pub = {};

    pub.init = function () {


        amplify.request.define("user.updateColor", "ajax", {
            url: "/User/UserTheme",
            dataType: "json",
            type: "POST"
        });

        // Not in use
        amplify.request.define("user.getDefinedColor", "ajax", {
            url: '/User/GetDefinedColor',
            dataType: "json",
            type: "GET"
        });


    };

    pub.updateColor = function (data, successCallBack, errorCallBack) {
        //Request for news from amplify   
        amplify.request({
            resourceId: "user.updateColor",
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

    pub.getDefinedColor = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "user.getDefinedColor",
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