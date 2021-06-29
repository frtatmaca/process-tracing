function SubController($scope) {
    $scope.SubMessage='Submessage directApp Controller';
}

var directApp = angular.module("directApp", []);
directApp.controller('SubController', SubController);

directApp.filter("chekcMark", function () {
    return function (input) {
        return input ? '\u2713' : '\u2718'
    }
});

directApp.directive("userDetail", function () {
    return ({
        retrict: 'E',
        template: '<div><strong>Image:</strong><img ng-src="{{User.avatar_url}}" width="200" title="{{User.Name}}"/> <textarea rows="20" cols="100">{{User | json}}</textarea></div>'
    });
});

directApp.directive("searchUser", function () {
    return ({
        restrict: 'A',
        template: 'Search User: <input type="text" placeholder="Search User" autofocus ng-model="searchKey"/> <input type="button" value="Search" ng-click="Search(searchKey)"/>'
    })
});