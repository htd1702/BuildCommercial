/// <reference path="../plugins/angular/angular.min.js" />
/// <reference path="../plugins/jquery/jquery.min.js" />

var app = angular.module("app_client", ['ngRoute', 'ngAnimate']).directive('paginDirective', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            if (scope.$last == true) {
                $timeout(function () {
                    scope.$emit(attr.paginDirective);
                });
            }
        }
    };
}).filter('promotionPriceFilter', function () {
    return function (input) {
        if (input > 0)
            return input + "%";
        else
            return "New";
    }
});
app.controller("ProductController", ProductController);
app.controller("ProductCategoryController", ProductCategoryController);
app.controller("ProductSaleController", ProductSaleController);
app.controller("NewProductController", NewProductController);
app.controller("ShoppingCartController", ShoppingCartController);

//---------------------------FILTER---------------------------------//
app.filter('offset', function () {
    return function (input, start) {
        start = parseInt(start, 10);
        return input.slice(start);
    };
});
//funcion change Stamped
function changeStamped(str) {
    str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
    str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
    str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
    str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
    str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
    str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
    str = str.replace(/đ/g, "d");
    str = str.replace(/À|Á|Ạ|Ả|Ã|Â|Ầ|Ấ|Ậ|Ẩ|Ẫ|Ă|Ằ|Ắ|Ặ|Ẳ|Ẵ/g, "A");
    str = str.replace(/È|É|Ẹ|Ẻ|Ẽ|Ê|Ề|Ế|Ệ|Ể|Ễ/g, "E");
    str = str.replace(/Ì|Í|Ị|Ỉ|Ĩ/g, "I");
    str = str.replace(/Ò|Ó|Ọ|Ỏ|Õ|Ô|Ồ|Ố|Ộ|Ổ|Ỗ|Ơ|Ờ|Ớ|Ợ|Ở|Ỡ/g, "O");
    str = str.replace(/Ù|Ú|Ụ|Ủ|Ũ|Ư|Ừ|Ứ|Ự|Ử|Ữ/g, "U");
    str = str.replace(/Ỳ|Ý|Ỵ|Ỷ|Ỹ/g, "Y");
    str = str.replace(/Đ/g, "D");
    return str;
}
//check mail
function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test($email);
}
//------------------------------------------------------------------//
//page product
ProductController.$inject = ["$scope", "$http"];
function ProductController($scope, $http) {
    $scope.parentCategory = [];
    function LoadSiteMenuCategory() {
        return $http({
            url: "/ProductCategory/GetCategoryByType",
            method: "GET",
            params: { type: 3 }
        }).then(function (response) {
            $scope.parentCategory = response.data;
        }, function (error) {
            alert('Error');
        });
    }
    //load menu cate
    LoadSiteMenuCategory();
}

//page shopping cart
ShoppingCartController.$inject = ["$scope", "$http"];
function ShoppingCartController($scope, $http) {
    $(document).ready(function () {
        var url = window.location.href;
        var id = url.substring(url.lastIndexOf('/') + 1);
        //function get color by id product
        function LoadColor(id, html) {
            $.ajax({
                url: "/api/productdetail/getlistproductbysizecolor",
                contentType: "application/json; charset=utf-8",
                type: "GET",
                dataType: "JSON",
                data: { id: id, type: 1 },
                async: false,
                success: function (data) {
                    var option = "<option value='0'>Choose an option</option>";
                    for (var i = 0; i < data.length; i++) {
                        option += "<option value='" + data[i].ColorID + "' >" + data[i].ColorName + "</option>";
                    }
                    $(html).html(option);
                }
            });
        }
        //function get size by id product
        function LoadSize(id, html) {
            $.ajax({
                url: "/api/productdetail/getlistproductbysizecolor",
                type: "GET",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                data: { id: id, type: 2 },
                async: false,
                success: function (data) {
                    var option = "<option value='0'>Choose an option</option>";
                    for (var i = 0; i < data.length; i++) {
                        option += "<option value='" + data[i].SizeID + "' >" + data[i].SizeName + "</option>";
                    }
                    $(html).html(option);
                }
            });
        }
        //binding value in select
        LoadColor(id, "#ddl_color");
        LoadSize(id, "#ddl_size");
        $('.img-zoom-details').zoom();
    });
}

//Product Sale
ProductSaleController.$inject = ["$scope", "$http"];
function ProductSaleController($scope, $http) {
    var count = 0;
    $scope.currentPage = 0;
    $scope.itemsPerPage = 8;
    $scope.productSales = [];
    $scope.listColor = [];
    $scope.LoadListSaleProduct = LoadListSaleProduct;
    //funtion load list sale product
    function LoadListSaleProduct() {
        return $http({
            method: 'GET',
            url: '/Product/ListProductDiscount',
            async: false,
        }).then(function (response) {
            $scope.productSales = response.data;
        });
    }
    //funtion load list color
    function LoadListColor() {
        $.ajax({
            url: "/Color/LoadListColor",
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                $scope.listColors = data;
            }
        });
    }
    //function search
    $("#txt_KeywordProductSale").blur(function () {
        var keyword = $(this).val();
        keyword = changeStamped(keyword);
        keyword = jQuery.trim(keyword);
        return $http({
            method: 'POST',
            url: '/Product/SearchProduct',
            async: false,
            data: { keyword: keyword }
        }).then(function (response) {
            $scope.productSales = response.data;
        });
    });
    //function find by color
    $scope.btnColor = function (index, event, colorID) {
        $("#sort-color > li > a").removeClass("filter-link-active");
        $(event.currentTarget).addClass("filter-link-active");
        var fromPrice = $("#sort-price > li > input[name='txt_checkPrice']").attr("data-from");
        var toPrice = $("#sort-price > li > input[name='txt_checkPrice']").attr("data-to");
        var categoryID = $(".treegrid-container").find("a[data-active='true']").attr("data-id");
        return $http({
            method: 'POST',
            url: '/Product/LoadListProduct',
            async: false,
            data: { colorID: colorID, fromPrice: fromPrice, toPrice: toPrice, categoryID: categoryID }
        }).then(function (response) {
            $scope.productSales = response.data;
        });
    };
    //function find by price
    $("#sort-price > li > input[name='txt_checkPrice']").click(function () {
        var fromPrice = $(this).attr("data-from");
        var toPrice = $(this).attr("data-to");
        var colorID = $("#sort-color > li").find(".filter-link-active").attr("data-id");
        var categoryID = $(".treegrid-container").find("a[data-active='true']").attr("data-id");
        if (colorID == undefined)
            colorID = 0;
        return $http({
            method: 'POST',
            url: '/Product/LoadListProduct',
            async: false,
            data: { colorID: colorID, fromPrice: fromPrice, toPrice: toPrice, categoryID: categoryID }
        }).then(function (response) {
            $scope.productSales = response.data;
        });
    });
    //call funtion load list sale product
    LoadListSaleProduct();
    //Load list color
    LoadListColor();
    //paging
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.range = function () {
            var rangeSize = 3;
            var ret = [];
            var start;

            start = $scope.currentPage;
            if (start > $scope.pageCount() - rangeSize) {
                start = $scope.pageCount() - rangeSize + 1;
                if (start < 0)
                    start = 0;
            }

            for (var i = start; i < start + rangeSize; i++) {
                ret.push(i);
            }
            return ret;
        };

        $scope.prevPage = function () {
            if ($scope.currentPage > 0) {
                $scope.currentPage--;
                $("html, body").animate({ scrollTop: 470 }, "slow");
            }
        };

        $scope.prevPageDisabled = function () {
            return $scope.currentPage == 0 ? "disabled" : "";
        };

        $scope.pageCount = function () {
            var count = Math.ceil($scope.productSales.length / $scope.itemsPerPage) - 1;
            return count;
        };

        $scope.nextPage = function () {
            if ($scope.currentPage < $scope.pageCount()) {
                $scope.currentPage++;
                $("html, body").animate({ scrollTop: 470 }, "slow");
            }
        };

        $scope.nextPageDisabled = function () {
            var countMax = $scope.pageCount();
            return $scope.currentPage == countMax ? "disabled" : "";
        };

        $scope.setPage = function (n) {
            $scope.currentPage = n;
            $("html, body").animate({ scrollTop: 470 }, "slow");
        };
    });
    $(document).ready(function () {
        //function click change parent
        $(".btnShowDetailsProduct").click(function () {
            $(".btnShowDetailsProduct").each(function () {
                $(this).attr("data-active", false);
            });
            $(this).attr("data-active", true);

            var id = $(this).attr("data-id");
            var parentID = $(this).attr("data-parent");
            if (parentID == undefined)
                parentID = 0;
            return $http({
                method: 'POST',
                url: '/Product/LoadListProductByCategory',
                async: false,
                data: { categories: id, parentID: parentID },
            }).then(function (response) {
                $scope.productSales = response.data;
            });
        });
    });
}

//New Product
NewProductController.$inject = ["$scope", "$http"];
function NewProductController($scope, $http) {
    var count = 0;
    $scope.currentPage = 0;
    $scope.itemsPerPage = 8;
    $scope.productNew = [];
    $scope.listColor = [];
    $scope.LoadListNewProduct = LoadListNewProduct;
    //funtion load list sale product
    function LoadListNewProduct() {
        return $http({
            method: 'GET',
            url: '/Product/ListNewProduct',
            async: false,
        }).then(function (response) {
            $scope.productNew = response.data;
        });
    }
    //funtion load list color
    function LoadListColor() {
        $.ajax({
            url: "/Color/LoadListColor",
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                $scope.listColors = data;
            }
        });
    }
    //function search
    $("#txt_KeywordProductNew").blur(function () {
        var keyword = $(this).val();
        keyword = changeStamped(keyword);
        keyword = jQuery.trim(keyword);
        return $http({
            method: 'POST',
            url: '/Product/SearchProduct',
            async: false,
            data: { keyword: keyword }
        }).then(function (response) {
            $scope.productNew = response.data;
        });
    });
    //function find by color
    $scope.btnColor = function (index, event, colorID) {
        $("#sort-color > li > a").removeClass("filter-link-active");
        $(event.currentTarget).addClass("filter-link-active");
        var fromPrice = $("#sort-price > li > input[name='txt_checkPrice']").attr("data-from");
        var toPrice = $("#sort-price > li > input[name='txt_checkPrice']").attr("data-to");
        var categoryID = $(".treegrid-container").find("a[data-active='true']").attr("data-id");
        return $http({
            method: 'POST',
            url: '/Product/LoadListProduct',
            async: false,
            data: { colorID: colorID, fromPrice: fromPrice, toPrice: toPrice, categoryID: categoryID }
        }).then(function (response) {
            $scope.productNew = response.data;
        });
    };
    //function find by price
    $("#sort-price > li > input[name='txt_checkPrice']").click(function () {
        var fromPrice = $(this).attr("data-from");
        var toPrice = $(this).attr("data-to");
        var colorID = $("#sort-color > li").find(".filter-link-active").attr("data-id");
        var categoryID = $(".treegrid-container").find("a[data-active='true']").attr("data-id");
        if (colorID == undefined)
            colorID = 0;
        return $http({
            method: 'POST',
            url: '/Product/LoadListProduct',
            async: false,
            data: { colorID: colorID, fromPrice: fromPrice, toPrice: toPrice, categoryID: categoryID }
        }).then(function (response) {
            $scope.productNew = response.data;
        });
    });
    //call funtion load list sale product
    LoadListNewProduct();
    //Load list color
    LoadListColor();
    //paging
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.range = function () {
            var rangeSize = 3;
            var ret = [];
            var start;

            start = $scope.currentPage;
            if (start > $scope.pageCount() - rangeSize) {
                start = $scope.pageCount() - rangeSize + 1;
                if (start < 0)
                    start = 0;
            }

            for (var i = start; i < start + rangeSize; i++) {
                ret.push(i);
            }
            return ret;
        };

        $scope.prevPage = function () {
            if ($scope.currentPage > 0) {
                $scope.currentPage--;
                $("html, body").animate({ scrollTop: 0 }, "slow");
            }
        };

        $scope.prevPageDisabled = function () {
            return $scope.currentPage == 0 ? "disabled" : "";
        };

        $scope.pageCount = function () {
            var count = Math.ceil($scope.productNew.length / $scope.itemsPerPage) - 1;
            return count;
        };

        $scope.nextPage = function () {
            if ($scope.currentPage < $scope.pageCount()) {
                $scope.currentPage++;
                $("html, body").animate({ scrollTop: 0 }, "slow");
            }
        };

        $scope.nextPageDisabled = function () {
            var countMax = $scope.pageCount();
            return $scope.currentPage == countMax ? "disabled" : "";
        };

        $scope.setPage = function (n) {
            $scope.currentPage = n;
            $("html, body").animate({ scrollTop: 0 }, "slow");
        };
    });
    $(document).ready(function () {
        //function click change parent
        $(".btnShowDetailsProduct").click(function () {
            $(".btnShowDetailsProduct").each(function () {
                $(this).attr("data-active", false);
            });
            $(this).attr("data-active", true);

            var id = $(this).attr("data-id");
            var parentID = $(this).attr("data-parent");
            if (parentID == undefined)
                parentID = 0;
            return $http({
                method: 'POST',
                url: '/Product/LoadListProductByCategory',
                async: false,
                data: { categories: id, parentID: parentID },
            }).then(function (response) {
                $scope.productNew = response.data;
            });
        });
    });
}

//Product Category
ProductCategoryController.$inject = ["$scope", "$http"];
function ProductCategoryController($scope, $http) {
    var count = 0;
    $scope.currentPage = 0;
    $scope.itemsPerPage = 8;
    $scope.productCategories = [];
    $scope.listColor = [];
    $scope.LoadListProductCategory = LoadListProductCategory;
    var id = $("#txt_CategoryID").val();
    var parentID = $("#txt_ParentID").val();
    //funtion load list sale product
    function LoadListProductCategory(categories, parentID) {
        return $http({
            method: 'POST',
            url: '/Product/LoadListProductByCategory',
            async: false,
            data: { categories: categories, parentID: parentID },
        }).then(function (response) {
            $scope.productCategories = response.data;
        });
    }
    //funtion load list color
    function LoadListColor() {
        $.ajax({
            url: "/Color/LoadListColor",
            type: "GET",
            dataType: "JSON",
            async: false,
            success: function (data) {
                $scope.listColors = data;
            }
        });
    }
    //function search
    $("#txt_KeywordProductCategory").blur(function () {
        var keyword = $(this).val();
        keyword = changeStamped(keyword);
        keyword = jQuery.trim(keyword);
        return $http({
            method: 'POST',
            url: '/Product/SearchProduct',
            async: false,
            data: { keyword: keyword }
        }).then(function (response) {
            $scope.productCategories = response.data;
        });
    });
    //function find by color
    $scope.btnColor = function (index, event, colorID) {
        $("#sort-color > li > a").removeClass("filter-link-active");
        $(event.currentTarget).addClass("filter-link-active");
        var fromPrice = $("#sort-price > li > input[name='txt_checkPrice']").attr("data-from");
        var toPrice = $("#sort-price > li > input[name='txt_checkPrice']").attr("data-to");
        var categoryID = $(".treegrid-container").find("a[data-active='true']").attr("data-id");
        return $http({
            method: 'POST',
            url: '/Product/LoadListProduct',
            async: false,
            data: { colorID: colorID, fromPrice: fromPrice, toPrice: toPrice, categoryID: categoryID }
        }).then(function (response) {
            $scope.productCategories = response.data;
        });
    };
    //function find by price
    $("#sort-price > li > input[name='txt_checkPrice']").click(function () {
        var fromPrice = $(this).attr("data-from");
        var toPrice = $(this).attr("data-to");
        var colorID = $("#sort-color > li").find(".filter-link-active").attr("data-id");
        var categoryID = $(".treegrid-container").find("a[data-active='true']").attr("data-id");
        if (colorID == undefined)
            colorID = 0;
        return $http({
            method: 'POST',
            url: '/Product/LoadListProduct',
            async: false,
            data: { colorID: colorID, fromPrice: fromPrice, toPrice: toPrice, categoryID: categoryID }
        }).then(function (response) {
            $scope.productCategories = response.data;
        });
    });
    //call funtion load list sale product
    LoadListProductCategory(id, parentID);
    //Load list color
    LoadListColor();
    //paging
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.range = function () {
            var rangeSize = 3;
            var ret = [];
            var start;

            start = $scope.currentPage;
            if (start > $scope.pageCount() - rangeSize) {
                start = $scope.pageCount() - rangeSize + 1;
                if (start < 0)
                    start = 0;
            }

            for (var i = start; i < start + rangeSize; i++) {
                ret.push(i);
            }
            return ret;
        };

        $scope.prevPage = function () {
            if ($scope.currentPage > 0) {
                $scope.currentPage--;
                $("html, body").animate({ scrollTop: 0 }, "slow");
            }
        };

        $scope.prevPageDisabled = function () {
            return $scope.currentPage == 0 ? "disabled" : "";
        };

        $scope.pageCount = function () {
            var count = Math.ceil($scope.productCategories.length / $scope.itemsPerPage) - 1;
            return count;
        };

        $scope.nextPage = function () {
            if ($scope.currentPage < $scope.pageCount()) {
                $scope.currentPage++;
                $("html, body").animate({ scrollTop: 0 }, "slow");
            }
        };

        $scope.nextPageDisabled = function () {
            var countMax = $scope.pageCount();
            return $scope.currentPage == countMax ? "disabled" : "";
        };

        $scope.setPage = function (n) {
            $scope.currentPage = n;
            $("html, body").animate({ scrollTop: 0 }, "slow");
        };
    });
    //
    $(document).ready(function () {
        //function click change parent
        $(".btnShowDetailsProduct").click(function () {
            $(".btnShowDetailsProduct").each(function () {
                $(this).attr("data-active", false);
            });
            $(this).attr("data-active", true);

            var id = $(this).attr("data-id");
            var parentID = $(this).attr("data-parent");
            if (parentID == undefined)
                parentID = 0;
            return $http({
                method: 'POST',
                url: '/Product/LoadListProductByCategory',
                async: false,
                data: { categories: id, parentID: parentID },
            }).then(function (response) {
                $scope.productCategories = response.data;
            });
        });
    });
}