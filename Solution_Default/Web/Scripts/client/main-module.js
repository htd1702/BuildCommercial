/// <reference path="../plugins/angular/angular.min.js" />
var app = angular.module("app_client", []);
app.controller("ProductController", ProductController);
app.controller("ProductCategoryController", ProductCategoryController);

ProductController.$inject = ["$scope", "$http"];
function ProductController($scope, $http) {
    $scope.btnDetails = btnDetails;

    $(".btn-category").click(function () {
        var a = $(this).attr("data-type");
        alert(a);
    });

    $("#sort-by > li > a").click(function () {
        var a = $(this).attr("data-row-1");
        alert(a);
    });

    $("#sort-price > li > a").click(function () {
        var a = $(this).attr("data-row-2");
        alert(a);
    });

    $("#sort-color > li > a").click(function () {
        var a = $(this).attr("data-row-3");
        alert(a);
    });

    LoadProduct();
    function LoadProduct() {
        var categories = $(".btn-category").attr("data-type");
        var sortBy = $("#sort-by > li > a").attr("data-row-1");
        var sortPrice = $("#sort-price > li > a").attr("data-row-2");
        var sortColor = $("#sort-color > li > a").attr("data-row-3");
        $.ajax({
            url: "/Product/LoadListProduct",
            type: "GET",
            dataType: "JSON",
            async: false,
            data: { categories: categories, sortBy: sortBy, sortPrice: sortPrice, sortColor: sortColor },
            success: function (data) {
                $scope.products = data;
            },
            error: function (error) {
            }
        });
    }

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
