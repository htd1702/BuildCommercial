(function (app) {
    app.controller("colorListController", colorListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    colorListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function colorListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //scope binding
        $scope.colors = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create funtion
        $scope.getColors = getColors;
        $scope.search = search;
        $scope.deleteColor = deleteColor;
        $scope.complateKeyWord = complateKeyWord;
        $scope.deleteAllColors = deleteAllColors;
        $scope.showParentName = showParentName;
        //method get product cate
        function getColors(page) {
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
            apiService.get('/api/color/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("No records were found!");
                $scope.colors = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load color failed.');
            });
        }
        //method search
        function search() {
            getColors();
        }
        //autocomplete
        function complateKeyWord(string) {
            if (string != "") {
                apiService.autocomplete("/api/color/getname", "#txt_search");
            }
        }
        //method delete
        function deleteColor(id) {
            $ngBootbox.confirm("Do y want delete?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/color/delete", config, function () {
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
        function deleteAllColors() {
            var listId = [];
            $ngBootbox.confirm("Do you want delete?").then(function () {
                $(".chk_allColors:checked").each(function () {
                    listId.push($(this).val());
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                };
                apiService.delete("/api/color/deletemulti", config, function (result) {
                    notificationService.displaySuccess('Success ' + result.data + ' record.');
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
        getColors();
    }
})(angular.module('default.colors'));