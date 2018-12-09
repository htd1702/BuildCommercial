/// <reference path="../../../../scripts/plugins/angular/angular.min.js" />

(function () {
    angular.module("default.banners", ['default.common']).config(config);
    //$stateProvider cai dat cau hinh link controller name
    //$urlRouterProvider setting default
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        //setting config url, page , controller in each function
        $stateProvider
            .state("banners", {
                url: "/banners",
                parent: "base",
                templateUrl: "/app/components/banner/views/bannerListView.html",
                controller: "bannerListController"
            })
            .state("add_banner", {
                url: "/add_banner",
                parent: "base",
                templateUrl: "/app/components/banner/views/bannerAddView.html",
                controller: "bannerAddController"
            })
            .state("edit_banner", {
                url: "/edit_banner/:id",
                parent: "base",
                templateUrl: "/app/components/banner/views/bannerEditView.html",
                controller: "bannerEditController"
            });
    }
})();