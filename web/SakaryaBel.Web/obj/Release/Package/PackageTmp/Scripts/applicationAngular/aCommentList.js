var aCommentList = (function ($) {
    var pub = {};
    pub.init = function () {
        console.log("angular page start");
        angularStart();
    };

    function angularStart() {
        function MainController($scope, $http, $log, $interval) {
            //$scope.Message = "Angular js ögreniyortum";

            //$scope.User = {
            //    Name: 'Firat Atmaca',
            //    Location: 'Turkey',
            //    Image: 'https://scontent-fra.xx.fbcdn.net/hphotos-xaf1/v/t1.0-9/382675_10151629112531842_103592849_n.jpg?oh=c49d2f39b8ca686337dac37a3392ad0c&oe=5577ECA6'
            //};

            $scope.orderKey = "stargazers_count";
            $scope.searchKey = "angular";

            $scope.counter = 5;

            var countBack = function () {
                $scope.counter--;
                if ($scope.counter < 1) {
                    $scope.Search($scope.searchKey);
                    $scope.counter = null;
                }
            }

            var intervalCount = null;

            var Count = function () {
                intervalCount = $interval(countBack, 1000, $scope.counter)
            }

            Count();
            $scope.Search = function (key) {
                $http.get("https://api.github.com/users/" + key).success(
                function (data) {
                    $log.info("All Users Data Taken");
                    $scope.User = data;

                    if (intervalCount) {
                        $interval.cancel(intervalCount);
                        $scope.counter = null;
                    }

                    $http.get($scope.User.repos_url).success(
                        function (response) {
                            $log.info("All Users Repos Data Taken");
                            $scope.Repos = response;
                        }).error(function (ex) { $log.info(ex) });
                }
                ).error(function (ex) {
                    console.log("Hata: " + ex);
                    $log.info(ex);
                });
            }


        };


        var app = angular.module("app", []);

        app.controller("MainController", ["$scope", "$http", "$log", "$interval", MainController]);
    }
    return pub;
}(jQuery));