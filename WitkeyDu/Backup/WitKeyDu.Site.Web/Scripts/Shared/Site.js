$(function () {
    $(window).on('scroll', function () {
        if ($(window).scrollTop() > 100) {
            $('.navbar').removeClass('NavigationLarge').addClass('NavigationSmall');
        } else {
            $('.navbar').removeClass('NavigationSmall').addClass('NavigationLarge');
        }
    });
});