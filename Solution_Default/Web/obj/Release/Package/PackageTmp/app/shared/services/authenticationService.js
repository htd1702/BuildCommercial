/// <reference path="../../../scripts/plugins/angular/angular.min.js" />

(function (app) {
    'use strict';

    //partent $q => promise đảm bảo thực thi trc sau
    //$window create session
    app.service('authenticationService', authenticationService);

    authenticationService.$inject = ['$http', '$q', '$window'];

    function authenticationService($http, $q, $window) {
        var tokenInfo;
        //set token
        function setTokenInfo(data) {
            tokenInfo = data;
            $window.sessionStorage["TokenInfo"] = JSON.stringify(tokenInfo);
        }
        //get token
        function getTokenInfo() {
            return tokenInfo;
        }
        //remove token
        function removeToken() {
            tokenInfo = null;
            $window.sessionStorage["TokenInfo"] = null;
        }
        //set token and pase json
        function init() {
            if ($window.sessionStorage["TokenInfo"])
                tokenInfo = JSON.parse($window.sessionStorage["TokenInfo"]);
        }
        //set header api
        function setHeader() {
            delete $http.defaults.headers.common['X-Requested-With'];
            if ((tokenInfo != undefined) && (tokenInfo.accessToken != undefined) && (tokenInfo.accessToken != null) && (tokenInfo.accessToken != "")) {
                $http.defaults.headers.common['Authorization'] = 'Bearer ' + tokenInfo.accessToken;
                $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
            }
        }
        //validatable
        function validateRequest() {
            var url = '/api/home/TestMethod';
            var deferred = $q.defer();
            $http.get(url).then(function (result) {
                deferred.resolve(null);
            }, function (error) {
                deferred.reject(error);
            });
            return deferred.promise;
        }
        //return function
        return {
            setTokenInfo: setTokenInfo,
            getTokenInfo: getTokenInfo,
            removeToken: removeToken,
            init: init,
            setHeader: setHeader,
            validateRequest: validateRequest
        };
    }
})(angular.module('default.common'));