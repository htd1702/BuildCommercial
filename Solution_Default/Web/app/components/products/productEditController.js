(function (app) {
    app.controller('productEditController', productEditController);

    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    productEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService"];

    function productEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        //set value model
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.editorOptions = {
            lang: 'en',
            height: '120px'
        }
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditProduct = EditProduct;
        //binding title seo by name
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSEOTitle($scope.product.Name);
        }
        //Method get id product
        function loadProductDetail() {
            apiService.get("/api/product/getid/" + $stateParams.id, null, function (result) {
                $scope.product = result.data;
            }, function (error) {
                notificationService.displayError("Lấy id thất bại!");
            });
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
        function EditProduct() {
            apiService.put("/api/product/update", $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " cập nhật thành công!");
                $state.go("products");
            }, function (error) {
                notificationService.displayError("Cập nhật thất bại!");
            });
        }
        //call method load list categories
        loadProductDetail();
        LoadCategory();
    }
})(angular.module('default.products'));