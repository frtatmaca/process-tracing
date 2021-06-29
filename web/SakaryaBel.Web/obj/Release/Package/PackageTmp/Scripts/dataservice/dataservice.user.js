var userService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("getUserList", "ajax", {
            url: '/User/GetUserList/',
            dataType: "json",
            type: "POST"
        });              
    };

    pub.getUserLists = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "getUserList",
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