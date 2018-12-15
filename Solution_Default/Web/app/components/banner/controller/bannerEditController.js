(function (app) {
    app.controller("bannerEditController", bannerEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    bannerEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function bannerEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        //create value by type
        $scope.types = [
            { ID: 1, Name: "1" },
            { ID: 2, Name: "2" },
            { ID: 3, Name: "3" },
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
        //create funtion
        $scope.EditBanner = EditBanner;
        $scope.changeType = changeType;
        $scope.ChooseImage = ChooseImage;
        //load detail banner
        function loadBannerDetail() {
            apiService.get("/api/banner/getid/" + $stateParams.id, null, function (result) {
                $scope.banner = result.data;
                $scope.$watch('banner.TitleType', changeType(result.data.type));
            }, function (error) {
                notificationService.displayError("Lấy id thất bại!");
            });
        }
        //Edit
        function EditBanner() {
            $scope.banner.CreatedBy = authData.authenticationData.userName;
            $scope.banner.UpdatedBy = $scope.banner.CreatedBy;
            $scope.banner.UpdatedDate = $scope.banner.CreatedDate;
            apiService.put("/api/banner/update", $scope.banner, function (result) {
                notificationService.displaySuccess(result.data.Name + " cập nhật thành công!");
                $state.go("banners");
            }, function (error) {
                notificationService.displayError("Cập nhật mới thất bại!");
            });
        }
        //function change type
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
        //call method load parent
        loadBannerDetail();
    }
})(angular.module('default.banners'));