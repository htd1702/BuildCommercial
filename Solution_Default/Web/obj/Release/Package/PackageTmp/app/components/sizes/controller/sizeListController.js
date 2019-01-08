(function (app) {
    app.controller("sizeListController", sizeListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    sizeListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function sizeListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //scope binding
        $scope.sizes = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create funtion
        $scope.getSizes = getSizes;
        $scope.search = search;
        $scope.deleteSize = deleteSize;
        $scope.complateKeyWord = complateKeyWord;
        $scope.deleteAllSizes = deleteAllSizes;
        $scope.showParentName = showParentName;
        //method get product cate
        function getSizes(page) {
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
            apiService.get('/api/size/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("No records were found!");
                $scope.sizes = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load size failed.');
            });
        }
        //method search
        function search() {
            getSizes();
        }
        //autocomplete
        function complateKeyWord(string) {
            if (string != "") {
                apiService.autocomplete("/api/size/getname", "#txt_search");
            }
        }
        //method delete
        function deleteSize(id) {
            $ngBootbox.confirm("Do you want delete?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/size/delete", config, function (response) {
                    if (response.data == 1) {
                        notificationService.displaySuccess("Delete Success!");
                        search();
                    }
                    else if (response.data == -1)
                        notificationService.displayError("Other is not delete!");
                }, function () {
                    notificationService.displayError("Delete Failed!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //method delete multi
        function deleteAllSizes() {
            var listId = [];
            $ngBootbox.confirm("Do you want delete?").then(function () {
                $(".chk_allSizes:checked").each(function () {
                    listId.push($(this).val());
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                };
                apiService.delete("/api/size/deletemulti", config, function (result) {
                    for (var i = 0; i <= result.data.length; i++) {
                        if (result.data[i] == 1) {
                            notificationService.displaySuccess('Row' + (i + 1) + ': Delete Success!</br>!');
                            search();
                        }
                        else if (result.data[i] == -1)
                            notificationService.displayError('Row' + (i + 1) + ': Other is not delete!</br>!');
                    }
                }, function (error) {
                    notificationService.displayError("Delete Failed!");
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
        getSizes();
    }
})(angular.module('default.sizes'));