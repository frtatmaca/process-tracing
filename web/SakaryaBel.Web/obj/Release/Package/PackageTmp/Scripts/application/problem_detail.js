var problem_detail = (function ($) {

    var viewModel = function () {
    };

    var pub = {};
    var problemId, urlPhoto, userName,statusIcon;

    var msgTemplate = "<li style='padding-bottom: -20px !important'>" +
                        "<a class='authorimg' href=''><img alt='' src='{%createdUserPhotoUrl%}'></a>" +
                        "<div class='comment'>" +
                            "<div class='commentauthor'>" +
                                "<strong>{%createdUserName%}</strong> <span class='commenttime'>{%createdDate%}</span> - <a href='' class='commentreply'>Reply</a>" +
                            "</div>" +
                            "<p class='commentbody'>{%createdMesaj%}</p>" +
                        "</div>" +
                    "</li>";

    pub.init = function (refProblemId, refUserName,refIcon) {
        problemId = refProblemId;
        statusIcon = refIcon;
        urlPhoto = "/Files/user_unknown.png";
        userName = refUserName;
        $('#commentSend').click(function () {

            var refMesaj = $('#response').val(); //CKEDITOR.instances.ticketResponse.getData();

            $('#commentSend').attr('disabled', true);
            $.ajax({
                url: '/Problem/NewProblemMesaj/',
                type: 'POST',
                dataType: 'json',
                data: { problemId: problemId, mesaj: encodeURI(refMesaj) },
                success: function (data) {
                    console.log(data);

                    var line = msgTemplate;
                    line = line.replace('{%createdUserPhotoUrl%}', urlPhoto);
                    line = line.replace('{%createdDate%}', data);
                    line = line.replace('{%createdUserName%}', userName);
                    line = line.replace('{%createdMesaj%}', refMesaj);

                    $('.comments').append(line);

                    $('#response').val('');
                    $('#commentSend').removeAttr('disabled');
                },
                error: function (res) {
                    console.log('Error in process.');
                }
            });
        });


        $('#changeProblemStatus').click(function () {
            $.ajax({
                url: '/Problem/ChangeProblemStatus/',
                type: 'POST',
                dataType: 'json',
                data: { problemId: problemId, status: $(this).attr("data-id") },
                success: function (data) {
                    console.log(data);
                    $("#statusIcon").removeClass(statusIcon);
                    if ($(this).attr("data-id") == 0)
                        statusIcon = "btn-danger";
                    else if ($(this).attr("data-id") == 1)
                        statusIcon = "btn-success";
                    else if ($(this).attr("data-id") == 2)
                        statusIcon = "btn-info";

                    $("#statusIcon").addClass(statusIcon);
                },
                error: function (res) {
                    console.log('Error in process.');
                }
            });
        });
    };


    $('#userCommentPermission').change(function () {
        var sendVal;
        if (this.checked) {
            sendVal = true;
        } else {
            sendVal = false;
        }

        $.ajax({
            url: '/UserTicket/SetUserTicketCommentPermissionStatus/',
            type: 'POST',
            dataType: 'json',
            data: { userTicketId: userTicketId, status: sendVal },
            success: function (data) {
                console.log(data);
            },
            error: function (res) {
                console.log('Error in process.');
            }
        });
    });

    return pub;
}(jQuery));