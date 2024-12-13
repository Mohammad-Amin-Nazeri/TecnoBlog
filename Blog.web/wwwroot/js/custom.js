$(document).ready(function () {
    LoadCkEditor4();

    $.ajax({
        url: "/index/PopularPost",
        type: "get"
    }).done(function (data) {
        $("#Popular_posts").html(data);
    });

    $.ajax({
        url: "/index/PopularPostPage",
        type: "get"
    }).done(function (data) {
        $("#Popular_posts_page").html(data);
    });
});

function LoadCkEditor4() {
    console.log("LoadCkEditor");

    if (!document.getElementById("ckEditor4"))
        return;
    console.log("LoadCkEditor42");

    $("body").append("<script src='/ckeditor4/ckeditor/ckeditor.js'></script>");

    CKEDITOR.replace('ckEditor4',
        {
            customConfig: '/ckeditor4/ckeditor/config.js'
        });
}
function changePage(pageId) {
    var url = new URL(window.location.href);
    var search_params = url.searchParams;

    // Change PageId
    search_params.set('pageId', pageId);
    url.search = search_params.toString();

    // the new url string
    var new_url = url.toString();

    window.location.replace(new_url);
}
