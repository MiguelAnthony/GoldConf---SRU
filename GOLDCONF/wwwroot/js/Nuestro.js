$(function () {
    var a = $(".barra").innerHeight();
    $(window).scroll(function () {
        $(window).scrollTop() > 0 ?
            ($(".barra").addClass("fixed"),
                $("body").css({ "margin-top": a + "px" })) : ($(".barra").removeClass("fixed"),
                    $("body").css({ "margin-top": "0px" }))
    });
    $(".menu-movil").on("click", function () {
        $(".navegacion-principal").slideToggle()
    });
    $(window).resize(function () {
        $(document).width() >= 768 ?
            $(".navegacion-principal").show() :
            $(".navegacion-principal").hide()
    })
});

$(function () {
    $(".programa-evento .info-curso:first").show(),
        $(".menu-programa a:first").addClass("activo"),
        $(".menu-programa a").on("click", function () {
            $(".menu-programa a").removeClass("activo"),
                $(this).addClass("activo"), $(".ocultar").hide();
            var n = $(this).attr("href");
            return $(n).fadeIn(1e3), !1
        })
});