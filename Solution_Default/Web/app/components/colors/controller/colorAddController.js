(function (app) {
    app.controller("colorAddController", colorAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    colorAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService", "authData"];

    function colorAddController($scope, apiService, notificationService, $state, commonService, authData) {
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
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.AddColor = AddColor;
        //binding title seo by name
        function GetSeoTitle() {
            $scope.color.Alias = commonService.getSEOTitle($scope.color.Name);
        }
        //function add
        function AddColor() {
            if ($scope.color.ParentID == undefined)
                $scope.color.ParentID = 0;
            $scope.color.CreatedBy = authData.authenticationData.userName;
            $scope.color.UpdatedBy = $scope.color.CreatedBy;
            $scope.color.UpdatedDate = $scope.color.CreatedDate;
            apiService.post("/api/color/create", $scope.color, function (result) {
                notificationService.displaySuccess(result.data.Name + " thêm thành công!");
                $state.go("colors");
            }, function (error) {
                notificationService.displayError("Thêm mới thất bại!");
            });
        }
    }
})(angular.module('default.colors'));