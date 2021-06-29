var assignmentService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("assignment.searchGrading", "ajax", {
            url: '/Assignment/SearchGrading',
            dataType: "html",
            type: "POST"
        });

        amplify.request.define("assignment.saveGrades", "ajax", {
            url: '/Assignment/SaveGrades',
            dataType: "JSON",
            type: "POST"
        });
    };


    pub.searchGrading = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "assignment.searchGrading",
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
            resourceId: "assignment.saveGrades",
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