/// <reference path="../../../scripts/plugins/angular/angular.min.js" />

//config routing default
(function () {
    angular.module('default.products', ['default.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/Admin/Product/productListView",
            controller: "productListController"
        }).state('product_add', {
            url: "/product_add",
            templateUrl: "/Admin/Product/productAddView",
            controller: "productAddController"
        }).state('product_edit', {
            url: "/product_edit/:id",
            templateUrl: "/Admin/Product/productEditView",
            controller: "productEditController"
        });
    }
})();