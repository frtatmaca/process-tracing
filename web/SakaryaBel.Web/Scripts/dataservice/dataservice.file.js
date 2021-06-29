var fileService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("file.getmyfiles", "ajax", {
            url: '/File/GetMyFiles',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("file.getsharedfiles", "ajax", {
            url: '/File/GetSharedFiles',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("file.getftpfiles", "ajax", {
            url: '/File/GetFtpFiles',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("file.uploadcomplete", "ajax", {
            url: '/File/UploadComplete',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("file.deletemyfiles", "ajax", {
            url: '/File/DeleteMyFiles',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("file.deletesharedfiles", "ajax", {
            url: '/File/DeleteSharedFiles',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("file.deleteftpfiles", "ajax", {
            url: '/File/DeleteFTPFiles',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("file.copytosharedfiles", "ajax", {
            url: '/File/CopyFTPFilesToSharedFiles',
            dataType: "json",
            type: "POST"
        });
        amplify.request.define("file.getmyrelatedfiles", "ajax", {
            url: '/File/GetMyRelatedFiles',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("file.getfiles", "ajax", {
            url: '/File/GetFiles',
            dataType: "json",
            type: "POST"
        });
    };
    pub.doAction = function (action, data, successCallBack, errorCallBack) {
        var resId = "file." + action;

        amplify.request({
            resourceId: resId,
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

    pub.getFiles = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "file.getfiles",
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