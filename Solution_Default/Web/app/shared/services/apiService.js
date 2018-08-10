/// <reference path="../../../scripts/angular.js" />

(function (app) {
    app.factory("apiService", apiService);

    apiService.$inject = ["$http", "notificationService"];

    function apiService($http, notificationService) {
        //method get
        function get(url, params, success, failed) {
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
            $http.post(url, data).then(function (result) {
                success(result);
            }, function (error) {
                failed(error);
            });
        }
        //method put
        function put(url, data, success, failed) {
            $http.put(url, data).then(function (result) {
                success(result);
            }, function (error) {
                failed(error);
            });
        }
        return {
            get: get,
            post: post,
            put: put
        }
    }

})(angular.module("default.common"));