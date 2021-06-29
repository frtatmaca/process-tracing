var message_contactUs = (function ($) {

    var pub = {};

    function isValidEmailAddress(emailAddress) {
        var pattern = new RegExp(/^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/);
        return pattern.test(emailAddress);
    };

    pub.init = function () {

        $("#btnClientMessageSend").click(function () {
            var message = $("#message").val();
            var uemail = $("#uemail").val();
            var uname = $("#uname").val();
            var web_site = $("#web_site").val();

            if (uname === "") {
                $('#err').html("");
                $('#err').append("Lütfen Isim Soyisim Kutusunu Doldurunuz.");
                return;
            }
            if (uemail === "") {
                $('#err').html("");
                $('#err').append("Lütfen Email Kutusunu Doldurunuz.");
                return;
            }
            if (!isValidEmailAddress(uemail)) {
                $('#err').html("");
                $('#err').append("Lütfen Geçerli bir Email adresi giriniz.");
                return;
            }
            if (message === "") {
                $('#err').html("");
                $('#err').append("Lütfen Mesaj Kutusunu Doldurunuz.");
                return;
            }
            $('#err').html("");

            $.ajax({
                url: "/Message/saveMessageComment/",
                type: "POST",
                dataType: "json",
                data: { uname: uname, uemail: uemail, uwebsite: web_site, messageBody: message },
                success: function (res) {
                    if (res == "success") {
                        alert("Message başarılı bir şekilde gönderildi.");
                        console.log("Message başarılı şekilde gönderildi.");
                        $("#message").val("");
                        $("#uemail").val("");
                        $("#uname").val("");
                        $("#web_site").val("");
                    } else {
                        alert("Message gönderimi başarısız.");
                        console.log("Message gönderimi başarısız.");
                    }
                },
                error: function (res) {
                    console.log("Error in process.");
                    //blmsCommon.hideLoading();
                }
            });
        });

    };

    return pub;
}(jQuery));