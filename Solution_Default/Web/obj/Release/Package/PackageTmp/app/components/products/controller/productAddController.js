(function (app) {
    app.controller('productAddController', productAddController);
    //apiService tầng service gọi get post put delete
    //notificationService message thông báo
    //$state : trỏ đến page
    productAddController.$inject = ["$scope", "apiService", "notificationService", "$state", "commonService", "authData"];

    function productAddController($scope, apiService, notificationService, $state, commonService, authData) {
        $scope.categories = [];
        $scope.moreImages = [];
        $scope.listColor = [];
        $scope.listSize = [];
        //create function
        $scope.GetSeoTitle = GetSeoTitle;
        $scope.AddProduct = AddProduct;
        $scope.ChooseImage = ChooseImage;
        $scope.ChooseImageMore = ChooseImageMore;
        $scope.GetIndexCode = GetIndexCode();
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
        //binding title seo by name
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSEOTitle($scope.product.Name);
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
                $scope.product.CategoryID = $scope.categories[0].ID;
            }, function () {
                notificationService.displayError("Load thất bại!");
            });
        }
        //function add
        function AddProduct() {
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
            $scope.product.Description = $scope.product.Content;
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            $scope.product.CreatedBy = authData.authenticationData.userName;
            $scope.product.UpdatedBy = $scope.product.CreatedBy;
            $scope.product.UpdatedDate = $scope.product.CreatedDate;
            if ($scope.product.HomeFlag == undefined)
                $scope.product.HomeFlag = false;
            if ($scope.product.HotFlag == undefined)
                $scope.product.HotFlag = false;
            if ($scope.product.Status == undefined)
                $scope.product.Status = false;
            if ($scope.product.colorList.length > 0) {
                apiService.post("/api/product/create", $scope.product, function (result) {
                    notificationService.displaySuccess(result.data.Name + " thêm thành công!");
                    $state.go("products");
                }, function (error) {
                    notificationService.displayError("Thêm mới thất bại!");
                });
            }
            else {
                notificationService.displayWarning("Bạn chưa chọn thuộc tính sản phẩm, vui lòng kiểm tra lại!");
            }
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
        //load code product
        function GetIndexCode() {
            apiService.get('/api/product/getindex', null, function (result) {
                $scope.product.Code = result.data;
            });
        }
        //call method load list categories
        LoadCategory();
        LoadListColor();
        LoadListSize();
        GetIndexCode();
    }
})(angular.module('default.products'));