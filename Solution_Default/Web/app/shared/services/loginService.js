/// <reference path="../../../scripts/plugins/angular/angular.min.js" />

(function (app) {
    'use strict';

    app.service('loginService', loginService);

    loginService.$inject = ['$http', '$q', 'authenticationService', 'authData'];

    function loginService($http, $q, authenticationService, authData) {
        var userInfo;
        var deferred;
        //function login get username and password
        function login(userName, password) {
            deferred = $q.defer();
            $http({
                url: '/oauth/token',
                method: "post",
                async: true,
                data: $.param({ username: userName, password: password, "grant_type": "password" }),
                headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            }).then(function (response) {
                userInfo = {
                    accessToken: response.data.access_token,
                    userName: userName
                };
                authenticationService.setTokenInfo(userInfo);
                authData.authenticationData.IsAuthenticated = true;
                authData.authenticationData.userName = userName;
                deferred.resolve(null);
            }).catch(function (err, status) {
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
                deferred.resolve(err);
            });
            return deferred.promise;
        }
        //function logout and remove token
        function logOut() {
            authenticationService.removeToken();
            authData.authenticationData.IsAuthenticated = false;
            authData.authenticationData.userName = "";
        }
        //return function
        return {
            login: login,
            logOut: logOut
        };
    }
})(angular.module('default.common'));