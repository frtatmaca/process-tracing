var course_new = (function ($) {
    var pub = {};


    pub.init = function () {
        $('.btnCourseNewSubmit').click(function () {
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
                $('#frmCourseNew').submit();

        });
    };



    return pub;
}(jQuery));