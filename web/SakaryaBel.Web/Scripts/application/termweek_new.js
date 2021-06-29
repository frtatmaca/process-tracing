var termweek_new = (function ($) {
    var pub = {};


    pub.init = function () {
        $('.btnTermWeekSubmit').click(function () {
            var counter = 0;

            var weekTitle = $('#weekTitle').val();
            if (weekTitle === "") {
                counter++;
                $('#weekTitleValidation').removeClass("hide");
                $('#weekTitleValidation').addClass("show");
            }
            else {
                if (counter > 0)
                    counter--;
                $('#weekTitleValidation').removeClass("show");
                $('#weekTitleValidation').addClass("hide");
            }

            //var activityAbbreviation = $('#activityAbbreviation').val();
            //if (activityAbbreviation === "") {
            //    $('#activityAbbreviationValidation').removeClass("hide");
            //    $('#activityAbbreviationValidation').addClass("show");
            //    counter++;
            //}
            //else {
            //    if (counter > 0)
            //        counter--;
            //    $('#activityAbbreviationValidation').removeClass("show");
            //    $('#activityAbbreviationValidation').addClass("hide");
            //}

            //var activityDescription = $('#activityDescription').val();
            //if (activityDescription === "") {
            //    $('#activityDescriptionValidation').removeClass("hide");
            //    $('#activityDescriptionValidation').addClass("show");
            //    counter++;
            //}
            //else {
            //    if (counter > 0)
            //        counter--;
            //    $('#activityDescriptionValidation').removeClass("show");
            //    $('#activityDescriptionValidation').addClass("hide");
            //}

            console.log("counte: " + counter);
            if (counter <= 0)
                $('#frmTermWeekNew').submit();

        });
    };



    return pub;
}(jQuery));