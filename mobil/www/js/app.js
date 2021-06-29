// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.controllers' is found in controllers.js
angular.module('sakaryabel', ['ionic', 'ngCordova', 'sakaryabel.controllers', 'sakaryabel.utilities'])
.directive('equals', function() {
    return {
        restrict: 'A', // only activate on element attribute
        require: '?ngModel', // get a hold of NgModelController
        link: function(scope, elem, attrs, ngModel) {
            if(!ngModel) return; // do nothing if no ng-model

            // watch own value and re-validate on change
            scope.$watch(attrs.ngModel, function() {
                validate();
            });

            // observe the other value and re-validate on change
            attrs.$observe('equals', function (val) {
                validate();
            });

            var validate = function() {
                // values
                var val1 = ngModel.$viewValue;
                var val2 = attrs.equals;
                // set validity
                ngModel.$setValidity('equals', ! val1 || ! val2 || val1 === val2);
            };
        }
    }
})
.run(function ($ionicPlatform) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (window.cordova && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
            cordova.plugins.Keyboard.disableScroll(true);

        }
        if (window.StatusBar) {
            // org.apache.cordova.statusbar required
            StatusBar.styleDefault();
        }
    });
})

.config(function ($stateProvider, $urlRouterProvider, $ionicConfigProvider, $httpProvider) {
    if (window.Settings && window.Settings.hasOwnProperty('defaultRequestCaching'))
        $httpProvider.defaults.cache = window.Settings.defaultRequestCaching;
    else
        $httpProvider.defaults.cache = true;

    if (window.Settings && window.Settings.hasOwnProperty('defaultRequestTimeout'))
        $httpProvider.defaults.timeout = window.Settings.defaultRequestTimeout;
    else
        $httpProvider.defaults.timeout = 10000;

    $httpProvider.interceptors.push('httpRequestInterceptor');
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];

    //angular-translate(http://angular-translate.github.io)
    //http://angular-translate.github.io/docs/#/guide/12_asynchronous-loading
    //$translateProvider.useUrlLoader('http://stageblms2.advancity.net/Static/globalizatons.json');

    $stateProvider

      .state('menu', {
          url: '/menu',
          abstract: true,
          templateUrl: 'templates/menu.html',
          controller: 'MenuCtrl'
      })
    .state('login', {
        url: '/login',
        cache: false,
        templateUrl: 'templates/login.html',
        controller: 'LoginCtrl'
    })
    .state('menu.dashboard', {
        url: '/dashboard',
        cache: false,
        views: {
            'menuContent': {
                templateUrl: 'templates/dashboard.html',
                controller: 'DashboardCtrl'
            }
        }
    })
    .state('menu.dashboardCheif', {
        url: '/dashboardCheif',
        cache: false,
        views: {
            'menuContent': {
                templateUrl: 'templates/dashboardCheif.html',
                controller: 'dashboardCheifCtrl'
            }
        }
    })
    .state('menu.dashboardSuperCheif', {
        url: '/dashboardSuperCheif',
        cache: false,
        views: {
            'menuContent': {
                templateUrl: 'templates/dashboardSuperCheif.html',
                controller: 'DashboardCtrl'
            }
        }
    })
    .state('menu.reportdetail', {
        url: '/reportdetail/:reportId',
        views: {
            'menuContent': {
                templateUrl: 'templates/reportdetail.html',
                controller: 'ReportDetailCtrl'
            }
        }
    })
    .state('menu.profile', {
        url: '/profile',
        cache: false,
        views: {
            'menuContent': {
                templateUrl: 'templates/profile.html',
                controller: 'ProfileCtrl'
            }
        }
    })
    .state('menu.report', {
        url: '/report',
        views: {
            'menuContent': {
                templateUrl: 'templates/report.html',
                controller: 'ReportCtrl'
            }
        }
    })
     .state('menu.users', {
         url: '/users',
         cache: false,
         views: {
             'menuContent': {
                 templateUrl: 'templates/users.html',
                 controller: 'UserCtrl'
             }
         }
     })


    ;
    // if none of the above states are matched, use this as the fallback

    $urlRouterProvider.otherwise('/login');
});
