var accountService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("account.getUsersInfos", "ajax", {
            url: '/Account/GetUsersInfos',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("account.getUsersInfosBySearch", "ajax", {
            url: '/Account/GetUsersInfosBySearch',
            dataType: "json",
            type: "POST"
        });

    };


    pub.getUsersInfos = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "account.getUsersInfos",
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

    pub.getUsersInfosBySearch = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "account.getUsersInfosBySearch",
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