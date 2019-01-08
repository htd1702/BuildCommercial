/// <reference path="../plugins/jquery/jquery.min.js" />
$(window).scroll(function () {
    //Menu
    var positionCurrent = $("html,body").scrollTop();
    //console.log(positionCurrent);

    //////Posion 320 Information
    //if (positionCurrent >= 100)
    //    $(".sec-banner").addClass("effectLeft");

    //////Posion 1040 New Product
    //if (positionCurrent >= 400)
    //    $(".section-main").addClass("effectLeft");
});

$("#btnContact").click(function () {
    var email = $("#txt_contactEmail").val();
    var des = $("#txt_contactDescription").val();
    var obj = {
        email: email,
        des: des
    };
    if (obj.email == undefined || obj.email == "") {
        swal("Faild!", "Please enter a email!", "error");
        return;
    }
    if (!validateEmail(obj.email)) {
        swal("Faild!", "Please check a email again!", "error");
        return;
    }
    if (obj.des == undefined || obj.des == "") {
        swal("Faild!", "Please enter a description!", "error");
        return;
    }
    swal({
        text: "Do you want send mail from to us?",
        icon: "success",
        buttons: true,
        dangerMode: true,
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                url: "/api/order/sendmailcontact",
                contentType: "application/json; charset=utf-8",
                type: "POST",
                dataType: "JSON",
                data: JSON.stringify(obj),
                beforeSend: function () { $(".loading-img-waiting-contact").addClass("loading").css("display", "block"); },
                complete: function () { $(".loading-img-waiting-contact").removeClass("loading").css("display", "none"); },
                success: function (data) {
                    swal("Success!", "Send mail success!", "success");
                },
                error: function (error) {
                    console.log(error);
                }
            });
        }
    });
});

$(document).ready(function () {
    //event load product page home
    var count = 0;
    if (count == 0) {
        var filterValue = ".new-clothes";
        $('.product-new-grid').isotope({ filter: filterValue });
        count++;
    }

    $('.filter-tope-group .btnParentNew').click(function (e) {
        if (count > 0) {
            var filterValue = $(this).attr('data-filter');
            $('.product-new-grid').isotope({ filter: filterValue });
        }
        e.preventDefault();
    });

    var btnParentNew = $('.filter-tope-group btnParentNew');
    $(btnParentNew).each(function () {
        $(this).on('click', function () {
            for (var i = 0; i < btnParentNew.length; i++) {
                $(btnParentNew[i]).removeClass('how-active1');
            }
            $(this).addClass('how-active1');
        });
    });
    $('.filter-tope-group .btnParentNew:nth-child(1)').addClass("how-active1");
    //end function load product by page home

    //call function show tree menu
    $('.tree').treegrid();
});