var activityService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("activity.getCourseViewJson", "ajax", {
            url: '/Activity/GetCourseViewJson',
            dataType: "json",
            type: "POST",
            cache: {
                type: "persist",
                expires: 15000
            }
        });

        amplify.request.define("activity.publish", "ajax", {
            url: '/Activity/Publish',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("activity.getCourseTermWeek", "ajax", {
            url: '/Activity/GetCourseTermWeek',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("activity.unpublish", "ajax", {
            url: '/Activity/Publish',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("activity.deleteActivity", "ajax", {
            url: '/Activity/Delete',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("activity.reportSearch", "ajax", {
            url: '/Activity/ReportSearch',
            dataType: "html",
            type: "POST"
        });

        amplify.request.define("activity.publishReplay", "ajax", {
            url: '/VirtualClass/PublishReplay',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("activity.getDeadlineCount", "ajax", {
            url: '/Activity/GetDeadlineCount',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("activity.getDeadlinesForTopNavigation", "ajax", {
            url: '/Activity/GetDeadlinesForTopNavigation',
            dataType: "html",
            type: "POST",
            cache: {
                type: "persist",
                expires: 120000
            }
        });
        amplify.request.define("activity.searchGrading", "ajax", {
            url: '/Activity/SearchGrading',
            dataType: "html",
            type: "POST"
        });

        amplify.request.define("activity.saveGrades", "ajax", {
            url: '/Activity/SaveGrades',
            dataType: "JSON",
            type: "POST"
        });

        amplify.request.define("activity.report", "ajax", {
            url: '/Activity/GetReport',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("activity.virtualclassreport", "ajax", {
            url: '/VirtualClass/VirtualClassReport',
            dataType: "html",
            type: "GET"
        });

    };


    pub.getCourseTermWeek = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.getCourseTermWeek",
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

    pub.getCourseViewJson = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.getCourseViewJson",
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

    pub.publish = function (data, successCallBack, errorCallBack) {
        data["publish"] = true;
        amplify.request({
            resourceId: "activity.publish",
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

    pub.unpublish = function (data, successCallBack, errorCallBack) {
        data["publish"] = false;
        amplify.request({
            resourceId: "activity.unpublish",
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

    pub.deleteActivity = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.deleteActivity",
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

    pub.reportSearch = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.reportSearch",
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

    pub.publishReplay = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.publishReplay",
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

    pub.getDeadlineCount = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.getDeadlineCount",
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

    pub.getDeadlinesForTopNavigation = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.getDeadlinesForTopNavigation",
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
    pub.searchGrading = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.searchGrading",
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

    pub.saveGrades = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.saveGrades",
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

    pub.getReport = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.report",
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

    pub.getVirtualClassReport = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "activity.virtualclassreport",
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