var roleService = (function ($) {
    var pub = {};
    pub.init = function () {

        amplify.request.define("role.addPermission", "ajax", {
            url: '/Role/AddPermission',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("role.addModule", "ajax", {
            url: '/Role/AddModule',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("role.getContexts", "ajax", {
            url: '/Role/GetContexts',
            dataType: "json",
            type: "POST"
        });
    };
    pub.addPermission = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "role.addPermission",
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
    pub.addModule = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "role.addModule",
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
    pub.getContexts = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "role.getContexts",
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