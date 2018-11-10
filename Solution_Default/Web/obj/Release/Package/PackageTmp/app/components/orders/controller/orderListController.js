(function (app) {
    app.controller("orderListController", orderListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    orderListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function orderListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //scope binding
        $scope.orders = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create funtion
        $scope.getOrders = getOrders;
        $scope.deleteOrder = deleteOrder;
        $scope.deleteAllOrders = deleteAllOrders;
        //method get product cate
        function getOrders(page) {
            page = page || 0;

            var config = {
                //params truyen vao api
                params: {
                    page: page,
                    pageSize: 20
                }
            }
            //call apiService url,params,success,error
            apiService.get('/api/order/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("Không có bản ghi nào được tìm thấy!");
                $scope.orders = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }
        //method delete
        function deleteOrder(id) {
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/productcategory/delete", config, function () {
                    notificationService.displaySuccess("Xóa thành công!");
                }, function () {
                    notificationService.displayError("Xóa không thành công!");
                });
            }, function () {
                console.log('Load productcategory failed.');
            });
        }
        //method delete multi
        function deleteAllOrders() {
            var listId = [];
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                $(".chk_allOrders:checked").each(function () {
                    listId.push($(this).val());
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                };
                apiService.delete("/api/productcategory/deletemulti", config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                }, function (error) {
                    notificationService.displayError("Xóa không thành công!");
                });
            }, function () {
                console.log('Load productcategory failed.');
            });
        }
        //call getproduct
        getOrders();
    }
})(angular.module('default.orders'));