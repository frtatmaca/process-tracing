var bannerService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("getBannerList", "ajax", {
            url: '/Banner/GetBannerList/',
            dataType: "json",
            type: "POST"
        });

        console.log("sdasdasdas");
    };

    pub.getBannerLists = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "getBannerList",
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