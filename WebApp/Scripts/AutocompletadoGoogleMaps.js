<script>
    function handleLocationError(browserHasGeolocation, infoWindow, pos) {
        if (infoWindow && pos) {
            infoWindow.setPosition(pos);
            infoWindow.setContent(browserHasGeolocation ?
                'Error: The Geolocation service failed.' :
                'Error: Your browser doesn\'t support geolocation.');
            infoWindow.open(map);
        } else {
            console.log("handleLocationError: infoWindow or pos is not defined.");
        }
    }

    function initAutocomplete() {
        var map = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -33.8688, lng: 151.2195 },
            zoom: 1,
        });

        if (!map) {
            console.log("initAutocomplete: map is not defined.");
            return;
        }

        var input = document.getElementById('pac-input');
        var searchBox = new google.maps.places.SearchBox(input);

        if (map.controls && input) {
            map.controls[google.maps.ControlPosition.TOP_CENTER].push(input);
        } else {
            console.log("initAutocomplete: map.controls or input is not defined.");
        }

        map.addListener('bounds_changed', function () {
            if (searchBox) {
                searchBox.setBounds(map.getBounds());
            } else {
                console.log("initAutocomplete: searchBox is not defined.");
            }
        });

        var markers = [];

        searchBox.addListener('places_changed', function () {
            var places = searchBox.getPlaces();

            if (places.length === 0) {
                console.log("initAutocomplete: places is empty.");
                return;
            }

            markers.forEach(function (marker) {
                marker.setMap(null);
            });
            markers = [];

            var bounds = new google.maps.LatLngBounds();

            places.forEach(function (place) {
                if (place.geometry) {
                    if (!place.geometry.location) {
                        console.log("initAutocomplete: place.geometry.location is not defined.");
                        return;
                    }
                    var icon = place.icon || {};
                    var name = place.name || "No name";

                    markers.push(new google.maps.Marker({
                        position: place.geometry.location,
                        map: map,
                        title: name,
                    }));

                    if (place.geometry.viewport) {
                        bounds.union(place.geometry.viewport);
                    } else {
                        bounds.extend(place.geometry.location);
                    }
                } else {
                    console.log("initAutocomplete: place.geometry is not defined.");
                }
            });

            map.fitBounds(bounds).catch(function(e) {
                console.log("initAutocomplete: map.fitBounds() failed: " + e.message);
            });
        });
    }
</script>
