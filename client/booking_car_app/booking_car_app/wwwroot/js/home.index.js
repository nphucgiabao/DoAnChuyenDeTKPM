
var options = {
    'backdrop': 'static',
    'keyboard': true,
    'show': true,
    'focus': false
};
let apiKey = 'c7973264305771f8f550ce353b92a4cf';
let idBooking;
var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44312/broadcastHub").build();

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
    //connection.invoke("BroadcastBooking").catch(function (err) {
    //    return console.log(err.toString());
    //});
    console.log('start');
}).catch(function (err) {
    return console.log(err);
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
var map = L.map('map').setView([10.79, 106.63], 13);

L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);

const control = L.Routing.control({
    waypoints: [],
    router: L.Routing.osrmv1({
        serviceUrl: 'https://router.project-osrm.org/route/v1'
    }),
    geocoder: L.Control.Geocoder.nominatim(),
    createMarker: function () { return null; } // Prevent default markers
}).addTo(map);

document.getElementById('routeButton').addEventListener('click', async () => {
    const startLocation = await search(document.getElementById('DiemDon').value);
    const endLocation = await search(document.getElementById('DiemDen').value);
  
    if (startLocation && endLocation) {
       
        // Clear existing markers and waypoints
        control.setWaypoints([L.latLng(startLocation), L.latLng(endLocation)]);

        // Add custom markers
        L.marker(startLocation).addTo(map).bindPopup('Start Location').openPopup();
        L.marker(endLocation).addTo(map).bindPopup('End Location').openPopup();

        // Center the map to the route
        const bounds = L.latLngBounds([startLocation, endLocation]);
        map.fitBounds(bounds);
    }
});

control.on('routesfound', async function (e) {
    const routes = e.routes;
    const summary = routes[0].summary;
    const distance = summary.totalDistance; // Distance in meters
    const distanceKm = (distance / 1000).toFixed(2); // Convert to kilometers and format

    // Display distance
    console.log(`Distance Km: ${distanceKm} km`);
    let result = await getData('/Home/GetPrice', { idType: 1, distance: distanceKm });
    let data = JSON.parse(result.data);
    console.log(data.price);
    let price = data.price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + "đ";
    $('#routeButton').remove();
    $('#frmBooking').append(`<button class='btn btn-primary' type='submit'>Đặt chuyến  ->  ${price}</button>`)
});

//$(document).ready(function () {
//    $('#timDiemDon').on('click', async function () {
//        await search($('#DiemDon').val());
//    });
//});


async function search(query) {
    //let query = $(textbox).val();
    const url = `https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(query)}`;

    try {
        const response = await fetch(url);
        const data = await response.json();
        if (data.length === 0) {
            alert('No results found.');
            return;
        }

        const location = data[0];
        const latLng = [location.lat, location.lon];
        return latLng;
        //if (map.marker) {
        //    map.removeLayer(map.marker);
        //}

        // Add marker for the search result
        //map.marker = L.marker(latLng).addTo(map).bindPopup(location.display_name).openPopup();

        // Center the map to the search result
        //map.setView(latLng, 14);
        //map.setView(latLng, 14);
        //L.marker(latLng).addTo(map).bindPopup(location.display_name).openPopup();
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

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