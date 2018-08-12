(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ["$scope", "apiService"];

    function productListController($scope, apiService) {
        $scope.products = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create function
        $scope.getProduct = getProduct;
        $scope.search = search;
        //method search
        function search() {
            getProduct();
        }
        //method get list product
        function getProduct(page) {
            page = page || 0;

            var config = {
                //params truyen vao api
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            //call apiService url,params,success,error
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("Không có bản ghi nào được tìm thấy!");
                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }
        //call getProduct
        getProduct();
    }
})(angular.module('default.products'));