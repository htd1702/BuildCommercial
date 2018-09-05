/// <reference path="../../../scripts/plugins/angular/angular.min.js" />

(function (app) {
    'use strict';

    app.factory("authData", authData);

    function authData() {
        var authDataFactory = {};
        //set authen when use is null
        var authentication = {
            IsAuthenticated: false,
            useName: ""
        };
        authDataFactory.authenticationData = authentication;
        return authDataFactory;
    }
})(angular.module("default.common"));