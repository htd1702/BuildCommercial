/// <reference path="../../../../scripts/plugins/angular/angular.min.js" />

(function () {
    angular.module("default.colors", ['default.common']).config(config);
    //$stateProvider cai dat cau hinh link controller name
    //$urlRouterProvider setting default
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        //setting config url, page , controller in each function
        $stateProvider
            .state("colors", {
                url: "/colors",
                parent: "base",
                templateUrl: "/app/components/colors/views/colorListView.html",
                controller: "colorListController"
            })
            .state("add_color", {
                url: "/add_color",
                parent: "base",
                templateUrl: "/app/components/colors/views/colorAddView.html",
                controller: "colorAddController"
            })
            .state("edit_color", {
                url: "/edit_color/:id",
                parent: "base",
                templateUrl: "/app/components/colors/views/colorEditView.html",
                controller: "colorEditController"
            });
    }
})();