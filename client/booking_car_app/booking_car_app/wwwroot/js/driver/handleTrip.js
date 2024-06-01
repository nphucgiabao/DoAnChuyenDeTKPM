var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44312/broadcastHub").build();
let marker1, marker2;
let startLocation, endLocation;
var options = {
    'backdrop': 'static',
    'keyboard': true,
    'show': true,
    'focus': false
};

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
    $('#chatbox').modal(options);
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
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

$(document).ready(function () {
    navigator.geolocation.getCurrentPosition(async (pos) => {
        connection.invoke("UpdateLocationDriver", idBooking, pos.coords.latitude, pos.coords.longitude)
            .catch(function (err) {
            return console.log(err.toString());
        });
        startLocation = [pos.coords.latitude, pos.coords.longitude];
        endLocation = await search(document.getElementById('end').value);
        //console.log(endLocation);
        if (endLocation) {

            // Clear existing markers and waypoints
            control.setWaypoints([L.latLng(startLocation), L.latLng(endLocation)]);

            // Add custom markers
            marker1 = L.marker(startLocation).addTo(map).bindPopup('Start Location').openPopup();
            marker2 = L.marker(endLocation).addTo(map).bindPopup('End Location').openPopup();

            // Center the map to the route
            const bounds = L.latLngBounds([startLocation, endLocation]);
            map.fitBounds(bounds);
        }
    });
});

navigator.geolocation.watchPosition((pos) => {
    console.log('update');
    if (marker1) {
        marker1.setLatLng([pos.coords.latitude, pos.coords.longitude])
            .bindPopup('Updated location.')
            .openPopup();
    }    
    connection.invoke("UpdateLocationDriver", idBooking, pos.coords.latitude, pos.coords.longitude)
        .catch(function (err) {
            return console.log(err.toString());
    });
});

function sendMessage() {
    let message = $('#chat').val();
    connection.invoke("SendMessageToRoom", idBooking, message)
        .then(function () {$('#chat').val('');})
        .catch(function (err) {return console.log(err.toString());});
    //$('#chatmessage').append(`<li>${message}</li>`);
}

async function goTrip(idBooking) {
    let header = createHeader($('form'));
    let result = await postData('/Driver/Booking/UpdateStatusBooking', { idBooking, status: 3 }, header);
    console.log(result);
    if (result.success) {
        $('#gotrip').css('display', 'none');
        $('#hoanthanh').css('display', 'block');
        startLocation = await search(document.getElementById('end').value);
        marker1.setLatLng(startLocation)
            .bindPopup('Đã đón khách')
            .openPopup();

        endLocation = await search(document.getElementById('go').value);
        marker2.setLatLng(endLocation)
            .bindPopup('Điểm đến')
            .openPopup();

        control.setWaypoints([L.latLng(startLocation), L.latLng(endLocation)]);
        const bounds = L.latLngBounds([startLocation, endLocation]);
        map.fitBounds(bounds);

        connection.invoke("UpdateLocationDriver", idBooking, startLocation[0], startLocation[1])
            .catch(function (err) {
                return console.log(err.toString());
            });
    }
}

async function finish(idBooking) {
    let header = createHeader($('form'));
    let result = await postData('/Driver/Booking/UpdateStatusBooking', { idBooking, status: 4 }, header);
    if (result.success) {
        connection.invoke("RemoveFromGroup", idBooking).catch(function (err) {
            return console.log(err.toString());
        });
        window.location.replace(`/Driver/Booking/Finish`);
    }
    //marker1.setLatLng(endLocation)
    //    .bindPopup('Đã đến nơi')
    //    .openPopup();
    //connection.invoke("UpdateLocationDriver", idBooking, endLocation[0], endLocation[1])
    //    .catch(function (err) {
    //        return console.log(err.toString());
    //    });
}