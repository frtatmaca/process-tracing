var globalizationService = (function ($) {
    var pub = {};
    pub.init = function () {

        amplify.request.define("globalization.getallglobalization", "ajax", {
            url: '/Globalization/GetAllGlobalization',
            dataType: "json",
            type: "POST"
        });
        
        amplify.request.define("globalization.getinfo", "ajax", {
            url: '/Globalization/GetInfo',
            dataType: "text",
            type: "POST"
        });
        
        amplify.request.define("globalization.save", "ajax", {
            url: '/Globalization/Save',
            dataType: "json",
            type: "POST"
        });
    };
   
    
    pub.getallglobalization = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "globalization.getallglobalization",
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
    pub.getinfo = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "globalization.getinfo",
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
   
    pub.save = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "globalization.save",
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