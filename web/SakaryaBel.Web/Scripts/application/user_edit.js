var user_edit = (function ($) {
    var pub = {};

    var userId;
    pub.init = function (refUserId) {
        userId = refUserId;

        $('.changeUserStatus').click(function () {
            console.log("changeUserStatus:" + $(this).attr("data-id"));
            $.ajax({
                url: '/User/ChangeUserStatus/',
                type: 'POST',
                dataType: 'json',
                data: { userId: userId, status: $(this).attr("data-id") },
                success: function (data) {
                    console.log("ChangeUserStatus: " + data);
                    $("#userStatus").html("");
                    $("#userStatus").html(data);
                },
                error: function (res) {
                    console.log('Error in process.');
                }
            });
        });
       
    };



    return pub;
}(jQuery));