(function (app) {
    app.controller("sizeEditController", sizeEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    sizeEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function sizeEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
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
        //create funtion
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditSize = EditSize;
        //binding title seo bye name
        function GetSeoTitle() {
            $scope.size.Alias = commonService.getSEOTitle($scope.size.Name);
        }
        //load detail size
        function loadSizeDetail() {
            apiService.get("/api/size/getid/" + $stateParams.id, null, function (result) {
                $scope.size = result.data;
            }, function (error) {
                notificationService.displayError("Lấy id thất bại!");
            });
        }
        //Edit
        function EditSize() {
            $scope.size.CreatedBy = authData.authenticationData.userName;
            $scope.size.UpdatedBy = $scope.size.CreatedBy;
            $scope.size.UpdatedDate = $scope.size.CreatedDate;
            apiService.put("/api/size/update", $scope.size, function (result) {
                notificationService.displaySuccess(result.data.Name + " cập nhật thành công!");
                $state.go("sizes");
            }, function (error) {
                notificationService.displayError("Cập nhật mới thất bại!");
            });
        }
        //call method load parent
        loadSizeDetail();
    }
})(angular.module('default.sizes'));