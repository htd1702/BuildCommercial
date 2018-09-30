/// <reference path="../../../../scripts/plugins/angular/angular.min.js" />

(function () {
    angular.module("default.product_categories", ['default.common']).config(config);
    //$stateProvider cai dat cau hinh link controller name
    //$urlRouterProvider setting default
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        //setting config url, page , controller in each function
        $stateProvider
            .state("product_categories", {
                url: "/product_categories",
                parent: "base",
                templateUrl: "/app/components/product_categories/views/productCategoryListView.html",
                controller: "productCategoryListController"
            })
            .state("add_product_category", {
                url: "/add_product_category",
                parent: "base",
                templateUrl: "/app/components/product_categories/views/productCategoryAddView.html",
                controller: "productCategoryAddController"
            })
            .state("edit_product_category", {
                url: "/edit_product_category/:id",
                parent: "base",
                templateUrl: "/app/components/product_categories/views/productCategoryEditView.html",
                controller: "productCategoryEditController"
            });
    }
})();