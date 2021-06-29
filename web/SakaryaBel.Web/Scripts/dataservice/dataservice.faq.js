var faqEditService = (function ($) {
    var pub = {};

    pub.init = function () {


        amplify.request.define("faq.updateDisplayOrder", "ajax", {
            url: "/Faq/UpdateFaqDisplayOrder",
            dataType: "json",
            type: "POST"
        });



        amplify.request.define("faqCategory.updateDisplayOrder", "ajax", {
            url: "/Faq/UpdateCategoryFaqDisplayOrder",
            dataType: "json",
            type: "POST"
        });

    };


    //UpdateCategoryFaqDisplayOrder


    pub.updateDisplayOrder = function (data, successCallBack, errorCallBack) {
        //Request for news from amplify   

        amplify.request({
            resourceId: "faq.updateDisplayOrder",
            data: data,
            success: function (data) {
                parent.location.reload(true);
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.updateCategoryFaqDisplayOrder = function (data, successCallBack, errorCallBack) {
        //Request for news from amplify   

        amplify.request({
            resourceId: "faqCategory.updateDisplayOrder",
            data: data,
            success: function (data) {
                parent.location.reload(true);
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