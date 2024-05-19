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
                toastr.error(error + ': ' + jqXHR.responseText);
                reject(error + ': ' + jqXHR.responseText);
            }
        });
    });
}