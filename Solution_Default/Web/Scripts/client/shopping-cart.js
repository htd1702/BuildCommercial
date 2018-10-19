$(function () {
    //add shopping cart
    $(".js-addcart-detail").click(function () {
        var pid = $(this).attr("data-add-to-cart");
        $.ajax({
            url: "/Cart/Add",
            data: { id: pid },
            type: "post",
            async: false,
            success: function (response) {
                $(".shopping-cart-des").attr("data-notify", response.Count);
                $(".shopping-cart-mobi").attr("data-notify", response.Count);
            }
        });
    });

    $(".btn-num-product-up").click(function () {
        var pid = $(this).attr("data-update-cart");
        var qty = $(this).parents("tr").find("input[name='number-pro']").val();
        var tr = $(this).parents("tr");
        $.ajax({
            url: "/Cart/Update",
            data: { id: pid, newqty: qty },
            type: "post",
            success: function (response) {
                $(".shopping-cart-des").attr("data-notify", response.Count);
                $(".shopping-cart-mobi").attr("data-notify", response.Count);
                //cap nhat thanh tien cua san pham
                tr.find("td:eq(5)").html(response.Total);
            }
        });
    });

    $(".btn-num-product-down").click(function () {
        var pid = $(this).attr("data-update-cart");
        var qty = $(this).parents("tr").find("input[name='number-pro']").val();
        var tr = $(this).parents("tr");
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
        else {
            $.ajax({
                url: "/Cart/Update",
                data: { id: pid, newqty: qty },
                type: "post",
                success: function (response) {
                    $(".shopping-cart-des").attr("data-notify", response.Count);
                    $(".shopping-cart-mobi").attr("data-notify", response.Count);
                    //cap nhat thanh tien cua san pham
                    tr.find("td:eq(5)").html(response.Total);
                }
            });
        }
    });
});