var userPreferenceService = (function ($) {
    var pub = {};
    pub.init = function () {
        amplify.request.define("userPreference.save", "ajax", {
            url: '/UserPreference/Save',
            dataType: "json",
            type: "POST",
            async: true
        });
    };
    pub.save = function (data, successCallBack, errorCallBack) {
        // allow the interface to react
        setTimeout(function () {
            amplify.request({
                resourceId: "userPreference.save",
                data: data,
                success: function (data) {
                    if (successCallBack) successCallBack(data);
                },
                error: function (message, level) {
                    if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                    if (errorCallBack) errorCallBack(message, level);
                }
            });
        }, 100);
    };
    return pub;
}(jQuery));