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
        //call function
        getPostCategories();
    }
})(angular.module('default.post_categories'));