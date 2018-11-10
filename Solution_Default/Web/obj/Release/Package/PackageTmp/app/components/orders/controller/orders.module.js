/// <reference path="../../../../scripts/plugins/angular/angular.min.js" />

(function () {
    angular.module("default.orders", ['default.common']).config(config);
    //$stateProvider cai dat cau hinh link controller name
    //$urlRouterProvider setting default
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        //setting config url, page , controller in each function
        $stateProvider
            .state("orders", {
                url: "/orders",
                parent: "base",
                templateUrl: "/app/components/orders/views/orderListView.html",
                controller: "orderListController"
            })
            .state("order_edit", {
                url: "/order_edity/:id",
                parent: "base",
                templateUrl: "/app/components/orders/views/orderEditView.html",
                controller: "orderEditController"
            });
    }
})();