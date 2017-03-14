var current = 0;

function changeAllColor(colorToRemove, colorToAdd, type, selector) {
    if (type == 'color') {
        var colorToRemoveClass = "color-" + colorToRemove;
        var colorToAddClass = "color-" + colorToAdd;

        $(selector).removeClass(colorToRemoveClass);
        $(selector).addClass(colorToAddClass);
    }

    if (type == 'text') {
        var textToRemoveClass = "text-" + colorToRemove;
        var textToAddClass = "text-" + colorToAdd;

        $(selector).removeClass(textToRemoveClass);
        $(selector).addClass(textToAddClass);
    }   
}

function changeThemeColor_1(color) {
    var colorClass = "color-" + color;
    $('.metro-color-1').removeClass(
		function (index, css) {
		    return (css.match(/\bcolor-\S+/g) || []).join(' ');
		}
	);

    $('.metro-color-1').addClass(colorClass);
}

function changeThemeColor_2(color) {
    var colorClass = "color-" + color;
    $('.metro-color-2').removeClass(
		function (index, css) {
		    return (css.match(/\bcolor-\S+/g) || []).join(' ');
		}
	);

    $('.metro-color-2').addClass(colorClass);
}

function toggleToInactive(className) {
    $(className).addClass('state-inactive');
}

function toggleToActive(className) {
    $(className).removeClass('state-inactive');
}

function nextPage() {
    var pagecount = $('.page').length;
    if (current < pagecount - 1) {
        current++;
        var pageName = '#page-' + current;

        toggleToInactive('.page');
        toggleToActive(pageName);
    }
}

function prevPage() {
    var pagecount = $('.page').length;
    if (current > 0) {
        toggleToInactive('.page');
        current--;

        var pageName = '#page-' + current;
        toggleToActive(pageName);
    }
}

function jumpToPage(pageNumber) {
    toggleToInactive('.page');
    current = pageNumber;

    var pageName = '#page-' + pageNumber;
    toggleToActive(pageName);
}

function closeDialog() {
    toggleToInactive('#dark-overlay');

    $('#dialog-content_Loader').empty();
}


/*Custom*/

function loadIntoSlideOnlyView(viewName, slideId) {
    $.ajax({
        url: "/Shared/_Slide",
        data: { view: viewName, id: slideId },
        success: function (data) {
            $('#slide-Expanded_Loader').html(data);
        }
    });

    $.ajax({
        url: "/Presentation/PresentationSlides/",
        data: { id: slideId },
        success: function (data) {
            presentationSlides = data;
        }
    });

}

function loadCreateSlideDialog(id) {
    var elem = $('#dialog-content_Loader');
    elem.empty();

    elem.load('/Slide/CreateDlg/' + id, function () {
        toggleToActive('#dark-overlay');
    });
}

function loadDeleteSlideDialog(id, view) {
    var elem = $('#dialog-content_Loader');
    elem.empty();

    elem.load('/Slide/DeleteDlg/?id=' + id + "&view=" + view, function () {
        toggleToActive('#dark-overlay');
    });
}

function loadCreatePresentationDialog() {
    var elem = $('#dialog-content_Loader');
    elem.empty();

    elem.load('/Presentation/CreateDlg', function () {
        toggleToActive('#dark-overlay');
    });
}

function loadDeletePresentationDialog(id, view) {
    var elem = $('#dialog-content_Loader');
    elem.empty();

    elem.load('/Presentation/DeleteDlg/?id=' + id + "&view=" + view, function () {
        toggleToActive('#dark-overlay');
    });
}

function createSlide(id, view) {
    console.log('createSlide');
        
    $.ajax({
        url: "/Presentation/Slide/",
        data: { id: id, Title: "New Slide" },
        type: "POST",
        success: function () {

            refreshQueuedSlides(id, view);
        },
        error: function () {
            alert('error creating slide');
        }
    });
}

function editSlide(slideId, target, callback) {
    saveIfChanged(target);
    $.get('/Slide/GetPresentationSlide/' + slideId, function (result) {
        $(target).html(result.HtmlContentString);
        console.dir(result);
        $(target).aloha();
    });

    /*
    $(target).load("/Slide/RawSlide/" + slideId, function (ev2) {
    oldHtml = $(target).html();
    if (callback !== undefined) {
    callback(slideId, target);
    }
    });
    */
}

function refreshSlide(viewName, slideID) {
    if (viewName == "Present") {
        $.ajax({
            url: "/Shared/_Slide",
            data: { view: viewName, id: slideId },
            success: function (data) {
                $('#slide-Present_Loader').html(data);
            }
        });
        $.ajax({
            url: "/Shared/_Slide",
            data: { view: viewName, id: slideId },
            success: function (data) {
                $('#slide-Expanded_Loader').html(data);
            }
        });
    }
    else if (viewName == "Edit") {
        $.ajax({
            url: "/Shared/_Slide",
            data: { view: viewName, id: slideId },
            success: function (data) {
                $('#slide-Expanded_Loader').html(data);
            }
        });
    }
    else if (viewName == "View") {
        $.ajax({
            url: "/Shared/_Slide",
            data: { view: viewName, id: slideId },
            success: function (data) {
                $('#slide-View_Loader').html(data);
            }
        });
        $.ajax({
            url: "/Shared/_Slide",
            data: { view: viewName, id: slideId },
            success: function (data) {
                $('#slide-Expanded_Loader').html(data);
            }
        });
    }
}

function refreshQueuedSlides(id, view) {
    if (view == "Edit") {
        $('#queuedSlides_Loader').load('/Shared/_QueuedSlides/?id=' + id + '&page=1&maxColumn=3&maxRow=4&view=Edit');
    }
    else if (view == "Present") {
        $('#queuedSlides_Loader').load('/Shared/_QueuedSlides/?id=' + id + '&page=1&maxColumn=7&maxRow=7&view=Present');
    }
}

function refreshPresentations(view) {
    if(view == undefined)
    {
        view = 'Edit';
    }
    $.ajax({
        url: '/Shared/_FileBrowser',
        data: { view: view, filetype: "Presentations", page: 1, itemPerPage: 9 },
        type: "GET",
        error: function (a, b) {
            alert('failed to update presentations: ' + b);
        },
        success: function (data) {
            console.log('successfully updated presentations');
            $('#fileBrowser-Presentations_Loader').html(data);
        }
    });
}

function refreshSlidesBrowser(view) {
    if(view == undefined)
    {
        view = 'Edit';
    }
    $.ajax({
        url: '/Shared/_FileBrowser',
        data: { view: view, filetype: "Slides", page: 1, itemPerPage: 9 },
        type: "GET",
        error: function (a, b) {
            alert('failed to update presentations: ' + b);
        },
        success: function (data) {
            console.log('successfully updated presentations');
            $('#fileBrowser-Slides_Loader').html(data);
        }
    });
}

function deletePresentation(id) {
    $.ajax({
        url: '/Presentation',
        data: { id: id },
        type: "DELETE",
        error: function (a, b) {
            alert('error deleting presentation: ' + b);
        },
        success: function (a, b) {
            console.log('successfully deleted presentation #' + id);
            refreshPresentations();
        }
    });
}

function deletePresentationSlide(presentationid, slideid) {
    $.ajax({
        url: '/Presentation/Slide',
        data: { id: presentation, slideID: slideid },
        type: "DELETE",
        error: function (a, b) {
            alert('error deleting slide: ' + b);
        },
        success: function (a, b) {
            console.log('successfully deleted slide #' + id);
            refreshSlides();
        }
    });
}

function deleteSlide(id) {
    $.ajax({
        url: '/Slide',
        data: { slideID: id },
        type: "DELETE",
        error: function (a, b) {
            alert('error deleting slide: ' + b);
        },
        success: function (a, b) {
            console.log('successfully deleted slide #' + id);
            refreshSlides();
        }
    });
}

function slideLoaded(slideID, target) {
    console.dir($('.editable'));
    $('#slide-content-edit').aloha();
}

function saveIfChanged(oldSlide, target) {
    if (oldSlide.ID != -1) {
        var curHtml = $(target).html();
        if (oldSlide.HtmlContentString != curHtml) {
            oldSlide.HtmlContentString = curHtml;

            if (!oldSlide.Layout) { delete oldSlide.Layout; }

            $.ajax({
                url: '/Slide/Update/' + oldSlide.ID,
                data: oldSlide,
                type: "POST",
                success: function (data, status, xhr) {
                    console.log('save succeeded');
                },
                error: function (data, status, xhr) {
                    alert('save failed: ' + status);
                }
            });

        }
    }
}