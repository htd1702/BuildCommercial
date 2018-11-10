(function (app) {
    app.controller("productCategoryEditController", productCategoryEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    productCategoryEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        //set value model
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        //create funtion
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditProductCategory = EditProductCategory;
        $scope.ChooseImage = ChooseImage;
        //binding title seo bye name
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSEOTitle($scope.productCategory.Name);
        }
        //method load parentId
        function LoadParentCategory() {
            apiService.get("/api/productcategory/getallparents", null, function (result) {
                $scope.parentCategories = result.data;
                if (result.data[0].ParentID == 0)
                    $("select[name='parentId']").find("option").val('').attr("selected", "selected");
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }
        //load detail productcategory
        function loadProductCategoryDetail() {
            apiService.get("/api/productcategory/getid/" + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
            }, function (error) {
                notificationService.displayError("Lấy id thất bại!");
            });
        }
        //Edit
        function EditProductCategory() {
            $scope.productCategory.CreatedBy = authData.authenticationData.userName;
            $scope.productCategory.UpdatedBy = $scope.productCategory.CreatedBy;
            $scope.productCategory.UpdatedDate = $scope.productCategory.CreatedDate;
            apiService.put("/api/productcategory/update", $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " cập nhật thành công!");
                $state.go("product_categories");
            }, function (error) {
                notificationService.displayError("Cập nhật mới thất bại!");
            });
        }
        //funcion upload
        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function (filtUrl) {
                $scope.$apply(function () {
                    $scope.productCategory.Image = filtUrl;
                });
            };
            finder.popup();
        }
        //call method load parent
        loadProductCategoryDetail();
        LoadParentCategory();
    }
})(angular.module('default.product_categories'));