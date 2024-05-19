var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44312/broadcastHub").build();

connection.start().then(function () {
     connection.invoke("JoinRoom", idBooking).catch(function (err) {
        return console.log(err.toString());
    });
    console.log('start');

}).catch(function (err) {
    return console.error(err.toString());
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