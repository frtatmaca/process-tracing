var classService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("class.getUsersInfos", "ajax", {
            url: '/Class/GetUsersInfos',
            dataType: "json",
            type: "GET"
        });

        amplify.request.define("class.deleteClass", "ajax", {
            url: '/Class/Delete',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("class.getProgramStudents", "ajax", {
            url: '/Class/GetProgStudentsForClass',
            dataType: "json",
            type: "GET"
        });

        amplify.request.define("class.getCommonUserList", "ajax", {
            url: '/Class/GetCommonUserList',
            dataType: "json",
            type: "GET"
        });
    };


    pub.getUsersInfos = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "class.getUsersInfos",
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


    pub.deleteClass = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "class.deleteClass",
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

    pub.getProgramStudents = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "class.getProgramStudents",
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

    pub.getCommonUserList = function (data, successCallBack, errorCallBack) {
        var excludeIdRefs = data.excludeIdRefs;
        var prmLimit = 1700;
        var wControl = true;
        while (wControl) {
            var excludePart = excludeIdRefs;
            var cBit = '';
            if (excludeIdRefs.length > prmLimit) {
                var part = excludeIdRefs.substring(0, prmLimit);
                var excludePart = part.substring(0, part.lastIndexOf('~') + 1);
                cBit = '+';
            }
            data.excludeIdRefs = excludePart+cBit;

            if (excludeIdRefs.length <= prmLimit) {
                wControl = false;
            }
            excludeIdRefs = excludeIdRefs.replace(excludePart, '');
            amplify.request({
                resourceId: "class.getCommonUserList",
                data: data,
                success: function (data) {
                    if (successCallBack && data.multiple === undefined) successCallBack(data);
                },
                error: function (message, level) {
                    if (typeof console !== "undefined" && typeof console.log !== "undefined") console.log(level + ": " + message);
                    if (errorCallBack) errorCallBack(message, level);
                }
            });
        }
       
    };

    return pub;
}(jQuery));