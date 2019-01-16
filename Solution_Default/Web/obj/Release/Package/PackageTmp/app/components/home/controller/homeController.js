(function (app) {
    app.controller('homeController', homeController);

    homeController.$inject = ["$scope", "$http", "notificationService"];

    function homeController($scope, $http, notificationService) {
        $scope.report = [];
        $scope.ListReport = ListReport;
        $scope.fromDate = new Date();
        $scope.toDate = new Date();
        $scope.search = search;
        function ListReport(fromDate, toDate) {
            return $http({
                url: '/Product/ReportProduct',
                method: "POST",
                data: {
                    fromDate: fromDate,
                    toDate: toDate
                }
            }).then(function (result) {
                $scope.report = result.data;
            }, function (error) {
                notificationService.displayError("Failed!");
            });
        }
        function search() {
            var fromDate = $("input[name='fromDate']").val();
            var toDate = $("input[name='toDate']").val();
            ListReport(fromDate, toDate);
        }
        ListReport($("input[name='fromDate']").val(), $("input[name='toDate']").val());
    }
})(angular.module('default'));
