$(".btn-open-info").click(function () {
  $(this).parent().parent().toggleClass("active");
  $(this).parent().parent().next().slideToggle();
});

$(".btn-more").click(function () {
  $(".popup-options-wrapper").show();
  $(".overlay-popup-options").show();
});

$(".overlay-popup-options").click(function () {
  $(".overlay-popup-options, .popup-options-wrapper").hide();
});
