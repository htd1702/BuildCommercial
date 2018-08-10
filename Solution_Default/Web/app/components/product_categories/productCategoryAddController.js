(function (app) {
    app.controller("productCategoryAddController", productCategoryAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    productCategoryAddController.$inject = ["$scope", "apiService", "notificationService", "$state"];

    function productCategoryAddController($scope, apiService, notificationService, $state) {
        //set value model
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }

        //load option parentID
        $scope.parentCategories = [];

        //method load parentId
        function LoadParentCategory() {
            apiService.get("/api/productcategory/getallparents", null, function (result) {
                $scope.parentCategories = result.data;
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }

        //add
        $scope.AddProductCategory = function () {
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