import React, { useEffect,useRef  } from "react";
import { shallowEqual, useDispatch, useSelector } from "react-redux";
import { GoogleMap, Marker, MarkerClusterer, InfoWindow, StandaloneSearchBox, OverlayView  } from '@react-google-maps/api';
import { Link } from "react-router-dom";
import { hotellist } from '../../store/actions';
import { useContext } from "react";



function Mapa() {
    let ref
    const [mapRef, setMapRef] = React.useState(null);
    
    const dispatch = useDispatch();
    let hotelState = useSelector((state) => state.activities, shallowEqual);
    let hotel = hotelState.hotel;
    const [zoom, setZoom] = React.useState(500000);
    const [circulo, setCirculo] = React.useState(500000);

    const onLoad = circle => {
        console.log('Circle onLoad circle: ', circle);
        setCirculo(circle);
    }

    const onUnmount = circle => {
        console.log('Circle onUnmount circle: ', circle)
    }

    const mapaRef = React.createRef();
    const { useState } = React;
    let ubi;
    let searchArea;
    const [searchBox, setSearchBox] = useState(null);

    
    let info = [];
    let coordenadas = [];
    const onSBLoad = ref => {
        setSearchBox(ref);
    };
    var myOptions = {
        zoom: 8,
        center: centerlnt
    }

    const onPlacesChanged = () => {
        let map = mapRef
        console.log(searchBox)
        console.log(searchBox.getPlaces()[0]);

        console.log(map)
        ubi = new google.maps.LatLng(searchBox.getPlaces()[0].geometry.location.lat(), searchBox.getPlaces()[0].geometry.location.lng())
        console.log(ubi.lat() + ubi.lng())

        map.state.map.panTo(ubi)
        map.state.map.setZoom(12)


    }
    class propi {
        constructor(latitud, longitud) {

            this.latitud = latitud;
            this.longitud = longitud;
        }
    }

    useEffect(() => {

        hotel.forEach(lugar => {

            let propiedad = new propi(parseFloat(lugar.propiedad.latitud), parseFloat(lugar.propiedad.longitud))/*google.maps.LatLng(parseFloat(lugar.propiedad.latitud) , parseFloat(lugar.propiedad.longitud));*/


            coordenadas.push(propiedad)


        })

    })
    const containerStyle = {
        position: "fixed!important",
        width: "600px",
        height: "400px",
       
        /*left: "900px",*/

    };
    const [position, setPosition] = useState(null)
    function handleLoad(map) {
        mapRef.current = map;
    }
    function handleCenterChanged() {
        if (ref != undefined && ref != null) {
          
            setCenter(ref.state.map.center);
        }
    }

     let fecha = f => {
         let fech = new Date(f);
         let stg = fech.getDay() + "/" + fech.getMonth() + "/" + fech.getFullYear();
         return stg;
     }

    
    const options = {
        imagePath:
            'https://developers.google.com/maps/documentation/javascript/examples/markerclusterer/m'// so you must have m1.png, m2.png, m3.png, m4.png, m5.png and m6.png in that folder
    }
    
    const [center, setCenter] = useState({

        lat: -34.0000000,
        lng: -64.000000
    });
    const refMap = useRef(null); 
    const [centerr, setcenterr] = React.useState({
        lat: -34.0000000,
        lng: -64.000000});
    let centerlnt = center
   
    let barrabusqueda;
    function cambiarhotels(newhotels) { dispatch(hotellist(newhotels)) }
     barrabusqueda = () => {
         return (
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
             </StandaloneSearchBox>
             )

     }
    const [infoWindowID, setInfoWindowID] = useState("");
   

    let markers;

    if (hotel != null) {
        markers = hotel.map((location, index) => {
            const marker = { lat: parseFloat(location.propiedad.latitud), lng: parseFloat(location.propiedad.longitud) };

            return (
               
                        <Marker
                            key={"marker" + index}
                    icon={{
                        url: "/Icons/comment.png",
                        fillOpacity: 1,
                        
                        scaledSize: new google.maps.Size(60, 30)}}
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
    } if (hotel == undefined || hotel == null || hotel.length == 0 ) {
        <OverlayView
            
            
        >
            <div >
                <h1>OverlayView</h1>

                <button
                    
                    type='button'
                >
                    Click me
                </button>
            </div>
        </OverlayView>
        
            
           
    }
    
    return (
        <div style={{ position: "fixed" , left:"900px" }}>
            <GoogleMap id="google-map" style={{ position: "fixed" }} class="google-map" ref={(mapRef) => { ref = mapRef; setMapRef(mapRef); }} mapContainerStyle={{
               
                position: 'fixed' ,
                width: "600px",
                height: "400px",

                /*left: "900px",*/

            }} center={center} zoom={3}
                onCenterChanged={handleCenterChanged} onProjectionChanged={handleCenterChanged} onBoundsChanged={() => {
                if (mapRef != undefined) {
                    if (hotel !== null) {
                        let newhotel = hotel.filter((location) => {
                            const mark = { lat: parseFloat(location.propiedad.latitud), lng: parseFloat(location.propiedad.longitud) };
                            if (mapRef.state.map.getBounds().contains(mark)==true) {return location }
                       })
                        cambiarhotels(newhotel);
                    }
                } }} >
             
                        { markers }
             {barrabusqueda()}
            
        );
            </GoogleMap>
     
         </div>
    );
}

export default React.memo(Mapa);

