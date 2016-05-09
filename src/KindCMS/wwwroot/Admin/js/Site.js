// Write your Javascript code.
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});
$(window).resize(function () {
    try{
        var slid = $("#SideBar");
        slid.removeClass("collapse");
        slid.removeClass("in");
        slid.removeAttr("aria-expanded");
        slid.css("height", "");

    }
    catch(e){}
});