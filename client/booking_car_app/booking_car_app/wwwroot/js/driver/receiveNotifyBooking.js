
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

connection.on("BroadcastBooking", function (data) {
    let bookingInfo = JSON.parse(data);
    console.log(bookingInfo);
    let item = document.createElement('div');  
    item.className = 'alert alert-primary';
    item.innerHTML = `<label>Điểm đón: ${bookingInfo.DiemDon}</label><br />
            <label>Điểm đến: ${bookingInfo.DiemDen}</label>`;
    //console.log(data);
    //var li = document.createElement("li");
    document.getElementById("notifyBooking").appendChild(item);

    //li.textContent = `${user} says ${message}`;
});

connection.on("ListBooking", function (data) {
    console.log(data);    
});

function test() {
    connection.invoke("RefreshBroadcastBooking").catch(function (err) {
        return console.log(err.toString());
    });    
}

