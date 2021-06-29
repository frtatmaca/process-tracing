var organizationService = (function ($) {
    var pub = {};

    pub.init = function () {

        amplify.request.define("organization.getOrganizationPrograms", "ajax", {
            url: '/Organization/GetOrganizationPrograms',
            dataType: "json",
            type: "GET"
        });

        amplify.request.define("organization.editMailTemplate", "ajax", {
            url: '/Organization/EditMailTemplate',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("organization.saveAddressBinding", "ajax", {
            url: '/Organization/SaveAddressBinding',
            dataType: "json",
            type: "POST"
        });

        amplify.request.define("organization.managemenuandmodules", "ajax", {
            url: '/Organization/ManageMenuAndModules',
            dataType: "json",
            contentType: 'application/json',
            type: "POST"
        });

        

    };

    pub.manageMenuAndModules = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "organization.managemenuandmodules",
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

    pub.editMailTemplate = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "organization.editMailTemplate",
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

    pub.saveAddressBinding = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "organization.saveAddressBinding",
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

    pub.getOrganizationPrograms = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "organization.getOrganizationPrograms",
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