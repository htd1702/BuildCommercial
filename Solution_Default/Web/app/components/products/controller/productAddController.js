(function (app) {
    app.controller('productAddController', productAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    productAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService"];

    function productAddController($scope, apiService, notificationService, $state, commonService) {
        $scope.categories = [];
        //set value model
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.AddProduct = AddProduct;
        //binding title seo by name
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSEOTitle($scope.product.Name);
        }
        //method load parentId
        function LoadCategory() {
            apiService.get("/api/productcategory/getallparents", null, function (result) {
                $scope.categories = result.data;
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }
        //function add
        function AddProduct() {
            apiService.post("/api/product/create", $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " thêm thành công!");
                $state.go("products");
            }, function (error) {
                notificationService.displayError("Thêm mới thất bại!");
            });
        }
        //call method load list categories
        LoadCategory();
    }
})(angular.module('default.products'));