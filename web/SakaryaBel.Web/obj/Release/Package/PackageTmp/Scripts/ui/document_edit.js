$(function () {
    'use strict';
    var form = $("form");
    $("#btnCancel").on("click", function () {
        window.close();
    });

    var valDescription = "";
    $('#fileupload').fileupload({
        url: '/User/Edit/',
        add: function (e, data) {
            var goUpload = true;
            var uploadFile = data.files[0];            

            if (!(/\.(jpg|jpeg|png)$/i).test(uploadFile.name)) {
                alert('Zin verilen dosya türleri : jpg|jpeg|png');
                goUpload = false;
            }
            if (uploadFile.size > 5000000) {
                alert('Dosya boyutu en fazla 5 mb olabilir.');
                goUpload = false;
            }

            if (goUpload === true) {
                $('#fileContainer').html('<i class="fa fa-file"></i>&nbsp;' + data.files[0].name);

                data.context = $('#btnCreate');
                data.context.off();
                data.context.click(function () {
                    data.context.text('Kaydediliyor...').replaceAll($(this));
                    data.context.addClass('disabled');
                    data.submit();
                    form.submit(function (e) {
                        e.preventDefault();
                    });
                });
            }

        },
        done: function (e, data) {
           
            //if ($('#ddlUserType').val() == -1) {
            //    alert('Lütfen kullanıcı tipi seçiniz.');
            //    return false;
            //}

            if (data.result.status === "success") {
                //ShowGritter('Kayıt Başarılı', 'Ödev Kaydedildi. Ders listesine yönlendiriliyorsunuz', 'success');
                setTimeout(function () {
                    location.href = "/User/Index";
                }, 1000);

            } else {
                if (data.result.status === "warning") {
                    // ShowGritter('Kayıt Yapılamadı', data.result.message, 'warning');


                } else {
                    //ShowGritter('Hata Oluştu', data.result.message, 'error');
                    alert("Gerekli Alanalrı Doldurunuz.");
                }
                $('#btnCreate').removeClass('disabled').text('Kaydet');
            }

        },
        fail: function (e, data) {
            //ShowGritter('Hata Oluştu', data.result.message, 'error');
            $('#btnCreate').removeClass('disabled').text('Kaydet');

        }
    }).on('fileuploadprogressall', function (e, data) {
        var progress = parseInt(data.loaded / data.total * 100, 10);
        $('.progress .progress-bar').css('width', progress + '%');
    });

});

