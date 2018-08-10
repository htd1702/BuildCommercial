/// <reference path="../scripts/angular.js" />
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
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        });
        $urlRouterProvider.otherwise('/admin');
    }
})();