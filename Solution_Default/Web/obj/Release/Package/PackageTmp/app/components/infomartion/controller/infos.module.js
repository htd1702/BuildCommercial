/// <reference path="../../../../scripts/plugins/angular/angular.min.js" />

(function () {
    angular.module("default.infos", ['default.common']).config(config);
    //$stateProvider cai dat cau hinh link controller name
    //$urlRouterProvider setting default
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        //setting config url, page , controller in each function
        $stateProvider
            .state("infos", {
                url: "/infos",
                parent: "base",
                templateUrl: "/app/components/infomartion/views/infoListView.html",
                controller: "infoListController"
            })
            .state("edit_info", {
                url: "/edit_info/:id",
                parent: "base",
                templateUrl: "/app/components/infomartion/views/infoEditView.html",
                controller: "infoEditController"
            });
    }
})();