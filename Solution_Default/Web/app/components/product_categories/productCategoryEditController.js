(function (app) {
    app.controller("productCategoryEditController", productCategoryEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    productCategoryEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService"];

    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        //set value model
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        //create funtion
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditProductCategory = EditProductCategory;
        //binding title seo bye name
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
        //load detail productcategory
        function loadProductCategoryDetail() {
            apiService.get("/api/productcategory/getid/" + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.displayError("Lấy id thất bại!");
            });
        }
        //Edit
        function EditProductCategory() {
            apiService.put("/api/productcategory/update", $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " cập nhật thành công!");
                $state.go("product_categories");
            }, function (error) {
                notificationService.displayError("Cập nhật mới thất bại!");
            });
        }
        //call method load parent
        loadProductCategoryDetail();
        LoadParentCategory();
    }
})(angular.module('default.product_categories'));