var course_edit = (function ($) {
    var pub = {};


    pub.init = function () {
        $('.btnCourseEditSubmit').click(function () {
            var counter = 0;

            var activityName = $('#activityName').val();
            if (activityName === "") {
                counter++;
                $('#nameValidation').removeClass("hide");
                $('#nameValidation').addClass("show");
            }
            else {
                if (counter > 0)
                    counter--;
                $('#nameValidation').removeClass("show");
                $('#nameValidation').addClass("hide");
            }

            var activityAbbreviation = $('#activityAbbreviation').val();
            if (activityAbbreviation === "") {
                $('#activityAbbreviationValidation').removeClass("hide");
                $('#activityAbbreviationValidation').addClass("show");
                counter++;
            }
            else {
                if (counter > 0)
                    counter--;
                $('#activityAbbreviationValidation').removeClass("show");
                $('#activityAbbreviationValidation').addClass("hide");
            }

            var activityDescription = $('#activityDescription').val();
            if (activityDescription === "") {
                $('#activityDescriptionValidation').removeClass("hide");
                $('#activityDescriptionValidation').addClass("show");
                counter++;
            }
            else {
                if (counter > 0)
                    counter--;
                $('#activityDescriptionValidation').removeClass("show");
                $('#activityDescriptionValidation').addClass("hide");
            }

            console.log(activitiesByType);
            var list = activitiesByType.join(",");
            console.log(list);
            $("input[name=fileTypeList]:hidden").val(list);

            if (counter <= 0)
                $('#frmCourseEdit').submit();

        });
    };

    pub.fileStatusReplace = function (el) {
        var actId = $(el).attr("data-Id");
        var actStatus = $(el).attr("data-status");
        console.log(actId);
        console.log(actStatus);
        $.ajax({
            url: "/Course/FileStatusReplace/",
            type: "POST",
            dataType: "json",
            data: { actId: actId, status: actStatus },
            success: function (res) {
                if (res == "success") {
                    alert("File statusu değiştirildi");
                } else {
                    console.log("File statusu değiştirilir iken bir hata ile karşılaşıldı.");
                }
            },
            error: function (res) {
                console.log("Error in process.");
                //blmsCommon.hideLoading();
            }
        });
    }

    return pub;
}(jQuery));