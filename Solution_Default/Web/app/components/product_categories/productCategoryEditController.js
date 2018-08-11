(function (app) {
    app.controller("productCategoryEditController", productCategoryEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    productCategoryEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService"];

    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        //set value model
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        $scope.GetSeoTitle = GetSeoTitle;

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

        function loadProductCategoryDetail() {
            apiService.get("/api/productcategory/getid/" + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.displayError("Lấy id thất bại!");
            });
        }

        //Edit
        $scope.EditProductCategory = function () {
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