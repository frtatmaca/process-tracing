var reference_edit = (function ($) {
    var pub = {};


    pub.init = function () {
        $('.btnReferenceEditSubmit').click(function () {
            var counter = 0;

            var title = $('#referenceTitle').val();
            if (title === "") {
                counter++;
                $('#referenceTitleValidation').removeClass("hide");
                $('#referenceTitleValidation').addClass("show");
            }
            else {
                if (counter > 0)
                    counter--;
                $('#referenceTitleValidation').removeClass("show");
                $('#referenceTitleValidation').addClass("hide");
            }

            var referenceDescription = $('#referenceDescription').val();
            if (referenceDescription === "") {
                $('#referenceDescriptionValidation').removeClass("hide");
                $('#referenceDescriptionValidation').addClass("show");
                counter++;
            }
            else {
                if (counter > 0)
                    counter--;
                $('#referenceDescriptionValidation').removeClass("show");
                $('#referenceDescriptionValidation').addClass("hide");
            }

            console.log(activitiesByType);
            var list = activitiesByType.join(",");
            console.log(list);
            $("input[name=fileTypeList]:hidden").val(list);

            console.log("counte: " + counter);
            if (counter <= 0)
                $('#frmReferenceEdit').submit();

        });
    };



    return pub;
}(jQuery));