/// <reference path="../scripts/plugins/angular/angular.min.js" />

//config routing default
(function () {
    angular.module('default',
        [
            'default.products',
            'default.product_categories',
            'default.common',
        ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state("home", {
            url: "/admin",
            templateUrl: "/admin/admin/homeView",
            controller: "homeController"
        });
        $urlRouterProvider.otherwise('/admin');
    }
})();