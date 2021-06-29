angular.module('sakaryabel.services', [])
        .service('Users', ['$http', 'Auth', function ($http, Auth) {
            return {
                getAuthToken: function (username, password) {

                    username = encodeURIComponent(username);
                    password = encodeURIComponent(password);

                    var func = $http.get('http://{apihost}/GetAuthToken?username=' + username + '&password=' + password, { cache: false }).then(function (response) {
                        return response.data;
                    });
                    return func;
                },
                changeProfileImage: function (userId, fileId) {

                    var func = $http.get('http://{apihost}/ChangeProfilePicture?userId=' + userId + "&fileId=" + fileId, { cache: false }).then(function (response) {
                        return response.data;
                    });
                    return func;
                },
                changePassword: function (model) {
                    // model.OldPassword = encodeURIComponent(  model.oldPassword);
                    // model.newPassword = encodeURIComponent(model.newPassword );

                    var func = $http.post('http://{apihost}/ChangePassword', model, { cache: false }).then(function (response) {
                        console.log(response);
                        return response;
                    });
                    return func;
                },
                getProblemList: function (data) {
                    id = data.hasOwnProperty('id') ? data.id : ""
                    userList = data.hasOwnProperty('userList') ? encodeURIComponent(data.userList) : "";
                    skip = data.hasOwnProperty('skip') ? data.skip : 0;
                    filter = data.hasOwnProperty('filter') ? encodeURIComponent(data.filter) : "";
                    lim = data.hasOwnProperty('lim') ? data.lim : 5;
                    var func = $http.get('http://{apihost}/GetProblemList?id=' + id + '&userList=' + userList + '&filtre=' + filter + '&skip=' + skip + '&top=' + lim, { cache: false }).then(function (response) {
                        return response.data;
                    });
                    return func;
                },
                getUserList: function (data) {
                    skip = data.hasOwnProperty('skip') ? data.skip : 0;
                    filter = data.hasOwnProperty('filter') ? encodeURIComponent(data.filter) : "";
                    lim = data.hasOwnProperty('lim') ? data.lim : 10;
                    var func = $http.get('http://{apihost}/GetUserList?filtre=' + filter + '&skip=' + skip + '&top=' + lim, { cache: false }).then(function (response) {
                        return response;
                    });
                    return func;
                },
                getProblem: function (id) {
                    var func = $http.get('http://{apihost}/GetEditProblem?id=' + id, { cache: false }).then(function (response) {
                        return response.data;
                    });
                    return func;
                },
                newProblem: function (problem) {

                    var func = $http.post('http://{apihost}/NewProblem', problem, { cache: false }).then(function (response) {
                        return response.data;
                    });
                    return func;
                },
                newUser: function (data) {

                    var func = $http.post('http://{apihost}/Register', data, { cache: false }).then(function (response) {
                        return response.data;
                    });
                    return func;
                }
            }
        }])
;
