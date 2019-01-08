(function (app) {
    app.controller("productCategoryListController", productCategoryListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    productCategoryListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function productCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //scope binding
        $scope.parentCategory = [];
        $scope.productCategories = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create funtion
        $scope.getProductCategories = getProductCategories;
        $scope.search = search;
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.complateKeyWord = complateKeyWord;
        $scope.deleteAllProductCategories = deleteAllProductCategories;
        $scope.showParentName = showParentName;
        //method get product cate
        function getProductCategories(page) {
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
                apiService.autocomplete("/api/productcategory/getname", "#txt_search");
            }
        }
        //method delete
        function deleteProductCategory(id) {
            $ngBootbox.confirm("Do you want delete?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/productcategory/delete", config, function (response) {
                    if (response.data == 1) {
                        notificationService.displaySuccess("Delete Success!");
                        search();
                    }
                    else if (response.data == -3)
                        notificationService.displayError("Parent is not delete!");
                    else if (response.data == -2)
                        notificationService.displayError("Please delete the parent category before!");
                    else if (response.data == -1)
                        notificationService.displayError("Please delete the product before delete category!");
                }, function () {
                    notificationService.displayError("Delete Failed!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //method delete multi
        function deleteAllProductCategories() {
            var listId = [];
            $ngBootbox.confirm("Do you want delete?").then(function () {
                $(".chk_allProductCategories:checked").each(function () {
                    listId.push($(this).val());
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                };
                apiService.delete("/api/productcategory/deletemulti", config, function (result) {
                    for (var i = 0; i <= result.data.length; i++) {
                        if (result.data[i] == 1) {
                            notificationService.displaySuccess('Row' + (i + 1) + ': Delete Success!</br>!');
                            search();
                        }
                        else if (result.data[i] == -3)
                            notificationService.displayError('Row' + (i + 1) + ': Parent is not delete!');
                        else if (result.data[i] == -2)
                            notificationService.displayError('Row' + (i + 1) + ': Please delete the parent category before!</br>!');
                        else if (result.data[i] == -1)
                            notificationService.displayError('Row' + (i + 1) + ': Please delete the product before delete category!</br>!');
                    }
                }, function (error) {
                    notificationService.displayError("Delete Failed!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //
        function loadCategoryAll() {
            //call apiService url,params,success,error
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategory = result.data;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }
        //function show parent name
        function showParentName(parentID) {
            if (parentID > 0) {
                for (var i = 0; i < $scope.parentCategory.length; i++) {
                    if (parentID == $scope.parentCategory[i].ID)
                        return $scope.parentCategory[i].Name;
                }
            }
            else
                return "";
        }
        //call getproduct
        loadCategoryAll();
        getProductCategories();
    }
})(angular.module('default.product_categories'));