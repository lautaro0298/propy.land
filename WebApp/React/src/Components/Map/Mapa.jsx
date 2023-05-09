import React, { useEffect, useRef, useState } from "react";
import { shallowEqual, useDispatch, useSelector } from "react-redux";
import { GoogleMap, Marker, MarkerClusterer, InfoWindow, StandaloneSearchBox, OverlayView, useLoadScript, useJsApiLoader } from '@react-google-maps/api';
import { Link } from "react-router-dom";
import { hotellist } from '../../store/actions';

function Mapa() {
    const dispatch = useDispatch();
    let refMap = useRef();
    const hotelState = useSelector((state) => state.activities, shallowEqual);
    const hotel = hotelState.hotel;
    const [zoom, setZoom] = useState(3);
    const [circulo, setCirculo] = useState(500000);
    const mapaRef = useRef(null);
    const [searchBox, setSearchBox] = useState(null);
    const [center, setCenter] = useState({ lat: -34.0000000, lng: -64.000000 });
    const [infoWindowID, setInfoWindowID] = useState("");
    const coordenadas = [];
    const [onmap, setOnmap] = useState(false);
    let ubi;

    const onLoad = (circle) => {
        console.log('Circle onLoad circle: ', circle);
        setCirculo(circle);
    }

    const onUnmount = (circle) => {
        console.log('Circle onUnmount circle: ', circle);
    }

    const onSBLoad = (ref) => {
        setSearchBox(ref);
    };
    const handleCenterChanged = () => {
        console.log(mapaRef);
        if (mapaRef.current != null) {
            console.log(mapaRef.current.getBounds());
        }
    }
    const onBoundsChanged = () => {
        if (mapaRef.current != null || mapaRef != undefined) {
            if (hotel !== null) {
                let newhotel = hotel.filter((location) => {
                    const mark = { lat: parseFloat(location.propiedad.latitud), lng: parseFloat(location.propiedad.longitud) };
                    if (mapaRef.current.getBounds().contains(mark) == true) { return location }
                })
                dispatch(hotellist(newhotel));
            }
        }
    }
    const onPlacesChanged = () => {

        if (searchBox != null) {
            ubi = new google.maps.LatLng(searchBox.getPlaces()[0].geometry.location.lat(), searchBox.getPlaces()[0].geometry.location.lng())

            mapaRef.current.panTo(ubi);
            mapaRef.current.setZoom(12);
        }
    }

    class Propi {
        constructor(latitud, longitud) {
            this.latitud = latitud;
            this.longitud = longitud;
        }
    }
   
    useEffect(() => {
        if (hotel) {
            hotel.forEach((lugar) => {
                let propiedad = new Propi(parseFloat(lugar.propiedad.latitud), parseFloat(lugar.propiedad.longitud));
                coordenadas.push(propiedad);
            });
        }
    }, [hotel]);

    const containerStyle = {
        position: "fixed!important",
        width: "600px",
        height: "400px",
        /*left: "900px",*/
    };

    function handleLoad(map) {
        mapaRef.current = map;
        setOnmap(true);
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

                        scaledSize: new google.maps.Size(60, 30)
                    }}
                    visible={true}

                    position={marker}
                    label={"$" + location.propiedad.precioPropiedad.toString()}
                    onClick={() => {
                        setInfoWindowID(index);
                    }}
                >
                    {infoWindowID === index && (
                        <InfoWindow>
                            <div key={"item" + index} className="hotel-info-div">

                                <img src={location.propiedad.imagenPropiedad[0].rutaImagenPropiedad} width={1} height={1} />



                                <div className="about-hotel1">

                                    <div className="location">
                                        <label>
                                            Precio: $ {location.propiedad.precioPropiedad}  {location.propiedad.tipoMoneda.denominacionMoneda}


                                        </label>
                                    </div>

                                    <div className="rating">


                                        <b> {location.propiedad.ubicacion}</b>




                                    </div>


                                    <div class="static">
                                        <Link to={`/VisitaInmueble/VisitarPublicacion?publicacionId=${location.publicacionId}`}>
                                            <button type="button" class="btn btn-primary "
                                                onClick={(e) => {
                                                    console.log(e.target);
                                                }}
                                            >
                                                <br />
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
        <div >

           

            <div className="mapagoogle" style={{ position: "fixed", left: "900px" }}>
                <GoogleMap
                        ref={refMap}
                        id="map-google"
                        mapContainerStyle={{

                            position: 'fixed',
                            width: "600px",
                            height: "400px",

                            /*left: "900px",*/

                        }}
                    center={center}
                    zoom={zoom}
                    onLoad={handleLoad}
                        onCenterChanged={handleCenterChanged}
                        onBoundsChanged={onBoundsChanged} >
                
                    {markers && (
                        <MarkerClusterer >
                            {(clusterer) =>
                                markers.map((marker, index) => (
                                    <Marker key={index} position={marker.props.position} label={marker.props.label} icon={marker.props.icon} onClick={() => setInfoWindowID(index)}>
                                        {infoWindowID === index && marker.props.children}
                                    </Marker>
                                ))
                            }
                        </MarkerClusterer>
                    )}
                   

                            {


                                    <StandaloneSearchBox
                                        onPlacesChanged={onPlacesChanged}
                                        onLoad={onSBLoad}
                                    >
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
                                                marginLeft: "-120px"
                                            }}
                                        />
                                    </StandaloneSearchBox>}
                        </GoogleMap>
                        </div>
                     
                    
           
        </div>
    );
}

export default Mapa;

