var masterCourseService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("masterCourse.getMasterCourses", "ajax", {
            url: 'MasterCourse/Search',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("masterCourse.deleteMasterCourse", "ajax", {
            url: '/MasterCourse/MultiDelete',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("masterCourse.fiter", "ajax", {
            url: '/MasterCourse/GetMasterCourses',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("masterCourse.updateStatus", "ajax", {
            url: '/MasterCourse/UpdateStatus',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("masterCourse.courseCatalog", "ajax", {
            url: '/MasterCourse/SearchCourseCatalog',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("masterCourse.removeEnrollment", "ajax", {
            url: '/MasterCourse/RemoveEnrollment',
            dataType: "json",
            type: "POST"
        });

    };

    pub.getMasterCourses = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "masterCourse.getMasterCourses",
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

    pub.deleteMasterCourse = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "masterCourse.deleteMasterCourse",
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

    pub.filter = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "masterCourse.fiter",
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

    pub.updateStatus = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "masterCourse.updateStatus",
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

    pub.searchCourseCatalog = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "masterCourse.courseCatalog",
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

    pub.removeEnrollment = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "masterCourse.removeEnrollment",
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