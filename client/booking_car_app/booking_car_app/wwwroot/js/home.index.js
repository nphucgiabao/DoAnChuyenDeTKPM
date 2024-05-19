var options = {
    'backdrop': 'static',
    'keyboard': true,
    'show': true,
    'focus': false
};
let idBooking;

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44312/broadcastHub").build();

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
    connection.invoke("BroadcastBooking").catch(function (err) {
        return console.log(err.toString());
    });
    console.log('start');

}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveBooking", function (data) {
    if (data) {
        $('#modal-placeholder').find('.modal').modal('hide');
        alert('Đã có tài xế');
        $('#btnChat').css('display', 'block');
    }
});

connection.on("ReceiveMessage", function (message) {
    $('#chatmessage').append(`<li>${message}</li>`);
});

function sendMessage() {
    let message = $('#chat').val();
    connection.invoke("SendMessageToRoom", idBooking, message).catch(function (err) {
        return console.log(err.toString());
    });
    //$('#chatmessage').append(`<li>${message}</li>`);
}

$(document).ready(function () {
    $('form').submit(function (e) {
        e.preventDefault();
        console.log(this);
        $.validator.unobtrusive.parse(this);
        if ($(this).valid()) {
            let headers = createHeader(this);
            let model = $(this).serializeJSON();
            postData('/Home/Booking', JSON.stringify(model), headers, 'application/json; charset=utf-8')
                .then((response) => {                    
                    if (response.success) {
                        console.log(response);
                        let data = JSON.parse(response.data);
                        idBooking = data.id;
                        $('#modal-placeholder').find('.modal').modal(options);
                        connection.invoke("JoinRoom", data.id).catch(function (err) {
                            return console.log(err.toString());
                        });
                       
                    }
                    else {

                    }
                }).catch(err => console.log(err));
        }
    })
});

function submit(form) {
    console.log('submit')
    return false;
    //try {
    //    $.validator.unobtrusive.parse(form);

    //    if ($(form).valid()) {
    //        let headers = createHeader(form);
    //        let model = $(form).serializeJSON();
    //        console.log(model);
    //        postData('/Home/Booking', JSON.stringify(model), headers, 'application/json; charset=utf-8')
    //            .then((response) => {
    //                if (response.Success) {
    //                    console.log(response);
    //                    $('#modal-placeholder').find('.modal').modal(options);
    //                }
    //                else {

    //                }
    //            }).catch(err => console.log(err));
    //    }
    //} catch (err) {
    //    console.log(err);
    //    return false;
    //}
    
    return false;
}