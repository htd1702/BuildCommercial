(function (app) {
    app.controller("postCategoryEditController", postCategoryEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    postCategoryEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function postCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        //setting ckeditor
        $scope.editorOptions = {
            lang: 'en',
            height: '120px'
        };
        //set value model
        $scope.postCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        //create funtion
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditPostCategory = EditPostCategory;
        $scope.ChooseImage = ChooseImage;
        //binding title seo bye name
        function GetSeoTitle() {
            $scope.postCategory.Alias = commonService.getSEOTitle($scope.postCategory.Name);
        }
        //method load parentId
        function LoadParentCategory() {
            apiService.get("/api/postcategory/getallparents", null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }
        //load detail productcategory
        function loadPostCategoryDetail() {
            apiService.get("/api/postcategory/getid/" + $stateParams.id, null, function (result) {
                $scope.postCategory = result.data;
            }, function (error) {
                notificationService.displayError("Lấy id thất bại!");
            });
        }
        //Edit
        function EditPostCategory() {
            $scope.postCategory.CreatedBy = authData.authenticationData.userName;
            $scope.postCategory.UpdatedBy = $scope.postCategory.CreatedBy;
            $scope.postCategory.UpdatedDate = $scope.postCategory.CreatedDate;
            apiService.put("/api/postcategory/update", $scope.postCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " cập nhật thành công!");
                $state.go("post_categories");
            }, function (error) {
                notificationService.displayError("Cập nhật mới thất bại!");
            });
        }
        //funcion upload
        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filtUrl) {
                $scope.$apply(function () {
                    $scope.postCategory.Image = filtUrl;
                });
            };
            finder.popup();
        }
        //call method load parent
        loadPostCategoryDetail();
        LoadParentCategory();
    }
})(angular.module('default.post_categories'));