/// <reference path="../../../../scripts/plugins/angular/angular.min.js" />

(function () {
    angular.module("default.orderDetails", ['default.common']).config(config);
    //$stateProvider cai dat cau hinh link controller name
    //$urlRouterProvider setting default
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        //setting config url, page , controller in each function
        $stateProvider
            .state("orderDetails", {
                url: "/orderDetails",
                parent: "base",
                templateUrl: "/app/components/order_details/views/orderDetailListView.html",
                controller: "orderDetailListController"
            });
    }
})();