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
        $stateProvider
            .state("base", {
                url: "",
                templateUrl: "/Admin/Home/Base",
                abstract: true
            })
            .state("login", {
                url: "/login",
                templateUrl: "/Admin/Account/Login",
                controller: "loginController",
                authenticate: false
            })
            .state("home", {
                url: "/",
                parent: "base",
                templateUrl: "/Admin/Home/homeView",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/login');
    }
})();