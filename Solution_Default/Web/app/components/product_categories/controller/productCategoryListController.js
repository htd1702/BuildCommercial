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
                    pageSize: 10
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
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/productcategory/delete", config, function (response) {
                    if (response.data == 1) {
                        notificationService.displaySuccess("Xóa thành công!");
                        search();
                    }
                    else if (response.data == -2)
                        notificationService.displayError("Danh mục đang được sử dụng làm parent của danh mục khác, vui lòng kiểm tra lại!");
                    else if (response.data == -1)
                        notificationService.displayError("Danh mục đang được sử dụng của sản phầm vui lòng xóa sản phẩm trước, vui lòng kiểm tra lại!");
                }, function () {
                    notificationService.displayError("Xóa không thành công!");
                });
            }, function () {
                console.log('Confirm dismissed!');
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
                };
                apiService.delete("/api/productcategory/deletemulti", config, function (result) {
                    for (var i = 0; i <= result.data.length; i++) {
                        if (result.data[i] == 1) {
                            notificationService.displaySuccess('Dòng:' + (i + 1) + ' xóa thành công!</br>!');
                            search();
                        }
                        else if (result.data[i] == -1) {
                            notificationService.displayError('Dòng:' + (i + 1) + ' danh mục đang được sử dụng của sản phầm vui lòng xóa sản phẩm trước, vui lòng kiểm tra lại!</br>!');
                        }
                        else if (result.data[i] == -2) {
                            notificationService.displayError('Dòng:' + (i + 1) + ' danh mục đang được sử dụng làm parent của danh mục khác, vui lòng kiểm tra lại!</br>!');
                        }
                    }
                }, function (error) {
                    notificationService.displayError("Xóa không thành công!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //function show parent name
        function showParentName(parentID, data) {
            if (parentID > 0) {
                for (var i = 0; i < data.length; i++) {
                    if (parentID == data[i].ID)
                        return data[i].Name;
                }
            }
            else
                return "";
        }
        //call getproduct
        getProductCategories();
    }
})(angular.module('default.product_categories'));