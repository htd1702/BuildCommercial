(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function productListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create function
        $scope.getProduct = getProduct;
        $scope.search = search;
        $scope.deleteProduct = deleteProduct;
        $scope.deleteAllProduct = deleteAllProduct;
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
        //method delete product
        function deleteProduct(id) {
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.delete("/api/product/delete", config, function () {
                    notificationService.displaySuccess("Xóa thành công!");
                    search();
                }, function () {
                    notificationService.displayError("Xóa không thành công!");
                });
            });
        }
        //method delete all product
        function deleteAllProduct() {
            var listId = [];
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                $(".chk_allProduct:checked").each(function () {
                    listId.push($(this).val());
                });

                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                }
                apiService.delete("/api/product/deletemulti", config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError("Xóa không thành công!");
                });
            });
        }
        //call getProduct
        getProduct();
    }
})(angular.module('default.products'));