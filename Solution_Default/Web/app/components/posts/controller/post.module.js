/// <reference path="../../../../scripts/plugins/angular/angular.min.js" />

//config routing default
(function () {
    angular.module('default.posts', ['default.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider
            .state('posts', {
                url: "/posts",
                parent: "base",
                templateUrl: "/app/components/posts/views/postListView.html",
                controller: "postListController"
            })
            .state('post_add', {
                url: "/post_add",
                parent: "base",
                templateUrl: "/app/components/posts/views/postAddView.html",
                controller: "postAddController"
            })
            .state('post_edit', {
                url: "/post_edit/:id",
                parent: "base",
                templateUrl: "/app/components/posts/views/postEditView.html",
                controller: "postEditController"
            });
    }
})();