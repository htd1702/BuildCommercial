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
            swal("Thất bại!", "Vui lòng chọn màu sắc khi thêm vào giỏ!", "error");
            return;
        }
        if (sizeID == 0) {
            swal("Thất bại!", "Vui lòng chọn kích thước khi thêm vào giỏ!", "error");
            return;
        }
        //call funtion load count and total
        $.ajax({
            url: "/Cart/Add",
            data: { id: pid, colorID: colorID, sizeID: sizeID, lang: lang },
            type: "post",
            async: false,
            success: function (response) {
                $(".shopping-cart-des").attr("data-notify", response.Count);
                $(".shopping-cart-mobi").attr("data-notify", response.Count);
                swal("Thành công", "Thêm vào giỏ hàng thành công!", "success");
            }
        });
    });
    //function up cart
    $(".btn-num-product-up").click(function () {
        //get attribute
        var pid = $(this).attr("data-update-cart");
        var qty = $(this).parents("tr").find("input[name='number-pro']").val();
        var lang = $("#cookieLang").val();
        //call funtion load count and total
        $.ajax({
            url: "/Cart/Update",
            data: { id: pid, newqty: qty, lang: lang },
            type: "post",
            success: function (response) {
                //set value atti in count and total
                $(".shopping-cart-des").attr("data-notify", response.Count);
                $(".shopping-cart-mobi").attr("data-notify", response.Count);
                //cap nhat thanh tien cua san pham
                $("#spanTotal").html(response.Total);
            }
        });
    });
    //funtion down cart
    $(".btn-num-product-down").click(function () {
        //get attribute
        var pid = $(this).attr("data-update-cart");
        var qty = $(this).parents("tr").find("input[name='number-pro']").val();
        var tr = $(this).parents("tr");
        //check count = 0 => remove product cart
        if (qty == 0) {
            if (confirm("Bạn có muốn xóa sản phẩm này ra khỏi giỏ hàng không?")) {
                $.ajax({
                    url: "/Cart/Remove",
                    data: { id: pid },
                    type: "post",
                    async: false,
                    success: function (response) {
                        $(".shopping-cart-des").attr("data-notify", response.Count);
                        $(".shopping-cart-mobi").attr("data-notify", response.Count);
                        $(tr).remove();
                        $("#spanTotal").html(response.Total);
                    }
                });
                $.ajax({
                    url: "/Cart/Index",
                    async: false,
                    success: function (response) {
                        $("#page_ShoppingCart").html(response);
                    }
                });
            }
        }
        //check count #0 return list cart
        else {
            $.ajax({
                url: "/Cart/Update",
                data: { id: pid, newqty: qty },
                type: "post",
                success: function (response) {
                    $(".shopping-cart-des").attr("data-notify", response.Count);
                    $(".shopping-cart-mobi").attr("data-notify", response.Count);
                    //cap nhat thanh tien cua san pham
                    $("#spanTotal").html(response.Total);
                }
            });
        }
    });
});