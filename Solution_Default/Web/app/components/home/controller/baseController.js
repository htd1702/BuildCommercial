(function (app) {
    app.controller('baseController', baseController);

    baseController.$inject = ["$translate"];

    function baseController($translate) {
        var translate = this;

        translate.language = 'en';

        translate.languages = ['en', 'vi', 'fr'];

        translate.updateLanguage = function (index) {
            $translate.use(translate.languages[index]);
        };
    }
})(angular.module('default'));