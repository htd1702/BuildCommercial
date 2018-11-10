/// <reference path="../../../scripts/plugins/angular/angular.min.js" />

(function (app) {
    app.factory("apiService", apiService);

    apiService.$inject = ["$http", "notificationService", "authenticationService"];

    function apiService($http, notificationService, authenticationService) {
        //method get
        function get(url, params, success, failed) {
            authenticationService.setHeader();
            $http.get(url, params).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status == "401") {
                    notificationService.displayError("Authenticate is required");
                }
                else if (failed != null) {
                    failed(error);
                }
            });
        }
        //method post
        function post(url, data, success, failed) {
            authenticationService.setHeader();
            $http.post(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status == "401") {
                    notificationService.displayError("Authenticate is required");
                }
                else if (failed != null) {
                    failed(error);
                }
            });
        }
        //method put
        function put(url, data, success, failed) {
            authenticationService.setHeader();
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status == "401") {
                    notificationService.displayError("Authenticate is required");
                }
                else if (failed != null) {
                    failed(error);
                }
            });
        }
        //method delete
        function del(url, data, success, failed) {
            authenticationService.setHeader();
            $http.delete(url, data).then(function (result) {
                success(result);
            }, function (error) {
                if (error.status == "401") {
                    notificationService.displayError("Authenticate is required");
                }
                else if (failed != null) {
                    failed(error);
                }
            });
        }
        //return function
        return {
            get: get,
            post: post,
            put: put,
            delete: del
        };
    }
})(angular.module("default.common"));