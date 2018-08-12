(function (app) {
    app.controller('productAddController', productAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    productAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService"];

    function productAddController($scope, apiService, notificationService, $state, commonService) {
        //set value model
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        //binding title seo by name
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSEOTitle($scope.product.Name);
        }
    }
})(angular.module('default.products'));