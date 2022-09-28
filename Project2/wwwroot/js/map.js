let map;

function initMap() {
    myLatLng = { lat: 49.8374262, lng: 18.1626191 };
    map = new google.maps.Map(document.getElementById("map"), {
        center: myLatLng,
        zoom: 15,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    new google.maps.Marker({
        position: myLatLng,
        map,
        title: "Hello World!",
    });
 
}