var securityService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("security.deletegroup", "ajax", {
            url: "/Security/DeleteSecurityActionGroup/{id}",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("security.updategroup", "ajax", {
            url: "/Security/UpdateSecurityActionGroup",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("security.addgroup", "ajax", {
            url: "/Security/AddSecurityActionGroup",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("security.getallgroup", "ajax", {
            url: "/Security/GetAllSecurityActionGroup/{id}",
            dataType: "json",
            type: "GET"
        });

    };

    pub.deleteGroup = function (data, successCallBack, errorCallBack) {
        //Request for news from amplify        
        amplify.request({
            resourceId: "security.deletegroup",
            data: data,
            success: function (data) {
                pub.refresh();
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.updateGroup = function (data, successCallBack, errorCallBack) {
        //Request for news from amplify   

        amplify.request({
            resourceId: "security.updategroup",
            data: data,
            success: function (data) {
                pub.refresh();
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.addGroup = function (data, successCallBack, errorCallBack) {
        //Request for news from amplify        
        amplify.request({
            resourceId: "security.addgroup",
            data: data,
            success: function (data) {
                pub.refresh();
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.getallGroup = function (data, successCallBack, errorCallBack) {
        //Request for news from amplify        
        amplify.request({
            resourceId: "security.getallgroup",
            data: data,
            success: function (data) {
                pub.refresh();
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.refresh = function () {
        amplifyJsClearRequestCache("security.getallgroup");
    };
    return pub;
}(jQuery));