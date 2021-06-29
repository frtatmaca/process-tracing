angular.module('sakaryabel.controllers', ["sakaryabel.services"])
.controller('LoginCtrl', function($scope, $ionicModal, $timeout,$ls,$location,Auth,Users) {

  $scope.logo = Auth.logo;
  $scope.user = {UserName:"system_admin",Password:"temppass2467"};
  if($ls.get('AuthToken'))
  {
    Auth.setToken($ls.get('AuthToken'));
    $location.path("/menu/dashboard")
  }
  $scope.doLogin = function () {
    console.log($ls.get('AuthToken'))
      if ($scope.user && $scope.user.UserName.length > 0 && $scope.user.Password.length > 0) {

        $scope.hasMessage = false;
        Users.getAuthToken($scope.user.UserName, $scope.user.Password).then(function (result) {
          if (result && result != 'null' && result.token) {

            $scope.hasMessage = false;
            $ls.set('AuthToken', result.token);
            Auth.setToken(result.token);
            $ls.set("userName",$scope.user.UserName);
            $ls.set("password",$scope.user.Password);
            $ls.setObject('CurrentUser', result.user);
            $location.path("/menu/dashboard")
          } else {
            swal({ title: 'Opps!', text: "Kullanıcı adı veya şifre hatalı", timer: 3000, type: 'error' });
          }
        });
      } else {
        swal({ title: 'Opps!', text:"Gerekli alanları doldurunuz", timer: 3000, type: 'error' });
      }


  };
  //
  // };

})
.controller('MenuCtrl', function($scope, $ionicModal, $timeout,$location,$ls,Auth,Users) {
  //$location.path("/menu/report");
  // $ionicModal.fromTemplateUrl('report.html', {
  //   scope: $scope
  // }).then(function(modal) {
  //   $scope.modal = modal;
  // });
  //   $scope.open=function(){
  //     $scope.modal.show();
  //   };
})
.controller('ReportCtrl', function($scope,$stateParams,$sce,$cordovaImagePicker,$cordovaCamera,$ionicModal,$timeout,$cordovaCapture,$cordovaFileTransfer,$location,Auth,Users) {

  $scope.resource={Url:"",Type:0,Name:"",MimeType:""};
  $scope.progress={Value:0};
  $scope.imageSrc="";
  $scope.report={};

  $ionicModal.fromTemplateUrl('report-modal.html', {
    scope: $scope,
    backdropClickToClose:false,
    animation: 'none',
    hardwareBackButtonClose:false
  }).then(function(modal) {
    $scope.modal = modal;
  });
  $ionicModal.fromTemplateUrl('progress-modal.html', {
    scope: $scope,
    backdropClickToClose:false,
    animation: 'none',
    hardwareBackButtonClose:false
  }).then(function(modal) {
    $scope.progressmodal = modal;
  });
  $ionicModal.fromTemplateUrl('image-modal.html', {
    scope: $scope
  }).then(function(modal) {
    $scope.imagemodal = modal;
  });

  $scope.showFullImage=function(){
    $scope.imageSrc=$scope.resource.Url;
    $scope.imagemodal.show();

  };

  $scope.closeImageModal=function(){
    $scope.imagemodal.hide();
  };
  $scope.closeModal = function() {
    $scope.modal.hide();
  };


  $scope.getResource=function(captureType,fromCamera){

      $scope.resource.Type=captureType;
      if(captureType==0){
          var options = {
              quality: 50, //0-100
              destinationType: Camera.DestinationType.FILE_URI, //DATA_URL (returns base 64) or FILE_URI (returns image path)
              allowEdit: true, //allow cropping
              encodingType: Camera.EncodingType.JPEG,
              popoverOptions: CameraPopoverOptions,
              saveToPhotoAlbum: false
            };
          if(fromCamera==0)
            options.sourceType=Camera.PictureSourceType.PHOTOLIBRARY;
          else if(fromCamera==1)
            options.sourceType=Camera.PictureSourceType.CAMERA;

          $cordovaCamera.getPicture(options).then(function(imageData) {
            console.log(imageData);
            $scope.resource.nativeUrl=imageData;
            $scope.resource.Url=$sce.trustAsResourceUrl(imageData);
            $scope.resource.Name=imageData.substr(imageData.lastIndexOf('/') + 1);
            $scope.resource.MimeType="image/jpeg";
            $scope.modal.show();
          });
      }
      else if(captureType==1){

          $cordovaCapture.captureVideo({limit:1}).then(function(data){
            $scope.resource.nativeUrl=data[0].fullPath
            $scope.resource.Url=$sce.trustAsResourceUrl($scope.resource.nativeUrl);
            $scope.resource.Name=data[0].name;
            $scope.resource.MimeType=data[0].type;
            $scope.modal.show();
          });
      }


  }
  $scope.sendReport=function(){
    //url http:// ile başlamalı
    // var url="firat78-001-site1.ctempurl.com/mobile/v1/PostFile";
    var options = {
      fileKey: "avatar",
      fileName: $scope.resource.Name,
      chunkedMode: false,
      mimeType: $scope.resource.MimeType,
      headers : {Connection: "close"}
    }
    var url="http://"+Settings.defaultApiHost+"/mobile/v1/PostFile?token="+Auth.token+"&type="+$scope.resource.Type;
    $scope.progressmodal.show();
    console.log(JSON.stringify($scope.resource.Url));
    $cordovaFileTransfer.upload(url, $scope.resource.nativeUrl, options)
    .then(function(result) {
      var res=JSON.parse(result.response);

      console.log(res);
      var data={Name: $scope.report.title,Description:$scope.report.description,FileId:res.fileID};
      Users.newProblem(data).then(function(dd){
        console.log(dd);
      });
      $scope.progress.Value=0;
      $scope.progressmodal.hide();
      $scope.modal.hide();
      $location.path("/menu/dashboard");
    }, function(err) {
      $scope.progress.Value=0;
    }, function (progress) {
      console.log(progress);
      $scope.progress.Value=(progress.loaded/progress.total)*100;
    });



  };
})
.controller('DashboardCtrl', function($scope,$location,$ionicModal,$ls,$sce,Users) {

  $scope.user=$ls.getObject("CurrentUser");
  $scope.problems={};
  $scope.resource={};


  if(!$scope.user.profileImage)
    $scope.user.profileImage="img/logo.png";

  $ionicModal.fromTemplateUrl('report-detail-modal.html', {
    scope: $scope
  }).then(function(modal) {
    $scope.modal = modal;
  });
  $ionicModal.fromTemplateUrl('image-modal.html', {
    scope: $scope
  }).then(function(modal) {
    $scope.imagemodal = modal;
  });
  $scope.showFullImage=function(){
    $scope.imageSrc=$scope.resource.Url;
    $scope.imagemodal.show();

  };
  $scope.newReport=function(){
    $location.path('/menu/report')
  };
  $scope.closeImageModal=function(){
    $scope.imagemodal.hide();
  };
  $scope.closeModal=function(){
    $scope.modal.hide();
  };

  $scope.problemDetail=function(id){
    Users.getProblem(id).then(function (result) {
      console.log(result)
      $scope.resource.title=result.name;
      $scope.resource.description=result.description;
      $scope.resource.description=result.description;
      $scope.resource.Type=result.fileType;
      $scope.resource.Url= $sce.trustAsResourceUrl("http://"+Settings.defaultApiHost+"/images/"+result.fileUrl);
      $scope.modal.show();

    });
  };

  Users.getProblemList({id:$scope.user.userId}).then(function (result) {
    $scope.problems=result;
  });


})
.controller('ProfileCtrl', function($scope,$location,$ionicModal,$ls,$sce,Users) {
  $scope.user=$ls.getObject("CurrentUser");
  $scope.problems={};
  $scope.resource={};

  $scope.changePicture=function(){
      var options = {
          quality: 50, //0-100
          destinationType: Camera.DestinationType.FILE_URI, //DATA_URL (returns base 64) or FILE_URI (returns image path)
          allowEdit: true, //allow cropping
          encodingType: Camera.EncodingType.JPEG,
          sourceType:Camera.PictureSourceType.PHOTOLIBRARY,
          popoverOptions: CameraPopoverOptions,
          saveToPhotoAlbum: false
        };


    $cordovaCamera.getPicture(options).then(function(imageData) {
      console.log(imageData);
      $scope.resource.nativeUrl=imageData;
      $scope.resource.Url=$sce.trustAsResourceUrl(imageData);
      $scope.resource.Name=imageData.substr(imageData.lastIndexOf('/') + 1);
      $scope.resource.MimeType="image/jpeg";
      $scope.modal.show();
    });
  };

  if(!$scope.user.profileImage)
    $scope.user.profileImage="img/logo.png";
})
;
