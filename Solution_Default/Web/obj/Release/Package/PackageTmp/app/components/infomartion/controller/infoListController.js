(function (app) {
    app.controller("infoListController", infoListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    infoListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter"];

    function infoListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        //scope binding
        $scope.infos = [];
        //create funtion
        $scope.getInfos = getInfos;
        //method get product cate
        function getInfos() {
            //call apiService url,params,success,error
            apiService.get('/api/info/getall', null, function (result) {
                if (result.data.TotalCount == 0)
                    notificationService.displayWarning("No records were found!");
                $scope.infos = result.data;
            }, function () {
                console.log('Load info failed.');
            });
        }
        //call getproduct
        getInfos();
    }
})(angular.module('default.infos'));