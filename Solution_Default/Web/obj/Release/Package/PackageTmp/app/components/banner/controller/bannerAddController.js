(function (app) {
    app.controller("bannerAddController", bannerAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    bannerAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService", "authData"];

    function bannerAddController($scope, apiService, notificationService, $state, commonService, authData) {
        //create value by type
        $scope.types = [
            { ID: 1, Name: "1" },
            { ID: 2, Name: "2" },
            { ID: 3, Name: "3" }
        ];
        //set value model
        $scope.banner = {
            CreatedDate: new Date(),
            Status: true
        };
        //setting ckeditor
        $scope.editorOptions = {
            lang: 'en',
            height: '120px'
        };
        //create function
        $scope.AddBanner = AddBanner;
        $scope.changeType = changeType;
        $scope.ChooseImage = ChooseImage;
        //function add
        function AddBanner() {
            if ($scope.banner.type == undefined)
                $scope.banner.type = 0;
            $scope.banner.CreatedBy = authData.authenticationData.userName;
            $scope.banner.UpdatedBy = $scope.banner.CreatedBy;
            $scope.banner.UpdatedDate = $scope.banner.CreatedDate;
            apiService.post("/api/banner/create", $scope.banner, function (result) {
                notificationService.displaySuccess(result.data.Name + " thêm thành công!");
                $state.go("banners");
            }, function (error) {
                notificationService.displayError("Thêm mới thất bại!");
            });
        }
        //funtion load type
        function changeType(type) {
            if (type == 1) {
                $scope.banner.TitleType = "Page Home";
            }
            else if (type == 2) {
                $scope.banner.TitleType = "Page Sale";
            }
            else if (type == 3) {
                $scope.banner.TitleType = "Page New";
            }
        }
        //funcion upload
        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filtUrl) {
                $scope.$apply(function () {
                    $scope.banner.Image = filtUrl;
                });
            };
            finder.popup();
        }
    }
})(angular.module('default.banners'));