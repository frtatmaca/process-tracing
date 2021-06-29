var feedService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("feed.course", "ajax", {
            url: '/Feed/Course',
            dataType: "json",
            type: "POST",
            cache: {
                type: "persist",
                expires: 300000
            }
        });
   
        amplify.request.define("feed.profile", "ajax", {
            url: '/Feed/Profile',
            dataType: "json",
            type: "POST",
            cache: {
                type: "persist",
                expires: 300000
            }
        });

        amplify.request.define("feed.dashboard", "ajax", {
            url: '/Feed/Dashboard',
            dataType: "json",
            type: "POST",
            cache: {
            type: "persist",
            expires: 300000
            } 
        });

    };

    pub.course = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "feed.course",
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

    pub.profile = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "feed.profile",
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

    pub.dashboard = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "feed.dashboard",
            //data: data,
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