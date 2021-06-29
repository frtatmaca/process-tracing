var contactService = (function ($) {
    var pub = {};

    pub.init = function () {
        //Define getNews AJAX JSONP request in Amplify Request
        amplify.request.define("contactService.getall", "ajax", {
            url: "/api/ContactApi/",
            dataType: "json",
            type: "GET"
            //,
            //cache: {
            //    type: "persist",
            //    expires: amplifyjs_cacheDuration
            //}
        });

        amplify.request.define("contactService.deleteContact", "ajax", {
            url: "/api/ContactApi/{id}",
            dataType: "json",
            type: "DELETE"
        });

        amplify.request.define("contactService.addContact", "ajax", {
            url: "/api/ContactApi/",
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("contactService.updateContact", "ajax", {
            url: "/api/ContactApi/{id}",
            dataType: "json",
            type: "PUT"
        });
    };


    pub.getAll = function (successCallBack, errorCallBack) {
        //Request for news from amplify        
        amplify.request({
            resourceId: "contactService.getall",
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.addContact = function (data, successCallBack, errorCallBack) {
        //Request for news from amplify        
        amplify.request({
            resourceId: "contactService.addContact",
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

    pub.updateContact = function (data, successCallBack, errorCallBack) {
        //Request for news from amplify        
        amplify.request({
            resourceId: "contactService.updateContact",
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

    pub.deleteContact = function (data, successCallBack, errorCallBack) {
        //Request for news from amplify        
        amplify.request({
            resourceId: "contactService.deleteContact",
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
        amplifyJsClearRequestCache("contactService.getall");
    };



    return pub;
}(jQuery));