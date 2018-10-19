(function (app) {
    app.controller("productDetailListController", productDetailListController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$ngBootbox show modelbox
    //$filter liberty
    productDetailListController.$inject = ["$scope", "apiService", "notificationService", "$ngBootbox", "$filter", "authData"];

    function productDetailListController($scope, apiService, notificationService, $ngBootbox, $filter, authData) {
        //scope binding
        $scope.productDetail = [];
        $scope.listColor = [];
        $scope.listSize = [];
        $scope.categories = [];
        $scope.productCategories = [];
        $scope.products = [];
        $scope.ImageMore = [];
        $scope.changeCategory = changeCategory;
        $scope.changeProductCategory = changeProductCategory;
        $scope.AddProductDetail = AddProductDetail;
        //$scope.ChooseImageMore = ChooseImageMore;
        function LoadCategory() {
            var obj = {
                params: {
                    id: 0,
                    type: 1
                }
            };
            apiService.get('/api/productdetail/getlistcategory', obj, function (result) {
                $scope.categories = result.data;
                if (result.data.length > 0) {
                    $scope.category = $scope.categories[0].ID;
                    changeCategory($scope.categories[0].ID);
                }
            });
        }
        //load list color
        function LoadListColor() {
            var obj = {
                params: {
                    type: 1
                }
            };
            apiService.get('/api/list/getallbytype', obj, function (result) {
                $scope.listColor = result.data;
            });
        }
        //load list size
        function LoadListSize() {
            var obj = {
                params: {
                    type: 2
                }
            };
            apiService.get('/api/list/getallbytype', obj, function (result) {
                $scope.listSize = result.data;
            });
        }
        //function change category
        function changeCategory(value) {
            var obj = {
                params: {
                    id: value,
                    type: 2
                }
            };

            if (value > 0) {
                apiService.get('/api/productdetail/getlistcategory', obj, function (result) {
                    $scope.productCategories = result.data;
                    if ($scope.productCategories.length > 0) {
                        $scope.product_category = $scope.productCategories[0].ID;
                        changeProductCategory($scope.productCategories[0].ID);
                    }
                    else
                        changeProductCategory(0);
                });
            }
        }
        //function change product
        function changeProductCategory(value) {
            var obj = {
                params: {
                    id: value,
                    type: 3
                }
            }
            if (value > 0) {
                apiService.get('/api/productdetail/getlistcategory', obj, function (result) {
                    $scope.products = result.data;
                    if ($scope.products.length > 0)
                        $scope.product = $scope.products[0].ID;
                });
            }
            else
                $scope.products = [];
        }
        ////function  Choose img more
        //function ChooseImageMore($index) {
        //    var finder = new CKFinder();
        //    finder.selectActionFunction = function (filtUrl) {
        //        $scope.$apply(function () {
        //            //$scope.ListImgMore.push(filtUrl);
        //            //$scope.ImageMore[$index] = $scope.ListImgMore;
        //            //$.unique($scope.ImageMore.sort()).sort();
        //            $scope.ImageMore[$index] = filtUrl;
        //        });
        //    };
        //    finder.popup();
        //}
        //function add
        function AddProductDetail(id) {
            var colorList = [];
            var sizeList = [];
            var listQuantity = [];
            $("input[name=chkSize]").each(function () {
                if ($(this).is(":checked"))
                    sizeList.push($(this).attr("data-id"));
            });

            $("input[name=chkColor]").each(function () {
                if ($(this).is(":checked")) {
                    var value = $(this).attr("data-id");
                    colorList.push(value);
                    var quantity = $(".form-size > .col-md-3").find("[data-quantity=" + value + "]").val();
                    if (quantity != "" && quantity != undefined)
                        listQuantity.push(quantity);
                }
            });

            var obj = {
                id: id,
                colorList: colorList,
                sizeList: sizeList,
                quantityList: listQuantity,
                createdBy: authData.authenticationData.userName,
            };

            apiService.post("/api/productdetail/create", obj, function (result) {
                notificationService.displaySuccess(result.data.Name + " thêm thành công!");
            }, function (error) {
                notificationService.displayError("Thêm mới thất bại!");
            });
        }
        LoadListColor();
        LoadListSize();
        LoadCategory();
    }
})(angular.module('default.product_details'));