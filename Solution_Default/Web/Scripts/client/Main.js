$(function () {
    $(window).scroll(function () {
        //Menu
        var positionCurrent = $("html,body").scrollTop();
        console.log(positionCurrent);
        //if (positionCurrent > 80) {
        //    //$("nav#sectionsNav").addClass("navBarSroll");
        //    $(".navbar.navbar-transparent").addClass("menuSroll");
        //}
        //else {
        //    //$("nav#sectionsNav").removeClass("navBarSroll");
        //    $(".navbar.navbar-transparent").removeClass("menuSroll");
        //}

        ////Posion 320 Information
        if (positionCurrent >= 400)
            $("#section1").addClass("effectLeft");

        ////Posion 1040 New Product
        if (positionCurrent >= 1040) {
            $("#section2").addClass("effectLeft");
            $(".section2_Left").addClass("effectTop");
            $(".section2_Right").addClass("effectBottom");
        }

        ////Posion 2320 Top Product
        if (positionCurrent >= 2000) {
            $("#section3").addClass("effectRight");
        }

        ////Posion 3760 Sale Product
        if (positionCurrent >= 2800) {
            $("#section4").addClass("effectLeft");
        }

        ////Position 4400 News
        if (positionCurrent >= 3400)
        {
            $("#sectionContact").addClass("effectRight");
        }

        //if (positionCurrent >= 4940)
        //{
        //    $(".Contact").addClass("effectLeft");
        //}
    });
});

