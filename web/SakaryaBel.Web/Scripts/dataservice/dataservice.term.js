var termService = (function ($) {
    var pub = {};

    pub.init = function () {
        
        amplify.request.define("term.create", "ajax", {
            url: '/Term/Create',
            dataType: "json",
            type: "GET"

        });
        
      
        amplify.request.define("term.getAllTermItems", "ajax", {
            url: '/Term/GetAllTermItems',
            dataType: "json",
            type: "GET"
        });
        

        amplify.request.define("term.updateTermItem", "ajax", {
            url: '/Term/UpdateTermItem',
            dataType: "json",
            type: "POST"
        });

       amplify.request.define("term.deleteTermItem", "ajax", {
            url: '/Term/DeleteTermItem',
            dataType: "json",
            type: "POST"
        });

       amplify.request.define("term.addTermItem", "ajax", {
           url: '/Term/AddTermItem',
           dataType: "json",
           type: "POST"
       });


    };
  
    pub.create = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "term.create",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

   
    pub.getAllTermItems = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "term.getAllTermItems",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });

        
        
    };

    pub.updateTermItem = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "term.updateTermItem",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.deleteTermItem = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "term.deleteTermItem",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    pub.addTermItem = function (data, successCallBack, errorCallBack) {
        amplify.request({
            resourceId: "term.addTermItem",
            data: data,
            success: function (data) {
                if (successCallBack) successCallBack(data);
            },
            error: function (message, level) {
                console.log(level + ": " + message);
                if (errorCallBack) errorCallBack(message, level);
            }
        });
    };

    return pub;
}(jQuery));