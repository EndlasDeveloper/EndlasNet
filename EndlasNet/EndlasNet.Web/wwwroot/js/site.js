// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


function IsImageSet(image) {
    if (image == null) {
        return getNoImageAvailable();
    }

}



jQueryAjaxNoImageAvailable = form => {
    try {
        $.ajax({
            type: 'GET',
            url: url,
            success: function (res) {
                $('#form-modal .modal-body').html(res);
                $('#form-modal .modal-title').html(title);
                $('#form-modal').modal('show');
                // to make popup draggable
                $('.modal-dialog').draggable({
                    handle: ".modal-header"
                });
            }
        })
        return false;
    }
     //to prevent default form submit event   
     catch (ex) {
        console.log(ex)
    }
}




// Write your JavaScript code.
function getWorkFromInput() {
    return {
        "workID": getValue("workID"),
    };
}



function getFromInput(partsForWork) {
    return {

        "powderForPartId": getValue("powderForPartId"),
        "powderBottleId": getValue("powderBottleId"),
        "partForWorkId": getValue("partForWorkId"),
        "powderWeightUsed": getValue("powderWeightUsed"),
        "checkboxes": partsForWork,
    };
}

function getCheckBoxFromInput(id) {
    let fullId = "id " + id;
    console.log(fullId);
    console.log(getValue(fullId));
    let value = "value " + id;
    return {
        "IsChecked": getValue(fullId),
        "PartForWorkId": getValue(value),
        "Label": null,
        "RuntimeId": fullId
    }
}


function getCheckBoxFromInput(id) {
    let fullId = "id " + id;
    console.log(fullId);
    console.log(getValue(fullId));
    let value = "value " + id;
    return {
        "IsChecked": getValue(fullId),
        "PartForWorkId": getValue(value),
        "Label": null,
        "RuntimeId": fullId
    }
}

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
            // to make popup draggable
            $('.modal-dialog').draggable({
                handle: ".modal-header"
            });
        }
    })
}

jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-all').html(res.html)
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else
                    $('#form-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}

jQueryAjaxDelete = form => {
    if (confirm('Are you sure to delete this record ?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    $('#view-all').html(res.html);
                },
                error: function (err) {
                    console.log(err)
                }
            })
        } catch (ex) {
            console.log(ex)
        }
    }

    //prevent default form submit event
    return false;
}