var questionService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("question.deleteQuestion", "ajax", {
            url: '/Question/Delete',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("question.deleteMultipleQuestion", "ajax", {
            url: '/Question/MultipleDelete',
            dataType: "json",
            type: "POST"
        });

    };

    pub.deleteMultipleQuestion = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "question.deleteMultipleQuestion",
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

    pub.deleteQuestion = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "question.deleteQuestion",
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