angular.module('sakaryabel.controllers', ["sakaryabel.services", "sakaryabel.utilities"])
.controller('LoginCtrl', function ($scope, $ionicModal, $timeout, $ls, $location, Auth, Users) {

    $scope.logo = Auth.getLogo();
    $scope.user = { UserName: "system_admin", Password: "temppass2467" };
    if ($ls.get('AuthToken')) {
        $scope.user.UserName = $ls.get('userName')
        $scope.user.Password = $ls.get('password')
        login();
    }
    function login() {
        if ($scope.user && $scope.user.UserName.length > 0 && $scope.user.Password.length > 0) {

            $scope.hasMessage = false;
            Users.getAuthToken($scope.user.UserName, $scope.user.Password).then(function (result) {
                if (result && result != 'null' && result.token) {

                    $scope.hasMessage = false;                    
                    Auth.setToken(result.token);
                    Auth.setUserName($scope.user.UserName);
                    Auth.setPassword($scope.user.Password);
                    result.user.profileImage = "http://" + Settings.defaultApiHost + result.user.profileImage;
                    $ls.setObject('CurrentUser', result.user);
                    $location.path("/menu/dashboard")

                     if(result.user.userType==1)
                         $location.path("/menu/dashboardCheif")
                     else  if(result.user.userType==0)
                       $location.path("/menu/dashboard")
                     else  if(result.user.userType==2)
                       $location.path("/menu/dashboardSuperCheif");

                } else {
                    swal({ title: 'Opps!', text: "Kullanıcı adı veya şifre hatalı", timer: 3000, type: 'error' });
                }
            });
        } else {
            swal({ title: 'Opps!', text: "Gerekli alanları doldurunuz", timer: 3000, type: 'error' });
        }
    }
    $scope.doLogin = function () {
        login();
    };

})
.controller('MenuCtrl', function ($scope, $ionicModal, $timeout, $location, $ls, Auth, Users) {

})
.controller('ReportCtrl', function ($scope, $stateParams, $sce, $cordovaImagePicker, $cordovaCamera, $ionicModal, $timeout, $cordovaCapture, $cordovaFileTransfer, $location, Auth, Users, Resource) {

    $scope.resource = { Url: "", Type: 0, Name: "", MimeType: "" };
    $scope.progress = { Value: 0 };
    $scope.imageSrc = "";
    $scope.report = {};

    $ionicModal.fromTemplateUrl('report-modal.html', {
        scope: $scope,
        backdropClickToClose: false,
        animation: 'none',
        hardwareBackButtonClose: false
    }).then(function (modal) {
        $scope.modal = modal;
    });
    $ionicModal.fromTemplateUrl('progress-modal.html', {
        scope: $scope,
        backdropClickToClose: false,
        animation: 'none',
        hardwareBackButtonClose: false
    }).then(function (modal) {
        $scope.progressmodal = modal;
    });
    $ionicModal.fromTemplateUrl('image-modal.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.imagemodal = modal;
    });

    $scope.showFullImage = function () {
        $scope.imageSrc = $scope.resource.Url;
        $scope.imagemodal.show();

    };

    $scope.closeImageModal = function () {
        $scope.imagemodal.hide();
    };
    $scope.closeModal = function () {
        $scope.modal.hide();
    };


    $scope.getResource = function (captureType, fromCamera) {

        if (captureType == 0) {
            Resource.GetImage(fromCamera).then(function (resource) {
                $scope.resource = resource;
                $scope.modal.show();
            });
        }
        else if (captureType == 1) {
            Resource.GetVideo().then(function (resource) {
                $scope.resource = resource;
                $scope.modal.show();
            });
        }
    };
    $scope.sendReport = function () {
        //url http:// ile başlamalı
        // var url="firat78-001-site1.ctempurl.com/mobile/v1/PostFile";
        $scope.progressmodal.show();

        Resource.Upload($scope.resource).then(function (result) {
            var res = JSON.parse(result.response);
            var data = { Name: $scope.report.title, Description: $scope.report.description, FileId: res.fileID };
            Users.newProblem(data).then(function (dd) {
                console.log(dd);
            });
            $scope.progress.Value = 0;
            $scope.progressmodal.hide();
            $scope.modal.hide();
            $location.path("/menu/dashboard");
        }, function (err) {
            console.log(err);
        },
          function (progress) {
              $scope.progress.Value = progress;
              console.log(progress);
          });
    };
})
.controller('DashboardCtrl', function ($scope, $location, $ionicModal, $ls, $sce, Users) {

    $scope.user = $ls.getObject("CurrentUser");
    $scope.problems = {};
    $scope.resource = {};

    $ionicModal.fromTemplateUrl('report-detail-modal.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.modal = modal;
    });
    $ionicModal.fromTemplateUrl('image-modal.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.imagemodal = modal;
    });
    $scope.showFullImage = function () {
        $scope.imageSrc = $scope.resource.Url;
        $scope.imagemodal.show();

    };
    $scope.newReport = function () {
        $location.path('/menu/report')
    };
    $scope.closeImageModal = function () {
        $scope.imagemodal.hide();
    };
    $scope.closeModal = function () {
        $scope.modal.hide();
    };

    $scope.problemDetail = function (id) {
        Users.getProblem(id).then(function (result) {
            console.log(result)
            $scope.resource.title = result.name;
            $scope.resource.description = result.description;
            $scope.resource.description = result.description;
            $scope.resource.Type = result.fileType;
            $scope.resource.Url = $sce.trustAsResourceUrl("http://" + Settings.defaultApiHost + "/Files/" + result.fileUrl);
            $scope.modal.show();

        });
    };

    Users.getProblemList({ id: $scope.user.userId }).then(function (result) {
        $scope.problems = result;
    });


})
.controller('dashboardCheifCtrl', function ($scope, $location, $ionicModal, $ls, $sce, Users) {
    $scope.user = $ls.getObject("CurrentUser");
   
   
})
.controller('ProfileCtrl', function ($scope, $location, $ionicModal, $ls, $sce, Users, Resource) {
    $scope.user = {};
    $scope.currenUser = $ls.getObject("CurrentUser");
    $scope.progress = {};
    $ionicModal.fromTemplateUrl('changepassword-modal.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.modal = modal;
    });
    $ionicModal.fromTemplateUrl('progress-modal.html', {
        scope: $scope,
        backdropClickToClose: false,
        animation: 'none',
        hardwareBackButtonClose: false
    }).then(function (modal) {
        $scope.progressmodal = modal;
    });
    $scope.logOut = function () {
        $ls.removeAll();
        $location.path("/login");
    }
    $scope.openChangePassword = function () {
        $scope.modal.show();
    };

    $scope.changePicture = function () {
        Resource.GetImage(0).then(function (resource) {
            console.log(resource);
            $scope.progressmodal.show();

            Resource.Upload(resource).then(function (data) {
                var response = JSON.parse(data.response);
                if (data.responseCode == 200) {
                    Users.changeProfileImage($scope.currenUser.userId, response.fileID).then(function (xd) {
                        swal({ title: 'Profil Resmi Değişikliği', text: "Profil fotoğrafınız başarıyla değiştirildi.", timer: 3000, type: 'success' });

                        $scope.progressmodal.hide();
                        $location.path("/login");
                    });
                }
                else {
                    swal({ title: 'Opps!', text: "Profil Fotoğrafı Değiştirilirken Bir Hata Oluştu", timer: 2000, type: 'error' });

                }

            }, function (err) {
                console.log(err);
            },
            function (progress) {
                $scope.progress.Value = progress;
            });

        });
    };
    $scope.changePassword = function () {
        if ($scope.user)
            if ($scope.user.OldPassword !== $ls.get('password'))
                swal({ title: 'Opps!', text: "Eski şifreniz yanlış", timer: 3000, type: 'error' });
            else if ($scope.user.NewPassword != $scope.user.ConfirmPassword)
                swal({ title: 'Opps!', text: "Şifreler birbiriyle uyuşmuyor", timer: 3000, type: 'error' });
            else {
                $scope.user.Id = $scope.currenUser.userId;
                Users.changePassword($scope.user).then(function (data) {
                    if (data && data.status == 200) {
                        swal({ title: 'Şifre Değişikliği', text: "Şifreniz başarıyla değiştirildi. Lütfen giriş yapınız.", timer: 3000, type: 'success' });
                        $scope.modal.hide();

                        $location.path("/login")
                    }
                    else {
                        swal({ title: 'Şifre Değişikliği', text: "Şifreniz değiştirilirken bir Talep oluştu. Lütfen Tekrar deneyiniz.", timer: 3000, type: 'error' });

                    }
                    $scope.logOut();

                });
            }
        else
            swal({ title: 'Opps!', text: "Tüm alanları doldurunuz.", timer: 3000, type: 'error' });
    };
})
.controller("UserCtrl", function ($scope, $ionicModal, Users,$ls) {
    $scope.user = { Name: "", Surname: "", Username: "", Email: "", Password: "", ConfirmPassword: "", UserType: 2, CheifId: "" };
    $scope.user.CheifId = $ls.getObject("CurrentUser").userId;
    $scope.users = {};
    Users.getUserList({}).then(function (userList) {
        $scope.users = userList;
    });
    $scope.addUser = function (data) {
        console.log(data)

        Users.newUser(data).then(function (xd) {
            console.log(xd)
        });

    
    };
    $ionicModal.fromTemplateUrl('new-user.html', {
        scope: $scope
    }).then(function (modal) {
        $scope.newusermodal = modal;
    });
    $scope.openNewUserModal = function () {
        $scope.newusermodal.show();
    };
})
;
