(function (app) {
    app.controller("postCategoryListController", postCategoryListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    postCategoryListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function postCategoryListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //scope binding
        $scope.postCategories = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create funtion
        $scope.getPostCategories = getPostCategories;
        $scope.search = search;
        $scope.deletePostCategory = deletePostCategory;
        $scope.complateKeyWord = complateKeyWord;
        $scope.deleteAllPostCategories = deleteAllPostCategories;
        //method get post cate
        function getPostCategories(page) {
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
            apiService.get('/api/postcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("Không có bản ghi nào được tìm thấy!");
                $scope.postCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load postcategory failed.');
            });
        }
        //method search
        function search() {
            getPostCategories();
        }
        //autocomplete
        function complateKeyWord(string) {
            if (string != "") {
                apiService.autocomplete("/api/postcategory/getname", "#txt_search");
            }
        }
        //method delete
        function deletePostCategory(id) {
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/postcategory/delete", config, function () {
                    notificationService.displaySuccess("Xóa thành công!");
                    search();
                }, function () {
                    notificationService.displayError("Xóa không thành công!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //method delete multi
        function deleteAllPostCategories() {
            var listId = [];
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                $(".chk_allPostCategories:checked").each(function () {
                    listId.push($(this).val());
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                }
                apiService.delete("/api/postcategory/deletemulti", config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError("Xóa không thành công!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //call function
        getPostCategories();
    }
})(angular.module('default.post_categories'));