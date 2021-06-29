var actionService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("getActionList", "ajax", {
            url: '/Action/GetActionList/',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("getSubActionList", "ajax", {
            url: '/SubAction/GetSubActionList/',
            dataType: "json",
            type: "POST"
        });
    };

    pub.getActionLists = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "getActionList",
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

    pub.getSubActionLists = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "getSubActionList",
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