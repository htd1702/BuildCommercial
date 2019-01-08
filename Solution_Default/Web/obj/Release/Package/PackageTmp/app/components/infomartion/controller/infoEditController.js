(function (app) {
    app.controller("infoEditController", infoEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    infoEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function infoEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        $scope.EditInfo = EditInfo;
        //load detail info
        function loadInfoDetail() {
            apiService.get("/api/info/getid/" + $stateParams.id, null, function (result) {
                $scope.info = result.data;
            }, function (error) {
                notificationService.displayError("Failed!");
            });
        }
        //Edit
        function EditInfo() {
            $scope.info.CreatedBy = authData.authenticationData.userName;
            $scope.info.UpdatedBy = $scope.info.CreatedBy;
            $scope.info.UpdatedDate = $scope.info.CreatedDate;
            apiService.put("/api/info/update", $scope.info, function (result) {
                notificationService.displaySuccess(result.data.Name + " Success!");
                $state.go("infos");
            }, function (error) {
                notificationService.displayError("Failed!");
            });
        }
        //call method load parent
        loadInfoDetail();
    }
})(angular.module('default.infos'));