/// <reference path="../../../scripts/plugins/angular/angular.min.js" />

(function (app) {
    app.controller("loginController", loginController);

    loginController.$inject = ["$scope", "loginService", "$state", "notificationService"];

    function loginController($scope, loginService, $state, notificationService) {
        $scope.loginData = {
            userName: "",
            password: ""
        };
        //create function loginsubmit
        $scope.loginSubmit = loginSubmit;
        //function login
        function loginSubmit() {
            var user = $scope.loginData.userName;
            var pass = $scope.loginData.password;
            loginService.login(user, pass).then(function (response) {
                if (response != null && response.data.error != undefined)
                    notificationService.displayError("Đăng nhập không đúng.");
                else
                    $state.go('home');
            });
        }
    }
})(angular.module('default'));