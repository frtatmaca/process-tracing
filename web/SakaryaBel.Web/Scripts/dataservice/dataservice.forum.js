var forumService = (function ($) {
    var pub = {};
    pub.init = function () {

        amplify.request.define("forum.createThreadSave", "ajax", {
            url: '/Forum/CreateThread',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("forum.makeSticky", "ajax", {
            url: '/Forum/SetThreadSticky',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("forum.closeThread", "ajax", {
            url: '/Forum/CloseThread',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("forum.deleteThread", "ajax", {
            url: '/Forum/DeleteThread',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("forum.sendMessage", "ajax", {
            url: '/Forum/CreateMessage',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("forum.approveAll", "ajax", {
            url: '/Forum/ApproveMessage',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("forum.deleteAll", "ajax", {
            url: '/Forum/DeleteMessage',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("forum.makeSticky", "ajax", {
            url: '/Forum/SetThreadSticky',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("forum.closeThread", "ajax", {
            url: '/Forum/CloseThread',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("forum.deleteThread", "ajax", {
            url: '/Forum/DeleteThread',
            dataType: "json",
            type: "POST"
        });
    };
    pub.deleteThread = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "forum.deleteThread",
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

    pub.closeThread = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "forum.closeThread",
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

    pub.makeSticky = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "forum.makeSticky",
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

    pub.deleteAll = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "forum.deleteAll",
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

    pub.approveAll = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "forum.approveAll",
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

    pub.sendMessage = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "forum.sendMessage",
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

    pub.createThreadSave = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "forum.createThreadSave",
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
    pub.deleteThread = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "forum.deleteThread",
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

    pub.closeThread = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "forum.closeThread",
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
    pub.makeSticky = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "forum.makeSticky",
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