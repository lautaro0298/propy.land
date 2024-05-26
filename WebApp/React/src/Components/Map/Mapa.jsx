import React, { useEffect, useRef, useState } from "react";
import { shallowEqual, useDispatch, useSelector } from "react-redux";
import {
  GoogleMap,
  Marker,
  MarkerClusterer,
  InfoWindow,
  StandaloneSearchBox,
} from "@react-google-maps/api";
import { Link } from "react-router-dom";
import { hotellist } from "../../store/actions";

function Mapa() {
  const dispatch = useDispatch();
  const hotelState = useSelector((state) => state.activities, shallowEqual);
  const hotel = hotelState.hotel;
  const [map, setMap] = useState(null);
  const [searchBox, setSearchBox] = useState(null);
  const [infoWindowID, setInfoWindowID] = useState(null);
  const [coordenadas, setCoordenadas] = useState([]);
  const formatPrecio = (precio) => {
    let formattedPrecio;

    if (precio >= 1000000) {
      formattedPrecio = (precio / 1000000).toLocaleString("en-US", {
        minimumFractionDigits: 0,
        maximumFractionDigits: 1,
      });
    } else if (precio >= 1000) {
      formattedPrecio = (precio / 1000).toLocaleString("en-US", {
        minimumFractionDigits: 0,
        maximumFractionDigits: 0,
      });
    } else {
      formattedPrecio = precio.toLocaleString("en-US", {
        minimumFractionDigits: 0,
        maximumFractionDigits: 0,
      });
    }

    return formattedPrecio;
  };

  useEffect(() => {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          setCoordenadas((prevCoordenadas) => [
            ...prevCoordenadas,
            { lat: position.coords.latitude, lng: position.coords.longitude },
          ]);
          setCenter({ lat: position.coords.latitude, lng: position.coords.longitude });
          setZoom(12);
        },
        (error) => {
          console.log("Error getting location:", error);
        }
      );
    } else {
      console.log("Geolocation is not supported by this browser.");
    }
  }, []);

  const onSBLoad = (ref) => {
    setSearchBox(ref);
  };

  const onBoundsChanged = () => {
    if (map != null) {
      if (hotel !== null) {
        let newhotel = hotel.filter((location) => {
          const mark = { lat: parseFloat(location.propiedad.latitud), lng: parseFloat(location.propiedad.longitud) };
          if (map.getBounds().contains(mark) == true) {
            return location;
          }
        });
        dispatch(hotellist(newhotel));
      }
    }
  };

  const onPlacesChanged = () => {
    if (searchBox != null) {
      const place = searchBox.getPlaces()[0];
      if (place != null) {
        setCenter({ lat: place.geometry.location.lat(), lng: place.geometry.location.lng() });
        setZoom(12);
      }
    }
  };

  const containerStyle = {
    position: "fixed",
    width: "600px",
    height: "400px",
    left: "900px",
  };

  function handleLoad(map) {
    setMap(map);
  }

  let markers;

  if (hotel) {
    markers = hotel.map((location, index) => {
      const marker = { lat: parseFloat(location.propiedad.latitud), lng: parseFloat(location.propiedad.longitud) };

      return (
        <Marker
          key={"marker" + index}
          icon={{
            url: "/Icons/comment.png",
            fillOpacity: 1,
            scaledSize: new google.maps.Size(60, 30),
          }}
          position={marker}
          label={"$" + formatPrecio(location.propiedad.precioPropiedad)}
          onClick={() => {
            setInfoWindowID(index);
          }}
        >
          {infoWindowID === index && (
            <InfoWindow>
              <div key={"item" + index} className="hotel-info-div">
                <img
                  src={location.propiedad.imagenPropiedad[0].rutaImagenPropiedad}
                  width={1}
                  height={1}
                />

                <div className="about-hotel1">
                  <div className="location">
                    <label>
                      Precio: $
                      {location.propiedad.precioPropiedad}{" "}
                      {location.propiedad.tipoMoneda.denominacionMoneda}
                    </label>
                  </div>

                  <div className="rating">
                    <b>{location.propiedad.ubicacion}</b>
                  </div>

                  <div className="static">
                    <Link to={`/VisitaInmueble/VisitarPublicacion?publicacionId=${location.publicacionId}`}>
                      <button
                        type="button"
                        className="btn btn-primary "
                        onClick={(e) => {
                          console.log(e.target);
                        }}
                      >
                        Ver
                      </button>
                    </Link>
                  </div>
                </div>
              </div>
            </InfoWindow>
          )}
        </Marker>
      );
    });
  }

  return (
    <div>
      <div className="mapagoogle" style={{ position: "fixed", left: "900px" }}>
        <GoogleMap
          ref={refMap}
          setMapCallback={() => console.log(refMap)}
          id="map-google"
          mapContainerStyle={containerStyle}
          center={center}
          zoom={zoom}
          onLoad={handleLoad}
          onBoundsChanged={onBoundsChanged}
        >
          {markers && (
            <MarkerClusterer>
              {(clusterer) =>
                markers.map((marker, index) => (
                  <Marker
                    key={index}
                    position={marker.props.position}
                    label={marker.props.label}
                    icon={marker.props.icon}
                    onClick={() => setInfoWindowID(index)}
                  >
                    {infoWindowID === index && marker.props.children}
                  </Marker>
                ))
              }
            </MarkerClusterer>
          )}

          {searchBox != null && (
            <StandaloneSearchBox onPlacesChanged={onPlacesChanged} onLoad={onSBLoad}>
              <input
                type="text"
                placeholder="Ingrese el lugar donde quiere buscar"
                style={{
                  boxSizing: `border-box`,
                  border: `1px solid transparent`,
                  width: `240px`,
                  height: `32px`,
                  padding: `0 12px`,
                  borderRadius: `3px`,
                  boxShadow: `0 2px 6px rgba(0, 0, 0, 0.3)`,
                  fontSize: `14px`,
                  outline: `none`,
                  textOverflow: `ellipses`,
                  position: "absolute",
                  left: "50%",
                  marginLeft: "-120px",
                }}
              />
            </StandaloneSearchBox>
          )}
        </GoogleMap>
      </div>
    </div>
  );
}

export default Mapa;
