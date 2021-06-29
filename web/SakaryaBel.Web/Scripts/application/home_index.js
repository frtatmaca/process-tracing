var home_index = (function ($) {
    var pub = {};
    var TopBannerList;
    var TopVideoList;

    var viewModel = null;
    var bannerList = null;
    var videoList = null;

    pub.init = function () {
     
        $.ajax({
            url: "/Home/GetBannerList/",
            type: "POST",
            dataType: "json",         
            success: function (res) {
                console.log(res);
                bannerList = res;
            },
            error: function (res) {
                console.log("Error in process.");
                
            }
        });

        $.ajax({
            url: "/Home/GetVideoList/",
            type: "POST",
            dataType: "json",          
            success: function (res) {
                videoList = res;
                console.log(res);
            },
            error: function (res) {
                console.log("Error in process.");

            }
        });

        viewModel = {           
            TopBannerList: ko.observableArray(bannerList),
            TopVideoList: ko.observableArray(videoList),

        };

        console.log(viewModel);
        ko.applyBindings(viewModel);
     
    }

   

    return pub;
}(jQuery));