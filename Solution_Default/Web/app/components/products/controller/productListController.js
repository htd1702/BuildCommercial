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
        $scope.complateKeyWord = complateKeyWord;
        //autocomplete
        function complateKeyWord(string) {
            if (string != "") {
                apiService.autocomplete("/api/product/getname", "#txt_search");
            }
        }
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
                    pageSize: 15
                }
            }
            //call apiService url,params,success,error
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("No records were found!");
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
            $ngBootbox.confirm("Do you want delete?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/product/delete", config, function () {
                    notificationService.displaySuccess("Delete successfully!");
                    search();
                }, function () {
                    notificationService.displayError("Failed!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //method delete all product
        function deleteAllProduct() {
            var listId = [];
            $ngBootbox.confirm("Do you want delete?").then(function () {
                $(".chk_allProduct:checked").each(function () {
                    listId.push($(this).val());
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                };
                apiService.delete("/api/product/deletemulti", config, function (result) {
                    notificationService.displaySuccess('Delete successfully ' + result.data + ' records.');
                    search();
                }, function (error) {
                    notificationService.displayError("Failed!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //call getProduct
        getProduct();
    }
})(angular.module('default.products'));