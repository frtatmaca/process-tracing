var loginService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("login.updateLoginAudits", "ajax", {
            url: '/Api/WebApiAccount/UpdateLoginAudits',
            dataType: "json",
            type: "POST"
        });
    };

    pub.updateLoginAudits = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "login.updateLoginAudits",
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