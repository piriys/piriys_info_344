
function ajaxFixupLinks() {
    $('a[data-ajax="true"]').click(function (ev) {
        var id = $(ev.srcElement).attr('data-ajax-update');
        var url = $(ev.srcElement).attr('href');

        $(id).load(url, function (ev2) {
            ajaxFixupLinks();
        });


        //console.dir(ev);
        return false;
    });
}




$(window).load(function () {
    ajaxFixupLinks();
});

