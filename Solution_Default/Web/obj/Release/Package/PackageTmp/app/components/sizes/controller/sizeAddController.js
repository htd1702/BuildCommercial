(function (app) {
    app.controller("sizeAddController", sizeAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    sizeAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService", "authData"];

    function sizeAddController($scope, apiService, notificationService, $state, commonService, authData) {
        //set value model
        $scope.size = {
            CreatedDate: new Date(),
            Status: true
        };
        //setting ckeditor
        $scope.editorOptions = {
            lang: 'en',
            height: '120px'
        };
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.AddSize = AddSize;
        //binding title seo by name
        function GetSeoTitle() {
            $scope.size.Alias = commonService.getSEOTitle($scope.size.Name);
        }
        //function add
        function AddSize() {
            if ($scope.size.ParentID == undefined)
                $scope.size.ParentID = 0;
            $scope.size.CreatedBy = authData.authenticationData.userName;
            $scope.size.UpdatedBy = $scope.size.CreatedBy;
            $scope.size.UpdatedDate = $scope.size.CreatedDate;
            apiService.post("/api/size/create", $scope.size, function (result) {
                notificationService.displaySuccess(result.data.Name + " success!");
                $state.go("sizes");
            }, function (error) {
                notificationService.displayError("Add failed!");
            });
        }
    }
})(angular.module('default.sizes'));