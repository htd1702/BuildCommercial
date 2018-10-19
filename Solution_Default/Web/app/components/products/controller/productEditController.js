(function (app) {
    app.controller('productEditController', productEditController);

    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    productEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function productEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        $scope.moreImages = [];
        //set value model
        $scope.product = {
            UpdatedDate: new Date(),
            Status: true
        }
        $scope.editorOptions = {
            lang: 'en',
            height: '120px'
        }
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditProduct = EditProduct;
        $scope.ChooseImage = ChooseImage;
        $scope.ChooseImageMore = ChooseImageMore;
        //binding title seo by name
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSEOTitle($scope.product.Name);
        }
        //Method get id product
        function loadProductDetail() {
            apiService.get("/api/product/getid/" + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                $scope.moreImages = JSON.parse($scope.product.MoreImages);
            }, function (error) {
                notificationService.displayError("Lấy id thất bại!");
            });
        }
        //method load parentId
        function LoadCategory() {
            apiService.get("/api/productcategory/getallparents", null, function (result) {
                $scope.categories = result.data;
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }
        //function add
        function EditProduct() {
            $scope.product.UpdatedBy = authData.authenticationData.userName;
            $scope.product.UpdatedDate = $scope.product.UpdatedDate;
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.put("/api/product/update", $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " cập nhật thành công!");
                $state.go("products");
            }, function (error) {
                notificationService.displayError("Cập nhật thất bại!");
            });
        }
        //funcion upload
        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filtUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = filtUrl;
                });
            }
            finder.popup();
        }
        //function upload multi img
        function ChooseImageMore() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filtUrl) {
                $scope.$apply(function () {
                    $scope.product.ImageMore = filtUrl;
                    $scope.moreImages.push(filtUrl);
                    $.unique($scope.moreImages.sort()).sort();
                });
            }
            finder.popup();
        }
        //call method load list categories
        loadProductDetail();
        LoadCategory();
    }
})(angular.module('default.products'));