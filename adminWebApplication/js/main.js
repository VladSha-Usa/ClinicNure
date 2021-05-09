$('.btn-open-info').click(function () {
    $(this).parent().toggleClass('active');
    $(this).parent().next().slideToggle();
});

$('.show_popup').click(function () {
    $('.popup-form-wrapper').show();
    $('.overlay-popup-form').show();
})

$('.overlay-popup-form').click(function () {
    $('.overlay-popup-form, .popup-form-wrapper').hide();
})