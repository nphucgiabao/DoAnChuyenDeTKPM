
function showPopup(url) {
    $.get(url).done(function (data) {
        placeholderElement.html(data);
        placeholderElement.find('.modal').modal(options);
    });
}
function createHeader(form) {
    let token = $('input[name="__RequestVerificationToken"]', form).val();
    let headers = {};
    headers['X-XSRF-Token'] = token;
    return headers;
}

function postData(url, data, headers, contentType = 'application/x-www-form-urlencoded; charset=UTF-8') {
    return new Promise((resolve, reject) => {
        $.ajax({
            url,
            type: 'POST',
            headers,
            data,
            contentType,
            success: function (response) {
                resolve(response);
            },
            error: function (jqXHR, exception, error) {
                console.log(error + ': ' + jqXHR.responseText);
                reject(error + ': ' + jqXHR.responseText);
            }
        });
    });
}

function getData(url, data) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url,
            type: 'GET',
            data,
            contentType: 'application/json',
            dataType: 'json',
            success: function (response) {
                resolve(response);
            },
            error: function (jqXHR, exception, error) {
                reject(error + ': ' + jqXHR.responseText);
            }
        });
    });
}


function sweetAlert(contentValue, content = 'text') {
    return new Promise((resolve, reject) => {
        Swal.fire({
            [content]: contentValue,
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Có',
            cancelButtonText: 'Không'
        }).then((result) => {
            resolve(result.value);
        });
    });
}