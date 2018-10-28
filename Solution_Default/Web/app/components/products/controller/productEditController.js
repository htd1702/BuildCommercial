(function (app) {
    app.controller('productEditController', productEditController);

    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state: trỏ đến page
    //$stateParams: set param
    productEditController.$inject = ["$scope", "apiService", "notificationService", "$state", "$stateParams", "commonService", "authData"];

    function productEditController($scope, apiService, notificationService, $state, $stateParams, commonService, authData) {
        $scope.moreImages = [];
        $scope.listColor = [];
        $scope.listSize = [];
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.EditProduct = EditProduct;
        $scope.ChooseImage = ChooseImage;
        $scope.ChooseImageMore = ChooseImageMore;
        //set value model
        $scope.product = {
            UpdatedDate: new Date(),
            Status: true
        }
        $scope.editorOptions = {
            lang: 'en',
            height: '120px'
        }
        //binding title seo by name
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSEOTitle($scope.product.Name);
        }
        //load list color
        function LoadListColor() {
            apiService.get('/api/color/getall', null, function (result) {
                $scope.listColor = result.data;
            });
        }
        //load list size
        function LoadListSize() {
            apiService.get('/api/size/getall', null, function (result) {
                $scope.listSize = result.data;
            });
        }
        //method load parentId
        function LoadCategory() {
            var obj = {
                params: {
                    type: 2
                }
            };
            apiService.get("/api/productcategory/getallparentbytype", obj, function (result) {
                $scope.categories = result.data;
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }
        //function add
        function EditProduct() {
            $scope.product.colorList = [];
            $scope.product.sizeList = [];
            $scope.product.quantityList = [];
            //get value list size
            if ($("input[name=chkSize]").length > 0) {
                $("input[name=chkSize]").each(function () {
                    if ($(this).is(":checked"))
                        $scope.product.sizeList.push($(this).attr("data-id"));
                });
            }
            //get value list color
            if ($("input[name=chkColor]").length > 0) {
                $("input[name=chkColor]").each(function () {
                    if ($(this).is(":checked")) {
                        var value = $(this).attr("data-id");
                        $scope.product.colorList.push(value);
                        var quantity = $(".form-size > .input-check").find("[data-quantity=" + value + "]").val();
                        if (quantity != "" && quantity != undefined)
                            $scope.product.quantityList.push(quantity);
                    }
                });
            }
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
        LoadListColor();
        LoadListSize();
        LoadCategory();
        $(document).ready(function () {
            //Method get id product
            function loadProductDetail() {
                var size = {
                    params: {
                        id: $stateParams.id,
                        type: 2
                    }
                };
                var color = {
                    params: {
                        id: $stateParams.id,
                        type: 1
                    }
                };
                apiService.get("/api/product/getid/" + $stateParams.id, null, function (result) {
                    $scope.product = result.data;
                    $scope.moreImages = JSON.parse($scope.product.MoreImages);
                }, function (error) {
                    notificationService.displayError("Lấy id thất bại!");
                });
                //Get list color
                apiService.get('/api/productdetail/getlistproductbysizecolor', color, function (result) {
                    console.log(result.data);
                    if (result.data.length > 0) {
                        //get value list color
                        $("input[name=chkColor]").each(function () {
                            var value = $(this).attr("data-id");
                            for (var i = 0; i < result.data.length; i++) {
                                if (value == result.data[i].ColorID) {
                                    $(this).prop("checked", true);
                                    $(".form-size > .input-check").find("[data-quantity=" + value + "]").val(result.data[i].Quantity);
                                }
                            }
                        });
                    }
                });
                //get list size
                apiService.get('/api/productdetail/getlistproductbysizecolor', size, function (result) {
                    console.log(result.data);
                    if (result.data.length > 0) {
                        //get value list size
                        $("input[name=chkSize]").each(function () {
                            var value = $(this).attr("data-id");
                            for (var i = 0; i < result.data.length; i++) {
                                if (value == result.data[i].SizeID)
                                    $(this).prop("checked", true);
                            }
                        });
                    };
                });
            }
            loadProductDetail();
        });
    }
})(angular.module('default.products'));