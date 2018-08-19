/// <reference path="../../../scripts/plugins/angular/angular.min.js" />

//config routing default
(function () {
    angular.module('default.products', ['default.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/app/components/products/views/productListView.html",
            controller: "productListController"
        }).state('product_add', {
            url: "/product_add",
            templateUrl: "/app/components/products/views/productAddView.html",
            controller: "productAddController"
        }).state('product_edit', {
            url: "/product_edit/:id",
            templateUrl: "/app/components/products/views/productEditView.html",
            controller: "productEditController"
        });
    }
})();