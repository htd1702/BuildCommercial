(function (app) {
    app.controller('baseController', baseController);

    baseController.$inject = ["$translate", "$scope", "$http", 'apiService'];

    function baseController($translate, $scope, $http, apiService) {
        $scope.countOrder = 0;
        $scope.Notifications = [];
        $scope.Show_Notifications = Show_Notifications;
        function Show_Notifications() {
            apiService.get('/api/order/getneworder', null, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("Không có bản ghi nào được tìm thấy!");
                else {
                    $scope.Notifications = result.data;
                }
            }, function () {
                console.log('Load post failed.');
            });
        }
        //create tran
        var translate = this;
        //set tran en
        translate.language = 'en';
        //set multi tran 
        translate.languages = ['en', 'vi', 'fr'];
        //function tran
        translate.updateLanguage = function (index) {
            $translate.use(translate.languages[index]);
        };
    }
})(angular.module('default'));