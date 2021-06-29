var messageService = (function ($) {
    var pub = {};

    pub.init = function () {        
        amplify.request.define("getMessageList", "ajax", {
            url: '/Message/GetMessageList/',
            dataType: "json",
            type: "POST"
        }); 
    };

    pub.getMessageLists = function (data, successCallBack, errorCallBack) {

        amplify.request({
            resourceId: "getMessageList",
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