$('.show_popup').click(function () {
    $('.popup-form-wrapper').show();
    $('.overlay-popup-form').show();
})

$('.overlay-popup-form').click(function () {
    $('.overlay-popup-form, .popup-form-wrapper').hide();
})