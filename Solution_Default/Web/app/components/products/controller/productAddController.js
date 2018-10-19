(function (app) {
    app.controller('productAddController', productAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    productAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService", "authData"];

    function productAddController($scope, apiService, notificationService, $state, commonService, authData) {
        $scope.categories = [];
        $scope.moreImages = [];
        if ($scope.moreImages == "") {
            $("input[name=imageMore]").show();
        }

        //set value model
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        };
        //setting ckeditor
        $scope.editorOptions = {
            lang: 'en',
            height: '120px'
        };
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.AddProduct = AddProduct;
        $scope.ChooseImage = ChooseImage;
        $scope.ChooseImageMore = ChooseImageMore;
        //binding title seo by name
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSEOTitle($scope.product.Name);
        }
        //method load parentId
        function LoadCategory() {
            apiService.get("/api/productcategory/getallparents", null, function (result) {
                $scope.categories = result.data;
                $scope.product.CategoryID = $scope.categories[0].ID;
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }
        //function add
        function AddProduct() {
            $scope.product.Description = $scope.product.Content;
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            $scope.product.CreatedBy = authData.authenticationData.userName;
            $scope.product.UpdatedBy = $scope.product.CreatedBy;
            $scope.product.UpdatedDate = $scope.product.CreatedDate;
            apiService.post("/api/product/create", $scope.product, function (result) {
                notificationService.displaySuccess(result.data.Name + " thêm thành công!");
                $state.go("products");
            }, function (error) {
                notificationService.displayError("Thêm mới thất bại!");
            });
        }
        //funcion upload
        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filtUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = filtUrl;
                });
            };
            finder.popup();
        }
        //function upload multi img
        function ChooseImageMore() {
            $("input[name=imageMore]").hide();
            var finder = new CKFinder();
            finder.selectActionFunction = function (filtUrl) {
                $scope.$apply(function () {
                    $scope.product.ImageMore = filtUrl;
                    $scope.moreImages.push(filtUrl);
                    $.unique($scope.moreImages.sort()).sort();
                });
            };
            finder.popup();
        }
        //call method load list categories
        LoadCategory();
    }
})(angular.module('default.products'));