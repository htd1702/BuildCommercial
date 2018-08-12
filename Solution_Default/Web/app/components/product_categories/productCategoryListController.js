(function (app) {
    app.controller("productCategoryListController", productCategoryListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    productCategoryListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];
    
    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //scope binding
        $scope.productCategories = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create funtion
        $scope.getProductCategories = getProductCategories;
        $scope.search = search;
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.complateKeyWord = complateKeyWord;
        $scope.filterSearch = filterSearch;
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.deleteAllProductCategories = deleteAllProductCategories;
        //method delete
        function deleteProductCategory(id) {
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.delete("/api/productcategory/delete", config, function () {
                    notificationService.displaySuccess("Xóa thành công!");
                    search();
                }, function () {
                    notificationService.displayError("Xóa không thành công!");
                });
            });
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
        //method search
        function search() {
            getProductCategories();
        }
        //autocomplete
        function complateKeyWord(string) {
            if (string != "") {
                $scope.hideSeach = false;
                var output = [];
                angular.forEach($scope.productCategories, function (value) {
                    if (value.Name.toLowerCase().indexOf(string.toLowerCase()) >= 0) {
                        output.push(value.Name);
                    }
                });
                $scope.filterName = output;
            }
            else
                $scope.hideSeach = true;
        }
        //search keyword in model
        function filterSearch(string) {
            $scope.keyword = string;
            $scope.hideSeach = true;
        }
        //method delete
        function deleteProductCategory(id) {
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.delete("/api/productcategory/delete", config, function () {
                    notificationService.displaySuccess("Xóa thành công!");
                    search();
                }, function () {
                    notificationService.displayError("Xóa không thành công!");
                });
            });
        }
        //method delete multi
        function deleteAllProductCategories() {
            var listId = [];
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                $(".chk_allProductCategories:checked").each(function () {
                    listId.push($(this).val());
                });

                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                }
                console.log(config);

                apiService.delete("/api/productcategory/deletemulti", config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError("Xóa không thành công!");
                });
            });
        }
        //call getproduct
        getProductCategories();
    }
})(angular.module('default.product_categories'));