// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function getToken() {

    return document.getElementsByTagName("__RequestVerificationToken").value;

}

function ButtonLoaderShow(selector) {

    $(selector).append("<img src='/assets/images/loader.gif' style='height: 25px; width: 25px;margin-left: 10px;'/>");

}

function ButtonLoaderHide(selector) {

    $(selector + " img").remove();

}

function LoaderOneShow(selector) {

    $(selector).html("<div style='text-align: center'><img src='/assets/images/loader.gif'/> </div>");

}

function LoaderOneHide(selector) {

    $(selector).html("");

}