/// <reference path="../../../scripts/angular.js" />

(function () {
    angular.module("default.product_categories", ['default.common']).config(config);
    //$stateProvider cai dat cau hinh link controller name 
    //$urlRouterProvider setting default
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        //setting config url, page , controller in each function
        $stateProvider.state("product_categories", {
            url: "/product_categories",
            templateUrl: "/app/components/product_categories/productCategoryListView.html",
            controller: "productCategoryListController"
        }).state("add_product_category", {
            url: "/add_product_category",
            templateUrl: "/app/components/product_categories/productCategoryAddView.html",
            controller: "productCategoryAddController"
        }).state("edit_product_category", {
            url: "/edit_product_category",
            templateUrl: "/app/components/product_categories/productCategoryEditView.html",
            controller: "productCategoryEditController"
        });
    }
})();