var announcementService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("announcement.writeAnnouncementCompose", "ajax", {
            url: '/Announcement/WriteAnnouncementCompose',
            dataType: "string",
            type: "POST"
        });

        amplify.request.define("Announcement.trackingAnnouncement", "ajax", {
            url: '/Announcement/TrackingAnnouncement',
            dataType: "string",
            type: "GET"
        });

        amplify.request.define("Announcement.getUserwithSearching", "ajax", {
            url: '/Announcement/GetUserwithSearching',
            dataType: "string",
            type: "GET"
        });

        amplify.request.define("announcement.homePageAnnouncement", "ajax", {
            url: '/Announcement/LoadAnnouncement',
            dataType: "json",
            type: "POST",
            cache: {
                type: "persist",
                expires: 300000
            }
        });

    };

    pub.writeAnnouncementCompose = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "announcement.writeAnnouncementCompose",
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


    pub.trackingAnnouncement = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "Announcement.trackingAnnouncement",
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

    pub.getUserwithSearching = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "Announcement.getUserwithSearching",
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

    pub.getHomePageAnnouncement = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "announcement.homePageAnnouncement",
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