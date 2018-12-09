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
app.controller("PostController", PostController);
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
//------------------------------------------------------------------//
//page product
ProductController.$inject = ["$scope", "$http"];
function ProductController($scope, $http) {
    var count = 0;
    $scope.products = [];
    $scope.parentCategory = [];
    //$scope.postCategories = [];
    $scope.btnDetails = btnDetails;
    //load menu category
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
    //function load list product
    function LoadProduct(categories, sortBy, sortPrice, sortColor, pageSize) {
        if (pageSize == 0)
            pageSize = 12;
        return $http({
            method: 'POST',
            url: '/Product/LoadListProduct',
            async: false,
            data: { categories: categories, sortBy: sortBy, sortPrice: sortPrice, sortColor: sortColor, pageSize: pageSize },
        }).then(function (response) {
            $scope.products = response.data;
            var len = response.data.length;
            var countSize = $("#txt_PageSize").val();
            if (countSize >= len)
                $("#txt_PageSize").val(len);
        });
    }
    //function load list details
    function btnDetails(id) {
        $.ajax({
            url: "/Product/Details",
            async: false,
            data: { id: id },
            success: function (data) {
                $("#view-details").html(data).show();
            },
            error: function (error) {
                alert(error);
            }
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
    //function Load list postcategories
    //function LoadPostCategories() {
    //    $.ajax({
    //        url: "/PostCategory/GetListPostCateogy",
    //        type: "GET",
    //        dataType: "JSON",
    //        async: false,
    //        success: function (data) {
    //            $scope.postCategories = data;
    //        }
    //    });
    //}
    //function click change parent
    $scope.btnParent = function (id) {
        var pageSize = $("#txt_PageSize").val();
        $("#txt_Category").val(id);
        pageSize = parseInt(pageSize);
        if (pageSize < 12)
            pageSize = 12;
        LoadProduct(id, 0, 0, 0, pageSize);
    };
    //function find by sort
    $("#sort-by > li > a").click(function () {
        $("#sort-by > li > a").removeClass("filter-link-active");
        $(this).addClass("filter-link-active");
    });
    //function find by price
    $("#sort-price > li > a").click(function () {
        $("#sort-price > li > a").removeClass("filter-link-active");
        $(this).addClass("filter-link-active");
    });
    //function find by color
    $scope.btnColor = function (index, event, id) {
        $("#sort-color > li > a").removeClass("filter-link-active");
        if (event == 0)
            $("#default-color").addClass("filter-link-active");
        else
            $(event.currentTarget).addClass("filter-link-active");
    };
    //function click search detail product
    $("#btnSearch").click(function () {
        var categories = $("button[name='btnParent']").attr("data-type");
        var sortBy = $("#sort-by > li").find(".filter-link-active").attr("data-row-1");
        var sortPrice = $("#sort-price > li").find(".filter-link-active").attr("data-row-2");
        var sortColor = $("#sort-color > li").find(".filter-link-active").attr("data-row-3");
        $("#txt_PageSize").val(12);
        var pageSize = $("#txt_PageSize").val();
        pageSize = parseInt(pageSize);
        if (categories == undefined)
            categories = 0;
        if (sortBy == undefined)
            sortBy = 0;
        if (sortPrice == undefined)
            sortPrice = 0;
        if (sortColor == undefined || sortColor == 0)
            sortColor = 0;
        LoadProduct(categories, sortBy, sortPrice, sortColor, pageSize);
    });
    //function cick more product
    $("#btnLoadMoreProduct").click(function () {
        var pageSize = $("#txt_PageSize").val();
        if (pageSize > 0)
            pageSize = parseInt(pageSize) + 8;
        else
            pageSize = 12;
        $("#txt_PageSize").val(pageSize);
        var category = $("#txt_Category").val();
        var sortBy = $("#sort-by > li").find(".filter-link-active").attr("data-row-1");
        var sortPrice = $("#sort-price > li").find(".filter-link-active").attr("data-row-2");
        var sortColor = $("#sort-color > li").find(".filter-link-active").attr("data-row-3");
        if (category == undefined)
            category = 0;
        if (sortBy == undefined)
            sortBy = 0;
        if (sortPrice == undefined)
            sortPrice = 0;
        if (sortColor == undefined)
            sortColor = 0;
        LoadProduct(category, sortBy, sortPrice, sortColor, pageSize);
    });
    //function search
    $("#txt_Keyword").blur(function () {
        var keyword = $(this).val();
        keyword = changeStamped(keyword);
        keyword = jQuery.trim(keyword);
        return $http({
            method: 'POST',
            url: '/Product/SearchProduct',
            async: false,
            data: { keyword: keyword }
        }).then(function (response) {
            $scope.products = response.data;
            var len = response.data.length;
            var countSize = $("#txt_PageSize").val();
            if (countSize >= len)
                $("#txt_PageSize").val(len);
        });
    });
    //load menu cate
    LoadSiteMenuCategory();
    //Load list color
    LoadListColor();
    //load postcategories
    //LoadPostCategories();
    //load list product
    LoadProduct(0, 0, 0, 0, 12);
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
    });
}

//Post
PostController.$inject = ["$scope", "$http"];
function PostController($scope, $http) {
    $scope.hotProductposts = [];
    $scope.newProductPosts = [];
    $scope.ListHotProduct = ListHotProduct;
    function ListHotProduct(top) {
        $.ajax({
            url: "/Product/ListHotProduct",
            type: "POST",
            dataType: "JSON",
            async: false,
            data: { top: top },
            success: function (data) {
                $scope.hotProductposts = data;
            }
        });
    }
    function ListNewProduct(take) {
        $.ajax({
            url: "/Product/ListNewProductByTake",
            type: "POST",
            dataType: "JSON",
            async: false,
            data: { take: take },
            success: function (data) {
                $scope.newProductPosts = data;
            }
        });
    }
    $("#btnSearchPost").click(function () {
        var keyword = $("#txt_KeywordPost").val();
        $.ajax({
            url: "/Post/SearchPost",
            type: "POST",
            dataType: "JSON",
            async: false,
            data: { keyword: keyword },
            success: function (response) {
                window.location = '/post-detail/' + response;
            }
        });
    });
    ListHotProduct(4);
    ListNewProduct(4);
}

//Product Sale
ProductSaleController.$inject = ["$scope", "$http"];
function ProductSaleController($scope, $http) {
    var count = 0;
    $scope.currentPage = 0;
    $scope.itemsPerPage = 8;
    $scope.productSales = [];
    $scope.parentCategory = [];
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
    //load menu category
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
    //function click change parent
    $scope.btnParent = function (id, parentID) {
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
    };
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
    //call funtion load list sale product
    LoadListSaleProduct();
    //load menu cate
    LoadSiteMenuCategory();
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
            var count = Math.ceil($scope.productSales.length / $scope.itemsPerPage) - 1;
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
}

//New Product
NewProductController.$inject = ["$scope", "$http"];
function NewProductController($scope, $http) {
    var count = 0;
    $scope.currentPage = 0;
    $scope.itemsPerPage = 8;
    $scope.productNew = [];
    $scope.parentCategory = [];
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
    //load menu category
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
    //function click change parent
    $scope.btnParent = function (id, parentID, index) {
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
    };
    //change child parent
    $scope.btnChild = function (id, parentID) {
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
    };
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
    //call funtion load list sale product
    LoadListNewProduct();
    //load menu cate
    LoadSiteMenuCategory();
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
}

//Product Category
ProductCategoryController.$inject = ["$scope", "$http"];
function ProductCategoryController($scope, $http) {
    var count = 0;
    $scope.currentPage = 0;
    $scope.itemsPerPage = 8;
    $scope.productCategories = [];
    $scope.parentCategory = [];
    $scope.LoadListProductCategory = LoadListProductCategory;
    var id = $("#txt_CategoryID").val();
    var parentID = $("#txt_ParentID").val();
    //load category parent by id
    function LoadCategoryByParent() {
        return $http({
            url: "/ProductCategory/GetCategoryByParent",
            method: "GET",
            params: { id: id }
        }).then(function (response) {
            $scope.childCategory = response.data;
        }, function (error) {
            alert('Error');
        });
    }
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
    //function click change parent
    $scope.btnParent = function (id, parentID) {
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
    };
    //
    LoadCategoryByParent();
    //call funtion load list sale product
    LoadListProductCategory(id, parentID);
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
}