(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ['$state', 'authData', 'loginService', '$scope', 'authenticationService'];

    function rootController($state, authData, loginService, $scope, authenticationService) {
        //create function logOut
        $scope.logOut = logOut;
        //function Logout
        function logOut() {
            loginService.logOut();
            $state.go('login');
        }
        $scope.authentication = authData.authenticationData;
        authenticationService.validateRequest();
    }
})(angular.module('default'));