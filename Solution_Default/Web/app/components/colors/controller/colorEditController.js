(function (app) {
    app.controller("colorEditController", colorEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    colorEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function colorEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        //set value model
        $scope.color = {
            CreatedDate: new Date(),
            Status: true
        };
        //setting ckeditor
        $scope.editorOptions = {
            lang: 'en',
            height: '120px'
        };
        //create funtion
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditColor = EditColor;
        //binding title seo bye name
        function GetSeoTitle() {
            $scope.color.Alias = commonService.getSEOTitle($scope.color.Name);
        }
        //load detail color
        function loadColorDetail() {
            apiService.get("/api/color/getid/" + $stateParams.id, null, function (result) {
                $scope.color = result.data;
            }, function (error) {
                notificationService.displayError("Failed!");
            });
        }
        //Edit
        function EditColor() {
            $scope.color.CreatedBy = authData.authenticationData.userName;
            $scope.color.UpdatedBy = $scope.color.CreatedBy;
            $scope.color.UpdatedDate = $scope.color.CreatedDate;
            apiService.put("/api/color/update", $scope.color, function (result) {
                notificationService.displaySuccess(result.data.Name + " Success!");
                $state.go("colors");
            }, function (error) {
                notificationService.displayError("Failed!");
            });
        }
        //call method load parent
        loadColorDetail();
    }
})(angular.module('default.colors'));