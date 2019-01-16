﻿(function (app) {
    app.controller("orderListController", orderListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    orderListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter", "$http"];

    function orderListController($scope, apiService, notificationService, $ngBootbox, $filter, $http) {
        //scope binding
        $scope.orders = [];
        $scope.orderDetails = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create funtion
        $scope.getOrders = getOrders;
        $scope.search = search;
        $scope.deleteOrder = deleteOrder;
        $scope.deleteAllOrders = deleteAllOrders;
        $scope.loadListDetailsOrder = loadListDetailsOrder;
        $scope.complateKeyWord = complateKeyWord;
        //method search
        function search() {
            getOrders();
        }
        //autocomplete
        function complateKeyWord(string) {
            if (string != "") {
                apiService.autocomplete("/api/order/getname", "#txt_search");
            }
        }
        //method get product cate
        function getOrders(page) {
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
            apiService.get('/api/order/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("No records were found!");
                $scope.orders = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load banner failed.');
            });
        }
        //method delete
        function deleteOrder(id) {
            $ngBootbox.confirm("Do you want delete?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/order/delete", config, function () {
                    notificationService.displaySuccess("Delete success!");
                    search();
                }, function () {
                    notificationService.displayError("Delete is not success!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //method delete multi
        function deleteAllOrders() {
            var listId = [];
            $ngBootbox.confirm("Do you want delete?").then(function () {
                $(".chk_allOrders:checked").each(function () {
                    listId.push($(this).val());
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                };
                apiService.delete("/api/order/deletemulti", config, function (result) {
                    notificationService.displaySuccess('Delete ' + result.data + ' record.');
                    search();
                }, function (error) {
                    notificationService.displayError("Delete is not success!");
                });
            }, function () {
                console.log('Confirm dismissed!');
            });
        }
        //function loadListDetailsOrder
        function loadListDetailsOrder(id) {
            return $http({
                method: 'POST',
                url: '/api/order/getorderdetail',
                async: false,
                params: { id: id }
            }).then(function (response) {
                $scope.orderDetails = response.data;
            });
        }
        //call getproduct
        getOrders();
    }
})(angular.module('default.orders'));