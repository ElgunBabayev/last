$(document).ready(function () {
    // "use strict";
    // jQuery(window).on("load", function () {
    //   $(".preloader").delay(1000).fadeOut("slow");
    // });

    updateNav();
    $(window).on("scroll", function () {
        updateNav();
    });

    function updateNav() {
        if ($(window).scrollTop()) {
            $(".navbar").addClass("active");
        } else {
            $(".navbar").removeClass("active");
        }
    }

    $(".owl-carousel").owlCarousel({
        loop: true,
        autoplay: true,
        autoplayTimeout: 5000,
        animateOut: "fadeOut",
        items: 1,
        dots: 0,
    });
    $(".btn-s li").on("click", function () {
        $(".group-wrapper").addClass("d-none");
        var selector = $(this).data("filter");
        $("." + selector).removeClass("d-none");
    });

    $("[data-countdown]").each(function () {
        const $deadline = new Date($(this).data("countdown")).getTime();

        var $dataDays = $(this).children("[data-days]");
        var $dataHours = $(this).children("[data-hours]");
        var $dataMinuts = $(this).children("[data-minuts]");
        var $dataSeconds = $(this).children("[data-seconds]");

        var x = setInterval(function () {
            var now = new Date().getTime();
            var t = $deadline - now;

            var days = Math.floor(t / (1000 * 60 * 60 * 24));
            var hours = Math.floor((t % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minuts = Math.floor((t % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((t % (1000 * 60)) / 1000);

            if (days < 10) {
                days = "0" + days;
            }
            if (days <= 0) {
                $dataDays.addClass("d-none")
            }

            if (hours < 10) {
                hours = "0" + hours;
            }
            if (hours <= 0) {
                $dataHours.addClass("d-none")
            }

            if (minuts < 10) {
                minuts = "0" + minuts;
            }
           
            if (seconds < 10) {
                seconds = "0" + seconds;
            }
           

            $dataDays.html(days + "d:");
            $dataHours.html(hours + "h:");
            $dataMinuts.html(minuts + "m:");
            $dataSeconds.html(seconds + "s");

            if (t <= 0) {
                clearInterval(x);

                //$dataDays.html("EXPIRED");
                //$dataHours.addClass("d-none");
                //$dataMinuts.addClass("d-none");
                //$dataSeconds.addClass("d-none");
                $dataDays.html('00');
                $dataHours.html('00');
                $dataMinuts.html('00');
                $dataSeconds.html('00');
            }
        }, 1000);
    });

    $(".add-img").mouseenter(function () {
        $("#uploadBtn").addClass("d-block");
    });
    $(".add-img").mouseleave(function () {
        $("#uploadBtn").removeClass("d-block");
    });

    $('.dropdown').click(function () {
        $(this).attr('tabindex', 1).focus();
        $(this).toggleClass('active');
        $(this).find('.dropdown-menu').slideToggle(300);
    });
    $('.dropdown').focusout(function () {
        $(this).removeClass('active');
        $(this).find('.dropdown-menu').slideUp(300);
    });
    $('.dropdown .dropdown-menu li').click(function () {
        $(this).parents('.dropdown').find('span').text($(this).text());
        $(this).parents('.dropdown').find('input').attr('value', $(this).attr('id'));
    });
    $('.dropdown-menu li').on("click", function () {
        var $type = $(this).data("cat-source");
        if ($type == "all") {
            $('.portfolio-block').fadeOut(0);
            $('.portfolio-block').fadeIn(1000);
        } else {
            $('.portfolio-block').hide();
            $('#' + $type + ".portfolio-block").fadeIn(1000);
        }
    });
    $(".dashboard").on("click", function () {
        $(".group-wrapper").addClass("d-none");
        var selector = $(this).data("filter");
        $("." + selector).removeClass("d-none");
    });
    $(".add-prodct").on("click", function () {
        $(".add-prod-modal").removeClass("d-none");
    });
    $(".close-add-prod-modal").click(function (e) {
        e.preventDefault();
        $(".add-prod-modal").addClass("d-none");
    });
});
