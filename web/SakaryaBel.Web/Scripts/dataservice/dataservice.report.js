var reportService = (function ($) {
    var pub = {};

    pub.init = function () {      
        amplify.request.define("getUserDetailAccessReportTime", "ajax", {
            url: '/Admin/GetUserDetailAccessTime/',
            dataType: "json",
            type: "POST"
        });      
      
    };
       
    pub.getDetailAccessTimeReport = function (data, successCallBack, errorCallBack) {
      
        amplify.request({
            resourceId: "getUserDetailAccessReportTime",
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