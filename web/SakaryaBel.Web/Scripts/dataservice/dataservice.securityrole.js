var securityRoleService = (function ($) {
    var pub = {};
    var baseApiUrl = '/Api/WebApiSecurityRole/';
    var ns = "securityRole";
    pub.init = function () {
        amplify.request.define(ns + ".add", "ajax", {
            url: baseApiUrl + 'Add',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define(ns + ".delete", "ajax", {
            url: baseApiUrl + 'Delete',
            dataType: "json",
            type: "GET"
        });
        amplify.request.define(ns + ".update", "ajax", {
            url: baseApiUrl + 'Update',
            dataType: "json",
            type: "GET"
        });
        amplify.request.define(ns + ".getusersinrole", "ajax", {
            url: baseApiUrl + 'GetUsersInRole',
            dataType: "json",
            type: "POST"
        });
    };
    pub.addrole = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: ns + ".add",
            data: data,
            success: function (data) {
                parent.location.reload(true);
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);;
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };
    pub.deleterole = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: ns + ".delete",
            data: data,
            success: function (data) {
                parent.location.reload(true);
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);;
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };
    pub.updaterole = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: ns + ".update",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);;
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };
    pub.getusersinrole = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: ns + ".getusersinrole",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);;
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };
    return pub;
}(jQuery));