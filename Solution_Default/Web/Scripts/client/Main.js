$(function () {
    $(window).scroll(function () {
        //Menu
        var positionCurrent = $("html,body").scrollTop();
        console.log(positionCurrent);
        if (positionCurrent > 80) {
            //$("nav#sectionsNav").addClass("navBarSroll");
            $(".navbar.navbar-transparent").addClass("menuSroll");
        }
        else {
            //$("nav#sectionsNav").removeClass("navBarSroll");
            $(".navbar.navbar-transparent").removeClass("menuSroll");
        }

        ////Posion 320 Information
        //if (positionCurrent >= 320)
        //    $(".Information").addClass("effectLeft");

        ////Posion 1040 New Product
        //if (positionCurrent >= 1040)
        //{
        //    $(".newProductEffectLeft").addClass("effectLeft");
        //    $(".newProductEffectTop").addClass("effectBottom");
        //    $(".newProductEffectRight").addClass("effectRight");
        //}

        ////Posion 2320 Top Product
        //if (positionCurrent >= 1900) {
        //    $(".topProEffectLeft").addClass("effectLeft");
        //    $(".topProEffectRight").addClass("effectBottom");
        //}

        ////Posion 3760 Sale Product
        //if (positionCurrent >= 2400)
        //{
        //    $(".saleProductEffectLeft").addClass("effectRight");
        //    $(".saleProductEffectRight").addClass("effectBottom");
        //}

        ////Position 4400 News
        //if (positionCurrent >= 4000)
        //{
        //    $(".NewsProduct").addClass("effectBottom");
        //}

        //if (positionCurrent >= 4940)
        //{
        //    $(".Contact").addClass("effectLeft");
        //}
    });
});

