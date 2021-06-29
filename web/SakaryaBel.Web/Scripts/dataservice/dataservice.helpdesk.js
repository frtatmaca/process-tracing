var helpDeskService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("helpdesk.assignedUser", "ajax", {
            url: '/HelpDesk/AssignToUser',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("helpdesk.closeTickets", "ajax", {
            url: '/HelpDesk/CloseTickets',
            dataType: "json",
            contentType:"application/json",
            type: "POST"
        });
        amplify.request.define("helpdesk.makeAsRead", "ajax", {
            url: '/HelpDesk/MakeAsRead',
            dataType: "json",
            contentType: "application/json",
            type: "POST"
        });
    };

    pub.MakeAsRead = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "helpdesk.makeAsRead",
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


    pub.assignedUser = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "helpdesk.assignedUser",
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

    pub.closeTickets = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "helpdesk.closeTickets",
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