
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44312/broadcastHub").build();

connection.start().then(function () {    
    connection.invoke("BroadcastBooking").catch(function (err) {
        return console.log(err.toString());
    });
    console.log('start');
    
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("BroadcastBooking", function (data) {
    let bookingInfo = JSON.parse(data);
    console.log(renderNotifyHtml(bookingInfo));
    $("#notifyBooking").append(renderNotifyHtml(bookingInfo));    
});

connection.on("ListBooking", function (data) {
    console.log(data);
    let listBooking = JSON.parse(data);    
    let html = listBooking.map(x => renderNotifyHtml(x));  
    $("#notifyBooking").html(html.join(''));
   
});

function renderNotifyHtml(notify) {   
    return `<div class='col-sm-6 col-md-6 col-lg-6 col-xl-6'>
                <div class='alert alert-primary'>
                    <label>Tên khách hàng: ${notify.Name}</label><br />
                    <label>Số điện thoại: ${notify.Phone}</label><br />
                    <label>Điểm đón: ${notify.DiemDon}</label><br />
                    <label>Điểm đến: ${notify.DiemDen}</label> <br />
                    <label>Giá: ${notify.UnitPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")}đ</label> <br />
                    <button class='btn btn-sm btn-success' onclick='receive("${notify.Id}")'>Nhận chuyến</button>
                </div>
            </div>`;    
}

async function receive(idBooking) {
    let header = createHeader($('form'));
    let result = await postData('/Driver/Booking/UpdateStatusBooking', { idBooking, status: 2 }, header);
    console.log(result);
    if (result.success) {
        connection.invoke("RefreshBroadcast").catch(function (err) {
            return console.log(err.toString());
        });
        window.location.replace(`/Driver/Booking/HandleTrip/${idBooking}`);
    }
    
    //connection.invoke("JoinRoom", idBooking).catch(function (err) {
    //    return console.log(err.toString());
    //});
   
    //connection.invoke("ReceiveBooking", idBooking).catch(function (err) {
    //    return console.log(err.toString());
    //});
}

