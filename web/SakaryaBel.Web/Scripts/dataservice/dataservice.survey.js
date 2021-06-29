var surveyService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("survey.saveSurvey", "ajax", {
            url: '/Api/WebApiSurvey/SaveSurvey',
            dataType: "json",
            type: "POST"
        });


    };

    pub.saveSurvey = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "survey.saveSurvey",
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