var termweekService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("getTermWeekList", "ajax", {
            url: '/TermWeek/GetTermWeekList/',
            dataType: "json",
            type: "POST"
        });
        console.log("termweeklist");
    };

    pub.getTermWeekLists = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "getTermWeekList",
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