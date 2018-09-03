(function (app) {
    app.controller('rootController', rootController);

    rootController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService"];

    function rootController($scope, apiService, notificationService, $state, commonService) {
        $scope.logout = logout;
        function logout() {
            $state.go("login");
        }
    }
})(angular.module('default'));