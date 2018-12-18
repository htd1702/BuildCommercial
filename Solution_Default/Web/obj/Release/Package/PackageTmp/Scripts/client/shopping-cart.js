$(function () {

    //add shopping cart
    $(".js-addcart-detail").click(function () {
        //get attribute
        var pid = $(this).attr("data-add-to-cart");
        var colorID = $("#ddl_color option:selected").val();
        var sizeID = $("#ddl_size option:selected").val();
        var lang = $("#cookieLang").val();
        //check size and code is not null
        if (colorID == 0) {
            swal("Faild!", "Please select color when adding to cart!", "error");
            return;
        }
        if (sizeID == 0) {
            swal("Faild!", "Please select size when adding to cart!", "error");
            return;
        }
        //function check quantity
        $.ajax({
            url: "/Product/CheckQuantityProduct",
            data: { colorID: colorID, sizeID: sizeID },
            type: "post",
            async: false,
            success: function (response) {
                if (response > 0) {
                    //call funtion load count and total
                    $.ajax({
                        url: "/Cart/Add",
                        data: { id: pid, colorID: colorID, sizeID: sizeID, lang: lang },
                        type: "post",
                        async: false,
                        success: function (response) {
                            $(".shopping-cart-des").attr("data-notify", response.Count);
                            $(".shopping-cart-mobi").attr("data-notify", response.Count);
                            swal("Success", "Add to cart successfully!", "success");
                        }
                    });
                }
                else {
                    swal("Faild!", "The color and size of this product is out of stock. Please select another size or color!", "error");
                }
            }
        });
    });
    //check out details by id
    $(".js-checkout-detail").click(function () {
        //get attribute
        var pid = $(this).attr("data-add-to-cart");
        var colorID = $("#ddl_color option:selected").val();
        var sizeID = $("#ddl_size option:selected").val();
        var lang = $("#cookieLang").val();
        if ((colorID != undefined || colorID != 0) && (sizeID != undefined || sizeID != 0)) {
            //call funtion load count and total
            //function check quantity
            $.ajax({
                url: "/Product/CheckQuantityProduct",
                data: { colorID: colorID, sizeID: sizeID },
                type: "post",
                async: false,
                success: function (response) {
                    if (response > 0) {
                        //call funtion load count and total
                        $.ajax({
                            url: "/Cart/Add",
                            data: { id: pid, colorID: colorID, sizeID: sizeID, lang: lang },
                            type: "post",
                            async: false,
                            success: function (response) {
                                $(".shopping-cart-des").attr("data-notify", response.Count);
                                $(".shopping-cart-mobi").attr("data-notify", response.Count);
                            }
                        });
                    }
                    else {
                        swal("Faild!", "The color and size of this product is out of stock. Please select another size or color!", "error");
                    }
                }
            });
        }
        window.location.href = '/check-out';
    });
    //function up cart
    $(".btn-num-product-up").click(function () {
        //get attribute
        var pid = $(this).attr("data-update-cart");
        var colorID = $(this).attr("data-color");
        var sizeID = $(this).attr("data-size");
        var qty = $(this).parents("tr").find("input[name='number-pro']").val();
        var lang = $("#cookieLang").val();
        //check inventory
        $.ajax({
            url: "/Product/InventoryByProductDetails",
            data: { colorID: colorID, sizeID: sizeID },
            type: "post",
            async: false,
            success: function (response) {
                if (qty <= response) {
                    //call funtion load count and total
                    $.ajax({
                        url: "/Cart/Update",
                        data: { id: pid, newqty: qty, lang: lang, colorID: colorID, sizeID: sizeID },
                        type: "post",
                        success: function (response) {
                            //set value atti in count and total
                            $(".shopping-cart-des").attr("data-notify", response.Count);
                            $(".shopping-cart-mobi").attr("data-notify", response.Count);
                            //cap nhat thanh tien cua san pham
                            if (lang == "en" || lang == "")
                                $("#spanTotal").html("$" + response.Total);
                            else
                                $("#spanTotal").html(response.Total);
                        }
                    });
                }
                else {
                    swal("Faild!", "Maximum number of item is " + response + ", please check again!", "error");
                    $(".num-product").val(response);
                }
            }
        });
    });
    //funtion down cart
    $(".btn-num-product-down").click(function () {
        //get attribute
        var pid = $(this).attr("data-update-cart");
        var colorID = $(this).attr("data-color");
        var sizeID = $(this).attr("data-size");
        var qty = $(this).parents("tr").find("input[name='number-pro']").val();
        var tr = $(this).parents("tr");
        var lang = $("#cookieLang").val();
        //check count = 0 => remove product cart
        if (qty == 0) {
            swal({
                title: "Are you sure?",
                text: "Do you want to remove this product from your shopping cart?",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            }).then((willDelete) => {
                if (willDelete) {
                    swal("Success", {
                        icon: "success",
                    }).then(function () {
                        $.ajax({
                            url: "/Cart/Remove",
                            data: { id: pid, colorID: colorID, sizeID: sizeID },
                            type: "post",
                            async: false,
                            success: function (response) {
                                $(".shopping-cart-des").attr("data-notify", response.Count);
                                $(".shopping-cart-mobi").attr("data-notify", response.Count);
                                $(tr).remove();
                                if (lang == "en" || lang == "")
                                    $("#spanTotal").html("$" + response.Total);
                                else
                                    $("#spanTotal").html(response.Total);
                            }
                        });
                        $.ajax({
                            url: "/Cart/ViewCartDetails",
                            async: false,
                            success: function (response) {
                                $("#page_ShoppingCart").html(response);
                            }
                        });
                    });
                }
            });
        }
        //check count #0 return list cart
        else {
            $.ajax({
                url: "/Cart/Update",
                data: { id: pid, newqty: qty, lang: lang, colorID: colorID, sizeID: sizeID },
                type: "post",
                success: function (response) {
                    $(".shopping-cart-des").attr("data-notify", response.Count);
                    $(".shopping-cart-mobi").attr("data-notify", response.Count);
                    //cap nhat thanh tien cua san pham
                    if (lang == "en" || lang == "")
                        $("#spanTotal").html("$" + response.Total);
                    else
                        $("#spanTotal").html(response.Total);
                }
            });
        }
    });
    //function
    $(".num-product").change(function () {
        //get attribute
        var pid = $(this).attr("data-update-cart");
        var colorID = $(this).attr("data-color");
        var sizeID = $(this).attr("data-size");
        var qty = $(this).parents("tr").find("input[name='number-pro']").val();
        var lang = $("#cookieLang").val();
        //check inventory
        $.ajax({
            url: "/Product/InventoryByProductDetails",
            data: { colorID: colorID, sizeID: sizeID },
            type: "post",
            async: false,
            success: function (response) {
                if (qty <= response) {
                    //call funtion load count and total
                    $.ajax({
                        url: "/Cart/Update",
                        data: { id: pid, newqty: qty, lang: lang, colorID: colorID, sizeID: sizeID },
                        type: "post",
                        success: function (response) {
                            //set value atti in count and total
                            $(".shopping-cart-des").attr("data-notify", response.Count);
                            $(".shopping-cart-mobi").attr("data-notify", response.Count);
                            //cap nhat thanh tien cua san pham
                            if (lang == "en" || lang == "")
                                $("#spanTotal").html("$" + response.Total);
                            else
                                $("#spanTotal").html(response.Total);
                        }
                    });
                }
                else {
                    swal("Faild!", "Maximum number of item is " + response + ", please check again!", "error");
                    $(".num-product").val(response);
                }
            }
        });
    });
});