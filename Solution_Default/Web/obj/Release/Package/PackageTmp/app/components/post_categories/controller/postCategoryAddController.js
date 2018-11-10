(function (app) {
    app.controller("postCategoryAddController", postCategoryAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    postCategoryAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService", "authData"];

    function postCategoryAddController($scope, apiService, notificationService, $state, commonService, authData) {
        //load option parentID
        $scope.parentCategories = [];
        //set value model
        $scope.postCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.AddPostCategory = AddPostCategory;
        $scope.ChooseImage = ChooseImage;
        //binding title seo by name
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
        //function add
        function AddPostCategory() {
            if ($scope.postCategory.ParentID == undefined)
                $scope.postCategory.ParentID = 0;
            $scope.postCategory.CreatedBy = authData.authenticationData.userName;
            $scope.postCategory.UpdatedBy = $scope.postCategory.CreatedBy;
            $scope.postCategory.UpdatedDate = $scope.postCategory.CreatedDate;
            apiService.post("/api/postcategory/create", $scope.postCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " thêm thành công!");
                $state.go("post_categories");
            }, function (error) {
                notificationService.displayError("Thêm mới thất bại!");
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
        LoadParentCategory();
    }
})(angular.module('default.post_categories'));