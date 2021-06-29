var syncJobService = (function ($) {

    var pub = {};

    pub.init = function () {
        amplify.request.define("sync.getJobs", "ajax", {
            url: '/Sync/GetJobs',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("sync.getClassList", "ajax", {
            url: '/Sync/GetClassList',
            dataType: "json",
            type: "POST"
        });      
    };

    pub.getJobs = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "sync.getJobs",
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


    pub.getClassList = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "sync.getClassList",
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