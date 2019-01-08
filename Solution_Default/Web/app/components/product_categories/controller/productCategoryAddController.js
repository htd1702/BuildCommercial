(function (app) {
    app.controller("productCategoryAddController", productCategoryAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    productCategoryAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService", "authData"];

    function productCategoryAddController($scope, apiService, notificationService, $state, commonService, authData) {
        //create value by type
        $scope.types = [
            //{ ID: 1, Name: "1" },
            { ID: 2, Name: "2" },
            { ID: 3, Name: "3" }
        ];
        //load option parentID
        $scope.parentCategories = [];
        //set value model
        $scope.productCategory = {
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
        $scope.AddProductCategory = AddProductCategory;
        $scope.ChooseImage = ChooseImage;
        $scope.changeType = changeType;
        //binding title seo by name
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSEOTitle($scope.productCategory.Name);
        }
        //function add
        function AddProductCategory() {
            if ($scope.productCategory.Type == undefined || $scope.productCategory.Type == "") {
                notificationService.displayWarning("Please enter a type!");
                return;
            }
            if ($scope.productCategory.Type != 1) {
                if ($scope.productCategory.ParentID == undefined || $scope.productCategory.ParentID == "") {
                    notificationService.displayWarning("Please choose a category!");
                    return;
                }
            }
            else {
                $scope.productCategory.ParentID = 0;
            }
            if ($scope.productCategory.DisplayOrder == undefined)
                $scope.productCategory.DisplayOrder = 0;
            $scope.productCategory.CreatedBy = authData.authenticationData.userName;
            $scope.productCategory.UpdatedBy = $scope.productCategory.CreatedBy;
            $scope.productCategory.UpdatedDate = $scope.productCategory.CreatedDate;
            apiService.post("/api/productcategory/create", $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " Add successfully!");
                $state.go("product_categories");
            }, function (error) {
                notificationService.displayError("Add Faild!");
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
        //function change
        function changeType(type) {
            var obj = {
                params: {
                    type: (parseInt(type) - 1)
                }
            };
            //
            if (type == 2) {
                apiService.get("/api/productcategory/loadListparentbytype", obj, function (result) {
                    $scope.parentCategories = result.data;
                }, function () {
                    notificationService.displayError("Load Failed!");
                });
            }
            else if (type == 3) {
                apiService.get("/api/productcategory/loadListparentbytype", obj, function (result) {
                    $scope.parentCategories = result.data;
                }, function () {
                    notificationService.displayError("Load Failed!");
                });
            }
        }
        //call method load parent
        if ($scope.productCategory.Type == undefined || $scope.productCategory.Type == "")
            $scope.productCategory.Type = 2;
        $scope.$watch('productCategory.Type', changeType($scope.productCategory.Type));
    }
})(angular.module('default.product_categories'));