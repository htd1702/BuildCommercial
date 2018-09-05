/// <reference path="../../../scripts/plugins/angular/angular.min.js" />

//(function (app) {
//    app.controller("loginController", loginController);
//    //apiService tầng service gọi get post put delete
//    //notificationService message thông báo
//    //$state : trỏ đến page
//    loginController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService"];

//    function loginController($scope, apiService, notificationService, $state, commonService) {
//        $scope.login = [];
//        $scope.loginSubmit = loginSubmit;
//        function loginSubmit() {
//            var loginWriteAccount = $scope.login.email;
//            var loginWritePass = $scope.login.pass;
//            var loginAccount = "hieu.n2395@gmail.com";
//            var loginPassword = "123";
//            $state.go("home");
//            //if (loginWriteAccount == loginAccount && loginWritePass == loginPassword) {
//            //    $state.go("home");
//            //    notificationService.displaySuccess("Đăng nhập thành công!");
//            //}
//            //else {
//            //    notificationService.displayError("Đăng nhập thất bại!");
//            //    $state.go("login");
//            //}
//        }
//    }
//})(angular.module('default'));

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