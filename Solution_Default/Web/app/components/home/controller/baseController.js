(function (app) {
    app.controller('baseController', baseController);

    baseController.$inject = ["$translate"];

    function baseController($translate) {
        var translate = this;

        translate.language = 'en';

        translate.languages = ['en', 'vi'];

        translate.updateLanguage = function () {
            $translate.use(translate.language);
        };
    }
})(angular.module('default'));
