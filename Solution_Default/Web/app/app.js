/// <reference path="../scripts/plugins/angular/angular.min.js" />

//config routing default
(function () {
    angular.module('default',
        [
            'default.products',
            'default.product_categories',
            'default.posts',
            'default.post_categories',
            'default.orders',
            'default.colors',
            'default.sizes',
            'default.banners',
            'default.common'
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
                templateUrl: "/app/components/home/views/Base.html",
                abstract: true
            })
            .state("login", {
                url: "/login",
                templateUrl: "/app/components/login/views/Login.html",
                controller: "loginController",
                authenticate: false
            })
            .state("home", {
                url: "/admin",
                parent: "base",
                templateUrl: "/app/components/home/views/homeView.html",
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
                ProductDetail: 'Product Detail',
                Category: 'Category',
                Post: 'New',
                PostCategory: 'New Category',
                Statistical: 'Statistical',
                Color: 'Color',
                Size: 'Size',
                Order: 'Order',
                Banner: 'Banner',
                OrderDetail: 'Order Detail',
                en: 'English',
                vi: 'Tiếng Việt',
                fr: 'French'
            })
            .translations('vi', {
                Product: 'Quảng lý sản phẩm',
                ProductDetail: 'Quảng lý chi tiết sản phẩm',
                Category: 'Quảng lý danh Mục',
                Post: 'Quảng lý tin tức',
                PostCategory: 'Quảng lý loại tin',
                Statistical: 'Thống kê',
                Color: 'Quảng lý màu sắc',
                Size: 'Quảng lý kích thước',
                Order: 'Quảng lý hóa đơn',
                Banner: 'Quảng lý quảng cáo',
                OrderDetail: 'Quảng lý chi tiết hóa đơn',
                en: 'English',
                vi: 'Tiếng Việt',
                fr: 'French'
            })
            .translations('fr', {
                Product: 'Product',
                ProductDetail: 'Product Detail',
                Category: 'Category',
                Post: 'Post',
                PostCategory: 'Post Category',
                Statistical: 'Statistical',
                Color: 'Color',
                Size: 'Size',
                Order: 'Order',
                OrderDetail: 'Order Detail',
                en: 'English',
                vi: 'Tiếng Việt',
                fr: 'French'
            });
        $translateProvider.preferredLanguage('en');
    }
})();