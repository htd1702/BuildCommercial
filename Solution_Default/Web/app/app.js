/// <reference path="../scripts/plugins/angular/angular.min.js" />

//config routing default
(function () {
    angular.module('default',
        [
            'default.products',
            'default.product_categories',
            'default.common',
        ])
        .config(config)
        .config(configAuthentication)
        .config(translations);
    //inject config
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    //function module config
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
                url: "/home",
                parent: "base",
                templateUrl: "/Admin/Home/homeView",
                controller: "homeController"
            });
        $urlRouterProvider.otherwise('/login');
    }
    //inject configAuthentication
    configAuthentication.$inject = ['$httpProvider'];
    //function module configAuthentication
    function configAuthentication($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location) {
            return {
                request: function (config) {
                    return config;
                },
                requestError: function (rejection) {
                    return $q.reject(rejection);
                },
                response: function (response) {
                    if (response.status == "401") {
                        $location.path('/login');
                    }
                    //the same response/modified/or a new one need to be returned.
                    return response;
                },
                responseError: function (rejection) {
                    if (rejection.status == "401") {
                        $location.path('/login');
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
    //inject translations
    translations.$inject = ['$translateProvider'];
    //function module translations
    function translations($translateProvider) {
        $translateProvider
            .translations('en', {
                Product: 'Product',
                Category: 'Category',
                en: 'English',
                vi: 'Tiếng Việt'
            })
            .translations('vi', {
                Product: 'Sản phẩm',
                Category: 'Danh Mục',
                en: 'English',
                vi: 'Tiếng Việt'
            });
        $translateProvider.preferredLanguage('en');
    }
})();