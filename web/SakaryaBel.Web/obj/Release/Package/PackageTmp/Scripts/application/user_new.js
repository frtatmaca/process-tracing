var user_new = (function ($) {
    var pub = {};

    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/);
        return pattern.test(emailAddress);
    };

    pub.init = function () {
        $('.btnTeacherNewSubmit').click(function () {
            var counter = 0;

            var displayName = $('#displayName').val();
            if (displayName === "") {
                counter++;
                $('#displayNameValidation').removeClass("hide");
                $('#displayNameValidation').addClass("show");
            }
            else {
                if (counter > 0)
                    counter--;
                $('#displayNameValidation').removeClass("show");
                $('#displayNameValidation').addClass("hide");
            }

            var teacherTitle = $('#teacherTitle').val();
            if (teacherTitle === "") {
                $('#titleValidation').removeClass("hide");
                $('#titleValidation').addClass("show");
                counter++;
            }
            else {
                if (counter > 0)
                    counter--;
                $('#titleValidation').removeClass("show");
                $('#titleValidation').addClass("hide");
            }

            var teacherEmail = $('#teacherEmail').val();
            if (teacherEmail === "") {
                $('#emailValidation').removeClass("hide");
                $('#emailValidation').addClass("show");
                counter++;
            }
            else {
                if (counter > 0)
                    counter--;
                $('#emailValidation').removeClass("show");
                $('#emailValidation').addClass("hide");
            }

            //if (!isValidEmailAddress(teacherEmail)) {
            //    $('#emailValidation').removeClass("hide");
            //    $('#emailValidation').addClass("show");
            //    counter++;
            //}
            //else {
            //    if (counter > 0)
            //        counter--;
            //    $('#emailValidation').removeClass("show");
            //    $('#emailValidation').addClass("hide");
            //}

            console.log(activitiesByType);
            var list = activitiesByType.join(",");
            console.log(list);
            $("input[name=fileTypeList]:hidden").val(list);
            if (counter <= 0)
                $('#frmUserNew').submit();

        });
    };



    return pub;
}(jQuery));