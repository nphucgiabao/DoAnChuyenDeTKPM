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
        const startLocation = [pos.coords.latitude, pos.coords.longitude];
        const endLocation = await search(document.getElementById('end').value);
        //console.log(endLocation);
        if (endLocation) {

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
});





function sendMessage() {
    let message = $('#chat').val();
    connection.invoke("SendMessageToRoom", idBooking, message).catch(function (err) {
        return console.log(err.toString());
    });
    //$('#chatmessage').append(`<li>${message}</li>`);
}