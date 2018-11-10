/// <reference path="../../../../scripts/plugins/angular/angular.min.js" />

(function () {
    angular.module("default.post_categories", ['default.common']).config(config);
    //$stateProvider cai dat cau hinh link controller name
    //$urlRouterProvider setting default
    config.$inject = ["$stateProvider", "$urlRouterProvider"];

    function config($stateProvider, $urlRouterProvider) {
        //setting config url, page , controller in each function
        $stateProvider
            .state("post_categories", {
                url: "/post_categories",
                parent: "base",
                templateUrl: "/app/components/post_categories/views/postCategoryListView.html",
                controller: "postCategoryListController"
            })
            .state("add_post_category", {
                url: "/add_post_category",
                parent: "base",
                templateUrl: "/app/components/post_categories/views/postCategoryAddView.html",
                controller: "postCategoryAddController"
            })
            .state("edit_post_category", {
                url: "/edit_post_category/:id",
                parent: "base",
                templateUrl: "/app/components/post_categories/views/postCategoryEditView.html",
                controller: "postCategoryEditController"
            });
    }
})();