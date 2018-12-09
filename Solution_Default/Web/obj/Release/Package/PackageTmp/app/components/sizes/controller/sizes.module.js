/// <reference path="../../../../scripts/plugins/angular/angular.min.js" />

(function () {
    angular.module("default.sizes", ['default.common']).config(config);
    //$stateProvider cai dat cau hinh link controller name
    //$urlRouterProvider setting default
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        //setting config url, page , controller in each function
        $stateProvider
            .state("sizes", {
                url: "/sizes",
                parent: "base",
                templateUrl: "/app/components/sizes/views/sizeListView.html",
                controller: "sizeListController"
            })
            .state("add_size", {
                url: "/add_size",
                parent: "base",
                templateUrl: "/app/components/sizes/views/sizeAddView.html",
                controller: "sizeAddController"
            })
            .state("edit_size", {
                url: "/edit_size/:id",
                parent: "base",
                templateUrl: "/app/components/sizes/views/sizeEditView.html",
                controller: "sizeEditController"
            });
    }
})();