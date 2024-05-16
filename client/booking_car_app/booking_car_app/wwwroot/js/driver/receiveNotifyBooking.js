
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44312/broadcastHub").build();



connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
    connection.invoke("BroadcastBooking").catch(function (err) {
        return console.log(err.toString());
    });00
    console.log('start');
    
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("BroadcastBooking", function (data) {
    let bookingInfo = JSON.parse(data);
    console.log(bookingInfo);
    renderNotifyHtml(bookingInfo);
    //let item = document.createElement('div');  
    //item.className = 'alert alert-primary';

    //item.innerHTML = `<label>Điểm đón: ${bookingInfo.DiemDon}</label><br />
    //        <label>Điểm đến: ${bookingInfo.DiemDen}</label>`;
    //console.log(data);
    //var li = document.createElement("li");
    //document.getElementById("notifyBooking").appendChild(item);

    //li.textContent = `${user} says ${message}`;
});

connection.on("ListBooking", function (data) {
    //console.log(data);    
    let listBooking = JSON.parse(data);
    $.each(listBooking, (index, value) => {
        renderNotifyHtml(value);
    });
});

function renderNotifyHtml(notify) {
    let item = document.createElement('div');
    item.className = 'col-sm-6 col-md-6 col-lg-6 col-xl-6';
    let itemChild = document.createElement('div');
    itemChild.className = 'alert alert-primary';
    itemChild.innerHTML = `<label>Điểm đón: ${notify.DiemDon}</label><br />
            <label>Điểm đến: ${notify.DiemDen}</label> <br /> 
            <button class='btn btn-sm btn-success' onclick='receive("${notify.Id}")'>Nhận chuyến</button>`;
    item.appendChild(itemChild);
    document.getElementById("notifyBooking").appendChild(item);
}

function receive(idBooking) {

}

function test() {
    connection.invoke("RefreshBroadcastBooking").catch(function (err) {
        return console.log(err.toString());
    });    
}

