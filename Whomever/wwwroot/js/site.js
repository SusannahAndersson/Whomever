// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    var x = 0;
    var s = "";

    console.log("Testing");

    var theForm = $("#theForm");
    theForm.hide();

    var button = $("#addtocartButton");
    button.on("click", function () {
        console.log("Added to cart");
    });

    var productInfo = $(".products-prop li");
    productInfo.on("click", function () {
        console.log($(this).text());
    });

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.slideToggle(100);
    });
});