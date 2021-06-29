
(function () {
    function MainController($scope,$http,$log,$interval) {
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
            $http.get("https://api.github.com/users/"+key).success(
            function (data) {
                $log.info("All Users Data Taken");
                $scope.User = data;

                if (intervalCount)
                {
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


    var app = angular.module("app", ['directApp']);

    app.controller("MainController", ["$scope", "$http", "$log", "$interval", MainController]);
   
})(angular);






//javascript Abstraction
/*
var Docoding = function (f) {
    f();
}

var coding1 = function () {
    console.log("coding1 start")
    try{
        console.log("coding1");
    } catch (ex) {
        console.log(ex);
    }

    console.log("coding1 end")
}


var coding2 = function () {
    console.log("coding2 start")
    try {
        console.log("coding2");
    } catch (ex) {
        console.log(ex);
    }

    console.log("coding2 end")
}

Docoding(coding1);
Docoding(coding2);  */
//----------------------------------------
//javascirpt Modules
/*
(function () {
    var CreateCode = function () {
        return {
            Job1: function () {
                console.log("task1");
            },
            Job2: function () {
                console.log("task2");
            }
        }
    }
    var coder = CreateCode();
    coder.Job1();
    coder.Job2();
})();*/