(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ["$scope", "apiService"];

    function productListController($scope, apiService) {
        $scope.products = [];

        $scope.getProduct = getProduct;

        function getProduct() {
            apiService.get("/api/product/getall", null, function (result) {
                $scope.products = result.data;
            }, function (error) {
                console.log('Load productcategory failed.');
            });
        }

        getProduct();
    }
})(angular.module('default.products'));