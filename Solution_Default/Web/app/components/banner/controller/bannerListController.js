(function (app) {
    app.controller("bannerListController", bannerListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    bannerListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function bannerListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //scope binding
        $scope.banners = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create funtion
        $scope.getBanners = getBanners;
        $scope.search = search;
        $scope.deleteBanner = deleteBanner;
        $scope.complateKeyWord = complateKeyWord;
        $scope.deleteAllBanners = deleteAllBanners;
        $scope.showParentName = showParentName;
        //method get product cate
        function getBanners(page) {
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
            apiService.get('/api/banner/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("No records were found!");
                $scope.banners = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load banner failed.');
            });
        }
        //method search
        function search() {
            getBanners();
        }
        //autocomplete
        function complateKeyWord(string) {
            if (string != "") {
                apiService.autocomplete("/api/banner/getname", "#txt_search");
            }
        }
        //method delete
        function deleteBanner(id) {
            $ngBootbox.confirm("Do y want delete?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/banner/delete", config, function () {
                    notificationService.displaySuccess("Success!");
                    search();
                }, function () {
                    notificationService.displayError("Failed!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //method delete multi
        function deleteAllBanners() {
            var listId = [];
            $ngBootbox.confirm("Do y want delete?").then(function () {
                $(".chk_allBanners:checked").each(function () {
                    listId.push($(this).val());
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                };
                apiService.delete("/api/banner/deletemulti", config, function (result) {
                    notificationService.displaySuccess('Success ' + result.data + ' records.');
                    search();
                }, function (error) {
                    notificationService.displayError("Failed!");
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
        getBanners();
    }
})(angular.module('default.banners'));