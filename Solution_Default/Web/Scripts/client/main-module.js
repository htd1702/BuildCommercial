/// <reference path="../plugins/angular/angular.min.js" />
var app = angular.module("app_client", []);
app.controller("ProductController", ProductController);
app.controller("ProductCategoryController", ProductCategoryController);
app.controller("ShoppingCartController", ShoppingCartController);

ProductController.$inject = ["$scope", "$http"];
function ProductController($scope, $http) {
    $scope.products = [];
    $scope.btnDetails = btnDetails;
    //function load list product
    function LoadProduct(categories, sortBy, sortPrice, sortColor, pageSize) {
        if (pageSize == 0)
            pageSize = 8;
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
            url: "/Client/Details",
            async: false,
            data: { id: id },
            success: function (data) {
                $("#view-details").html(data).show();
            },
            error: function (error) {
            }
        });
    }
    //function load list parent
    function LoadListParent() {
        $.ajax({
            url: "/ProductCategory/GetCategoryByType",
            type: "GET",
            async: false,
            data: { type: 1 },
            success: function (data) {
                var obj = { ID: 0, Name: "All Product" };
                var array = [];
                array.push(obj);
                for (var i = 0; i < data.length; i++) {
                    array.push(data[i]);
                }
                $scope.parents = array;
            },
            error: function (error) {
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
    //function click change parent
    $scope.btnParent = function (id) {
        var pageSize = $("#txt_PageSize").val();
        pageSize = parseInt(pageSize);
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
        $(event.currentTarget).addClass("filter-link-active");
    };
    //function click search detail product
    $("#btnSearch").click(function () {
        var categories = $("button[name='btnParent']").attr("data-type");
        var sortBy = $("#sort-by > li").find(".filter-link-active").attr("data-row-1");
        var sortPrice = $("#sort-price > li").find(".filter-link-active").attr("data-row-2");
        var sortColor = $("#sort-color > li").find(".filter-link-active").attr("data-row-3");
        var pageSize = $("#txt_PageSize").val();
        pageSize = parseInt(pageSize);
        if (categories == undefined)
            categories = 0;
        if (sortBy == undefined)
            sortBy = 0;
        if (sortPrice == undefined)
            sortPrice = 0;
        if (sortColor == undefined)
            sortColor = 0;
        LoadProduct(categories, sortBy, sortPrice, sortColor, pageSize);
    });
    //function cick more product
    $("#btnLoadMoreProduct").click(function () {
        var pageSize = $("#txt_PageSize").val();
        pageSize = parseInt(pageSize) + 8;
        $("#txt_PageSize").val(pageSize);
        var categories = $("button[name='btnParent']").attr("data-type");
        var sortBy = $("#sort-by > li").find(".filter-link-active").attr("data-row-1");
        var sortPrice = $("#sort-price > li").find(".filter-link-active").attr("data-row-2");
        var sortColor = $("#sort-color > li").find(".filter-link-active").attr("data-row-3");
        if (categories == undefined)
            categories = 0;
        if (sortBy == undefined)
            sortBy = 0;
        if (sortPrice == undefined)
            sortPrice = 0;
        if (sortColor == undefined)
            sortColor = 0;
        LoadProduct(categories, sortBy, sortPrice, sortColor, pageSize);
    });
    //load list parent
    LoadListParent();
    //Load list color
    LoadListColor();
    //load list product
    LoadProduct(0, 0, 0, 0, 8);
}

ProductCategoryController.$inject = ["$scope", "$http"];
function ProductCategoryController($scope, $http) {
    LoadCategoryByTake(3);
    function LoadCategoryByTake(take) {
        $.ajax({
            url: "/ProductCategory/GetCategoryByTake",
            type: "GET",
            dataType: "JSON",
            async: false,
            data: { take: take },
            success: function (data) {
                $scope.categories = data;
            },
            error: function (error) {
            }
        });
    }
}

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