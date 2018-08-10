(function (app) {
    app.controller("productCategoryEditController", productCategoryEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    productCategoryEditController.$inject = ["$scope", "apiService", "notificationService", "$state"];

    function productCategoryEditController($scope, apiService, notificationService, $state) {
       
    }
})(angular.module('default.product_categories'));