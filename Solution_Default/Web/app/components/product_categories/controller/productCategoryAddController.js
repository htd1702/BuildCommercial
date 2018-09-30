(function (app) {
    app.controller("productCategoryAddController", productCategoryAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    productCategoryAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService", "authData"];

    function productCategoryAddController($scope, apiService, notificationService, $state, commonService, authData) {
        //load option parentID
        $scope.parentCategories = [];
        //set value model
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.AddProductCategory = AddProductCategory;
        //binding title seo by name
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSEOTitle($scope.productCategory.Name);
        }
        //method load parentId
        function LoadParentCategory() {
            apiService.get("/api/productcategory/getallparents", null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }
        //function add
        function AddProductCategory() {
            if ($scope.productCategory.ParentID == undefined)
                $scope.productCategory.ParentID = 0;
            $scope.productCategory.CreatedBy = authData.authenticationData.userName;
            $scope.productCategory.UpdatedBy = $scope.productCategory.CreatedBy;
            $scope.productCategory.UpdatedDate = $scope.productCategory.CreatedDate;
            apiService.post("/api/productcategory/create", $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " thêm thành công!");
                $state.go("product_categories");
            }, function (error) {
                notificationService.displayError("Thêm mới thất bại!");
            });
        }
        //call method load parent
        LoadParentCategory();
    }
})(angular.module('default.product_categories'));