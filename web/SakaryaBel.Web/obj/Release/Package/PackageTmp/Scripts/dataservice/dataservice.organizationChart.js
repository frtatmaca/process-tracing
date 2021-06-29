var organizationChartService = (function ($) {
    var pub = {};

    pub.init = function () {
        amplify.request.define("organizationChart.action", "ajax", {
            url: '/OrganizationChart/Action',
            dataType: "text",
            type: "POST"
        });

        amplify.request.define("organizationChart.getall", "ajax", {
            url: '/OrganizationChart/GetAllItems',
            dataType: "html",
            type: "POST"
        });

        amplify.request.define("organizationChart.getprogramdeleteinfo", "ajax", {
            url: '/OrganizationChart/GetProgramDeleteInfo',
            dataType: "html",
            type: "POST"
        });

    };

    pub.action = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "organizationChart.action",
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

    function getHtml(data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "organizationChart.getall",
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

    pub.getprogramdeleteinfo = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "organizationChart.getprogramdeleteinfo",
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

    pub.getListHtml = function ( successCallBack, errorCallBack) {
        getHtml({ viewMode: "list" }, successCallBack, errorCallBack);
    };
    pub.getTreeHtml = function (successCallBack, errorCallBack) {
        getHtml({ viewMode: "tree" }, successCallBack, errorCallBack);
    };
    return pub;
}(jQuery));