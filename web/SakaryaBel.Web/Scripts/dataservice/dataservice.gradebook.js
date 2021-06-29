var gradebookService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("gradebook.publishing", "ajax", {
            url: '/GradebookItem/Publishing',
            dataType: "json",
            type: "POST"
        });

    };


    pub.publishing = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "gradebook.publishing",
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