var groupService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("group.getUsersInfos", "ajax", {
            url: '/Group/GetUsersInfos',
            dataType: "json",
            type: "GET"
        });

        amplify.request.define("group.getUsers", "ajax", {
            url: '/Group/GetUsers',
            dataType: "json",
            type: "GET"
        });

        amplify.request.define("group.pasiveStatus", "ajax", {
            url: '/Group/PassiveGroupsStatus',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("group.getUsersGroup", "ajax", {
            url: '/Group/GetUsersGroupAsync',
            dataType: "json",
            type: "POST"
        });
    };

    pub.getUsers = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "group.getUsers",
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

    pub.getUsersInfos = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "group.getUsersInfos",
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

    pub.pasiveStatus = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "group.pasiveStatus",
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

    pub.getUsersGroup = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "group.getUsersGroup",
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