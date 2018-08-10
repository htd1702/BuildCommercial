(function (app) {
    app.controller("productCategoryListController", productCategoryListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    productCategoryListController.$inject = ["$scope", "apiService", "notificationService"];

    function productCategoryListController($scope, apiService, notificationService) {
        //scope binding
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.getProductCategories = getProductCategories;
        //binding
        $scope.keyword = "";
        //method search
        $scope.search = function () {
            getProductCategories();
        }
        //method get product cate
        function getProductCategories(page) {
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
            apiService.get('/api/productcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("Không có bản ghi nào được tìm thấy!");
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }

        $scope.getProductCategories();
    }
})(angular.module('default.product_categories'));