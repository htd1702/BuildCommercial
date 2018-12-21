(function (app) {
    app.controller("orderEditController", orderEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    orderEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function orderEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        $scope.order = [];
        //create funtion
        $scope.EditOrder = EditOrder;
        //Edit
        function EditOrder() {
            $scope.order.CreatedBy = authData.authenticationData.userName;
            $scope.order.UpdatedBy = $scope.order.CreatedBy;
            $scope.order.UpdatedDate = $scope.order.CreatedDate;
            apiService.put("/api/order/update", $scope.order, function (result) {
                notificationService.displaySuccess(result.data.CustomerName + " success!");
                $state.go("orders");
            }, function (error) {
                notificationService.displayError("Faild!");
            });
        }

        //load detail productcategory
        function loadOrder() {
            apiService.get("/api/order/getid/" + $stateParams.id, null, function (result) {
                $scope.order = result.data;
            }, function (error) {
                notificationService.displayError("Faild!");
            });
        }
        //call funtion load order
        loadOrder();
    }
})(angular.module('default.orders'));