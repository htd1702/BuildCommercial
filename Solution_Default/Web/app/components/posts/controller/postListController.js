(function (app) {
    app.controller("postListController", postListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    postListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function postListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //scope binding
        $scope.posts = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create funtion
        $scope.getPosts = getPosts;
        $scope.search = search;
        $scope.deletePost = deletePost;
        $scope.complateKeyWord = complateKeyWord;
        $scope.deleteAllPost = deleteAllPost;
        //method get post cate
        function getPosts(page) {
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
            apiService.get('/api/post/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("Không có bản ghi nào được tìm thấy!");
                $scope.posts = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load post failed.');
            });
        }
        //method search
        function search() {
            getPosts();
        }
        //autocomplete
        function complateKeyWord(string) {
            if (string != "") {
                apiService.autocomplete("/api/post/getname", "#txt_search");
            }
        }
        //method delete
        function deletePost(id) {
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/post/delete", config, function () {
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
        function deleteAllPost() {
            var listId = [];
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                $(".chk_allPosts:checked").each(function () {
                    listId.push($(this).val());
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                }
                apiService.delete("/api/post/deletemulti", config, function (result) {
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
        getPosts();
    }
})(angular.module('default.posts'));