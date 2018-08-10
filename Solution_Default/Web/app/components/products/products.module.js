/// <reference path="../../../scripts/angular.js" />
//config routing default
(function () {
    angular.module('default.products', ['default.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/app/components/products/productListView.html",
            controller: "productListController"
        }).state('product_add', {
            url: "/product_add",
            templateUrl: "/app/components/products/productAddView.html",
            controller: "productAddController"
        }).state('product_edit', {
            url: "/product_edit",
            templateUrl: "/app/components/products/productEditView.html",
            controller: "productEditController"
        });
    }
})();