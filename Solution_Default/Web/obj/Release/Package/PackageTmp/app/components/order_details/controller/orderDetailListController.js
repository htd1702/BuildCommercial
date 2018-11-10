(function (app) {
    app.controller("orderDetailListController", orderDetailListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    orderDetailListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function orderDetailListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //scope binding
        $scope.orderDetails = [];
        $scope.keyword = "";
        $scope.page = 0;
        $scope.pagesCount = 0;
        //create funtion
        $scope.getOrderDetails = getOrderDetails;
        $scope.search = search;
        $scope.deleteOrderDetail = deleteOrderDetail;
        $scope.complateKeyWord = complateKeyWord;
        $scope.filterSearch = filterSearch;
        $scope.deleteAllOrderDetails = deleteAllOrderDetails;
        //method get product cate
        function getOrderDetails(page) {
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
            apiService.get('/api/orderDetails/getall', config, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("Không có bản ghi nào được tìm thấy!");
                $scope.orderDetails = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load productcategory failed.');
            });
        }
        //method search
        function search() {
            getOrderDetails();
        }
        //autocomplete
        function complateKeyWord(string) {
            if (string != "") {
                $scope.hideSeach = false;
                var output = [];
                angular.forEach($scope.orderDetails, function (value) {
                    if (value.Name.toLowerCase().indexOf(string.toLowerCase()) >= 0) {
                        output.push(value.Name);
                    }
                });
                $scope.filterName = output;
            }
            else
                $scope.hideSeach = true;
        }
        //search keyword in model
        function filterSearch(string) {
            $scope.keyword = string;
            $scope.hideSeach = true;
        }
        //method delete
        function deleteOrderDetail(id) {
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                var config = {
                    params: {
                        id: id
                    }
                };
                apiService.delete("/api/productcategory/delete", config, function () {
                    notificationService.displaySuccess("Xóa thành công!");
                    search();
                }, function () {
                    notificationService.displayError("Xóa không thành công!");
                });
            });
        }
        //method delete multi
        function deleteAllOrderDetails() {
            var listId = [];
            $ngBootbox.confirm("Bạn có muốn xóa không?").then(function () {
                $(".chk_allOrderDetails:checked").each(function () {
                    listId.push($(this).val());
                });
                var config = {
                    params: {
                        listId: JSON.stringify(listId)
                    }
                }
                apiService.delete("/api/productcategory/deletemulti", config, function (result) {
                    notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.displayError("Xóa không thành công!");
                });
            });
        }
        //call getproduct
        getOrderDetails();
    }
})(angular.module('default.orderDetails'));