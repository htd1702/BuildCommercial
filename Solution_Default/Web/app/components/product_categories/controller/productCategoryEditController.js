(function (app) {
    app.controller("productCategoryEditController", productCategoryEditController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    productCategoryEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        //create value by type
        $scope.types = [
            //{ ID: 1, Name: "1" },
            { ID: 2, Name: "2" },
            { ID: 3, Name: "3" }
        ];
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
        //create funtion
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditProductCategory = EditProductCategory;
        $scope.ChooseImage = ChooseImage;
        $scope.changeType = changeType;
        //binding title seo bye name
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSEOTitle($scope.productCategory.Name);
        }
        //load detail productcategory
        function loadProductCategoryDetail() {
            apiService.get("/api/productcategory/getid/" + $stateParams.id, null, function (result) {
                $scope.productCategory = result.data;
                $scope.$watch('productCategory.Type', changeType(result.data.Type));
            }, function (error) {
                notificationService.displayError("Get id failed!");
            });
        }
        //Edit
        function EditProductCategory() {
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
            apiService.put("/api/productcategory/update", $scope.productCategory, function (result) {
                notificationService.displaySuccess(result.data.Name + " success!");
                $state.go("product_categories");
            }, function (error) {
                notificationService.displayError("Update failed!");
            });
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
    }
})(angular.module('default.product_categories'));