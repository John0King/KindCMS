// for tooltip 
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

// for resize fix siderbar problem
$(window).resize(function () {
    try{
        var slid = $("#SideBar");
        slid.removeClass("collapse");
        slid.removeClass("in");
        slid.removeAttr("aria-expanded");
        slid.css("height", "");

    }
    catch (e) {}
});