var courseService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("getCourseList", "ajax", {
            url: '/Course/GetCourseList/',
            dataType: "json",
            type: "POST"
        });

    };

    pub.getCourseLists = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "getCourseList",
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