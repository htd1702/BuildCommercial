/// <reference path="../../../../scripts/plugins/angular/angular.min.js" />

(function () {
    angular.module("default.product_details", ['default.common']).config(config);
    //$stateProvider cai dat cau hinh link controller name
    //$urlRouterProvider setting default
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        //setting config url, page , controller in each function
        $stateProvider
            .state("product_details", {
                url: "/product_details",
                parent: "base",
                templateUrl: "/app/components/product_details/views/productDetailListView.html",
                controller: "productDetailListController"
            });
    }
})();