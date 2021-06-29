var banner_edit = (function ($) {
    var pub = {};


    pub.init = function () {
        $('.btnBannerEditSubmit').click(function () {
            var counter = 0;

            var bannerTitle = $('#bannerTitle').val();
            if (bannerTitle === "") {
                counter++;
                $('#titleValidation').removeClass("hide");
                $('#titleValidation').addClass("show");
            }
            else {
                if (counter > 0)
                    counter--;
                $('#titleValidation').removeClass("show");
                $('#titleValidation').addClass("hide");
            }

            var bannerSubTitle1 = $('#bannerSubTitle1').val();
            if (bannerSubTitle1 === "") {
                $('#SubTitle1Validation').removeClass("hide");
                $('#SubTitle1Validation').addClass("show");
                counter++;
            }
            else {
                if (counter > 0)
                    counter--;
                $('#SubTitle1Validation').removeClass("show");
                $('#SubTitle1Validation').addClass("hide");
            }
            
            console.log(activitiesByType);
            var list = activitiesByType.join(",");
            console.log(list);
            $("input[name=fileTypeList]:hidden").val(list);

            if (counter <= 0)
                $('#frmBannerChildEdit').submit();

        });
    };



    return pub;
}(jQuery));