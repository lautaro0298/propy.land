import styled from "styled-components";
import axios from "axios";
import placeholder from "../../Logos/placeholder.png";
import addgroup from "../../Logos/addgroup.png";
import { Button } from "@material-ui/core";
import ClearIcon from "@material-ui/icons/Clear";
import ArrowBackIosIcon from "@material-ui/icons/ArrowBackIos";
import ArrowForwardIosIcon from "@material-ui/icons/ArrowForwardIos";
import GuestCard from "../material-ui-components/GuestCard";
import { RatingCard } from "../material-ui-components/RatingCard"
import { useState, useEffect, useRef } from "react";
import { addHotelList, tipoPublicante } from '../../store/actions';
import { shallowEqual, useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router-dom";
import { MoreFilterCard } from "../material-ui-components/MoreFilterCard";
import KeyboardArrowDownIcon from "@material-ui/icons/KeyboardArrowDown";
import { PrettoSlider } from "../material-ui-components/LocationCard";
import { palabra } from '../../store/actions';
import { Marker, MarkerClusterer, InfoWindow, StandaloneSearchBox, useLoadScript, LoadScript } from '@react-google-maps/api';
import Mapa from "../Map/Mapa";
export function SearchBar() {
    let hotelState = useSelector((state) => state.activities, shallowEqual);
    let hotel = hotelState.hotel;
    const [city, setCity] = useState();
    let [hotels, setHotels] = useState([]);
    let map;
    let searchArea
    /* const [searchBox, setSearchBox] = useState(null);*/
    const [price, setPrice] = useState(14100);
    const [show, setShow] = useState(false);
    const [hotelClass, setHotelClass] = useState(false);
    const [houseClass, setHouseClass] = useState(false);
    const [border, setBorder] = useState(false);
    const [datePicker, setDatePicker] = useState(false);
    const [clickedCheckOut, setClickedCheckOut] = useState(false);
    const [guestSelect, setGuestSelect] = useState(false);
    const [location, setLocation] = useState("");
    const [checkInDate, setCheckInDate] = useState("Seleccione una opcion");
    const [checkOutDate, setCheckOutDate] = useState("Seleccione una opcion");
    const [guestNumber, setGuestNumber] = useState(2)
    const [roomsNumber, setRoomsNumber] = useState(1)
    const [facilitiesforfilter, setFacilitiesforfilter] = useState({});
    const [facilitieslength, setFacilitieslength] = useState(0);
    const [showMoreFilterCard, setShowMoreFilterCard] = useState(false);
    const [moneda, setMoneda] = useState(["ARS", "USD"]);
    const [publicante, setPublicante] = useState([]);
    let publicanteIs = false;
    let monedaIs = false;
    const [monedaSelect, setMonedaSelect] = useState("ARS")
    const [publicacionSelect, setPublicacionSelect] = useState("")
    const dispatch = useDispatch();
    useEffect(() => {
        axios.get(`https://propycore.azurewebsites.net/api/TipoMoneda/obtenerTiposMonedas`, {
            method: 'GET',
            headers: {
                'content-type': 'application/json',
            }

        })
            .then(res => { setMoneda(res.data); monedaIs = true; });
        axios.get(`https://propycore.azurewebsites.net/api/TipoPublicacion/obtenerTiposPublicaciones`, {
            method: 'GET',
            headers: {
                'content-type': 'application/json',
            }
        })
            .then(res => { setPublicante(res.data); publicanteIs = true; });
    }, []);
    function cambioMoneda(e) {
        setMonedaSelect(e.target.value)
        dispatch(addHotelList(e.target.value, monedaSelect))
    }
    function cambioPublicacion(e) {
        setPublicacionSelect(e.target.value)
        dispatch(tipoPublicante(e.target.value))
    }
    const history = useHistory()
    const onSBLoad = ref => {
        setSearchBox(ref);
        console.log(ref);
    };


    const handleBorder = () => {
        setBorder(true);
        setDatePicker(false);
        setClickedCheckOut(false);
    };

    const handleShow = () => {
        setShow(true);
        setHotelClass(false);
        setHouseClass(false);
        setBorder(false);
    };
    const handleHotelClass = () => {
        setHotelClass(true);
        setShow(false);
        setHouseClass(false);
        setBorder(false);
    };
    const handleHouseClass = () => {
        setHouseClass(true);
        setShow(false);
        setHotelClass(false);
        setBorder(false);
    };
    const getPrice = (e, value) => {
        setPrice(value);
        dispatch(addHotelList(value, monedaSelect))
    };
    const handleLocationInput = (e) => {
        e.preventDefault();
        console.log(location);
        setLocation(e.target.value);
    };
    const handleClear = () => {
        setLocation("");
    };
    const handleDatePicker = () => {
        setDatePicker(!datePicker);
        setClickedCheckOut(false);
        setGuestSelect(false)
    };
    const handleClickedCheckOut = () => {
        setClickedCheckOut(!clickedCheckOut);
        setDatePicker(false);
        setGuestSelect(false)
        setShowMoreFilterCard(false);
    };
    const hanldleAllCards = () => {
        setShowMoreFilterCard(false);

    };
    const handleGuestSelector = () => {
        setDatePicker(false);
        setClickedCheckOut(false);
        setShowMoreFilterCard(false);
        setGuestSelect(!guestSelect)

    }
    const handleMoreFilterCard = () => {

        setShowMoreFilterCard(!showMoreFilterCard);


    };
    const searchBoxRef = useRef(null);
    //if (window.google != null || window.google != undefined) {
    //    const mapas = new window.google.maps.Map(document.createElement('div'));
    //    const searchBox = new window.google.maps.places.StandaloneSearchBox(searchBoxRef.current);
    //    searchBox.bindTo('bounds', mapas);
    //}
    const busqueda = () => {

        return (<LoadScript
            googleMapsApiKey="AIzaSyDHXJNkL77-_eh9GRL1pZr1EAHrAh_uQR4"

        >

           

                <StandaloneSearchBox
                    ref={searchBoxRef}
                    onPlacesChanged={onPlacesChanged}
                onLoad={(ref) => {console.log(ref)}}
                >
                    <input
                        placeholder="Ingrese el lugar donde quiere buscar propiedades"
                        type="text"
                        value={location}
                        onChange={handleLocationInput}
                    />
                </StandaloneSearchBox>



          
        </LoadScript

        >);

    }
    function markers () {

        if (hotel !== null) {
            console.log(hotel)
            markers = hotel.map((location, index) => {
                const marker = { lat: parseFloat(location.propiedad.latitud), lng: parseFloat(location.propiedad.longitud) };

                return (
                    <MarkerClusterer options={options}>
                        {(clusterer) =>
                            <Marker
                                key={index}
                                icon={"/Icons/casaIcono.png"}
                                visible={true}
                                position={marker}
                                label={index.toString()}
                                onClick={() => {
                                    setInfoWindowID(index);
                                }}
                            >
                                {infoWindowID === index && (
                                    <InfoWindow>
                                        <div key={location.publicacionId} className="hotel-info-div">

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
                                                <div className="static">
                                                    <Link to={`/VisitaInmueble/VisitarPublicacion?publicacionId=${location.publicacionId}`}>
                                                        <button type="button" className="btn btn-primary "
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
                            </Marker>}</MarkerClusterer>
                );
            });
        }
    }
    const onPlacesChanged = () => {
        
        if (document.getElementById('google-map') != undefined && document.getElementById('google-map') != null ) {
            map = new window.google.maps.Map(document.getElementById('google-map'),
                {
                    zoom: 14,
                    center: new google.maps.LatLng(searchBox.getPlaces()[0].geometry.location.lat(), searchBox.getPlaces()[0].geometry.location.lng())

                });
            searchArea = new google.maps.Circle({
                strokeColor: '#FF0000',
                strokeOpacity: 0.5,
                strokeWeight: 2,
                fillColor: '#FF0000',
                fillOpacity: 0.2,
                map: map,
                center: map.getCenter,
                radius: 10000000000
            });
            hotel.map((element) => {
                console.log(element)
                let pos = new google.maps.LatLng(element.propiedad.latitud, element.propiedad.longitud)
                let mark = new google.maps.Marker({
                    position: pos,
                    map,
                    icon: "/Icons/casaIcono.png",

                });
                const contentString =
                    `
                <div key=${element.publicacionId} className="hotel-info-div">

                                         <img src=${element.propiedad.imagenPropiedad[0].rutaImagenPropiedad} style="height: 190px; width: 200px;"/>



                                <div className="about-hotel1">

                                    <div className="location">
                                        <label>
                                            Precio: $ ${element.propiedad.precioPropiedad}  ${element.propiedad.tipoMoneda.denominacionMoneda}


                                        </label>
                                    </div>

                                    <div className="rating">


                                                <b> ${element.propiedad.ubicacion}</b>




                                    </div>


                                         <div className="static">
                                                 
                                                 <a type="button" className="btn btn-primary "
                                                       href=/VisitaInmueble/VisitarPublicacion?publicacionId=${element.publicacionId}
                                                     >
                                                        Ver
                                                    </a>
                                                
                                            </div>


                                         </div>
                            </div>
`
                const infowindow = new google.maps.InfoWindow({
                    content: contentString,
                })
                mark.addListener("click", () => {
                    infowindow.open({
                        anchor: mark,
                        map,
                        shouldFocus: false,
                    });
                });

            })
            let ubi = new google.maps.LatLng(searchBox.getPlaces()[0].geometry.location.lat(), searchBox.getPlaces()[0].geometry.location.lng())
            map.moveCamera(ubi)
            map.setZoom(12)
        }
    }
    const [libraries] = useState(['places']);
  const handleSearchButton = () => {

    handleSearchData(location)
    handleGuestsData(guestNumber)
    handleRoomsData(roomsNumber)

    handleFirstDate(checkInDate)
    handleSecondDate(checkOutDate)

    let first = Number(checkInDate.slice(8, 10))
    let last = Number(checkOutDate.slice(8, 10))

    let fmonth = checkInDate.slice(4, 7)
    let smonth = checkOutDate.slice(4, 7)
   
    if (fmonth === smonth) {
      handleDays(last - first)
    } else {
      if (smonth === "Jan" || smonth === "Mar" || smonth === "May" || smonth === "Jul" || smonth === "Aug" || smonth === "Oct" || smonth === "Nov" || smonth === "Dec") {
        if (last < first) {
          if (fmonth === "Jun" || fmonth === "Sep" || fmonth === "Nov" || fmonth === "Apr") {
            handleDays(30 - first + last)
          } else if (fmonth === "Jan" || fmonth === "Mar" || fmonth === "May" || fmonth === "Jul" || fmonth === "Aug" || fmonth === "Oct" || fmonth === "Nov" || fmonth === "Dec") {
            handleDays(31 - first + last)
          }
        }
      }
      if (smonth === "Jun" || smonth === "Sep" || smonth === "Nov" || smonth === "Apr") {
        if (last < first) {
          if (fmonth === "Jun" || fmonth === "Sep" || fmonth === "Nov" || fmonth === "Apr") {
            handleDays(30 - first + last)
          } else if (fmonth === "Jan" || fmonth === "Mar" || fmonth === "May" || fmonth === "Jul" || fmonth === "Aug" || fmonth === "Oct" || fmonth === "Nov" || fmonth === "Dec") {
            handleDays(31 - first + last)
          }
        }
      }
    }




    history.push('/hotel-booking')
  }
    
  return (
    <SearchBarWrapper>
      <div className="search-bar-cont">
        <SearchBoxWrapper>
          <SearchBarMainWrapper>
            <SelectLocationWrapper>
                          <img src={placeholder} alt="" />
                          <div
                onClick={handleBorder}
                className={border ? "dottedBorder" : undefined}
              >
                              <Button
                                  onClick={handleClear}
                                  style={{
                                      position: "relative",
                                      maxWidth: "30px",
                                      maxHeight: "30px",
                                      minWidth: "30px",
                                      minHeight: "30px",
                                      top: "4px",
                                      left:"400px"
                                  }}
                              >
                                  <ClearIcon />
                              </Button>

                              <LoadScript
                                  googleMapsApiKey="AIzaSyDHXJNkL77-_eh9GRL1pZr1EAHrAh_uQR4"
                                  onLoad={() => console.log("API loaded")}
                              >



                                  <StandaloneSearchBox
                                      ref={searchBoxRef}
                                      onPlacesChanged={onPlacesChanged}
                                      onLoad={(ref) => { console.log(ref) }}
                                  >
                                      <input
                                          placeholder="Ingrese el lugar donde quiere buscar propiedades"
                                          type="text"
                                          value={location}
                                          onChange={handleLocationInput}
                                      />
                                  </StandaloneSearchBox>




                              </LoadScript

                              >

                
              </div>
            </SelectLocationWrapper>
            <PickDateWrapper>
              <div>
                
                              <SelectGuestsWrapper>
                                  <div>
                                      <div onClick={handleGuestSelector} className="guestsnumber">
                                          <img src={addgroup} alt="" />
                                          <div className="guest-al">
                                              <span>Caracteristicas</span>
                                          </div>
                                      </div>
                                     
                                  </div>
                              </SelectGuestsWrapper>
                <span className="partitionLine"></span>
                <div onClick={handleClickedCheckOut} className="checkOutdate">
                                  <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-house" viewBox="0 0 16 16">
                                      <path fillRule="evenodd" d="M2 13.5V7h1v6.5a.5.5 0 0 0 .5.5h9a.5.5 0 0 0 .5-.5V7h1v6.5a1.5 1.5 0 0 1-1.5 1.5h-9A1.5 1.5 0 0 1 2 13.5zm11-11V6l-2-2V2.5a.5.5 0 0 1 .5-.5h1a.5.5 0 0 1 .5.5z" />
                                      <path fillRule="evenodd" d="M7.293 1.5a1 1 0 0 1 1.414 0l6.647 6.646a.5.5 0 0 1-.708.708L8 2.207 1.354 8.854a.5.5 0 1 1-.708-.708L7.293 1.5z" />
                                  </svg>
                  <div className="date-al date-al-margin">
                     
                    <span>Tipo de propiedad:</span>
                   
                  </div>
                   <div className="arrows-margin2">
                    <ArrowBackIosIcon style={{ cursor: "pointer" }} />
                    <ArrowForwardIosIcon style={{ cursor: "pointer" }} />
                  </div>
                </div>
                            
              </div>
            </PickDateWrapper>
            
                      <MoreFilteringWrapper onClick={() => {
                          handleMoreFilterCard()
                          setDatePicker(false);
                          setClickedCheckOut(false);
                          setGuestSelect(false);
                      }}>
                          <div>
                              <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-bookmark-plus-fill" viewBox="0 0 16 16">
                                  <path fillRule="evenodd" d="M2 15.5V2a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v13.5a.5.5 0 0 1-.74.439L8 13.069l-5.26 2.87A.5.5 0 0 1 2 15.5zm6.5-11a.5.5 0 0 0-1 0V6H6a.5.5 0 0 0 0 1h1.5v1.5a.5.5 0 0 0 1 0V7H10a.5.5 0 0 0 0-1H8.5V4.5z" />
                              </svg>
                          </div>
                          <div className="downTextandArrow">
                              <span>
                                  {facilitieslength >= 1
                                      ? `${facilitieslength} Applied`
                                      : "Extras "}
                              </span>
                              <KeyboardArrowDownIcon />
                          </div>
                          </MoreFilteringWrapper>
                      
                  </SearchBarMainWrapper>
                  
              {/*barra de precio*/}
                      <GuestRatingWrapper >
                          <div>
                              <span>Tipo de moneda</span>
                          </div>
                          <div className="downTextandArrow">
                          <select id="tipoMoneda" onChange={cambioMoneda}>
                              <option>

                              </option>
                               {moneda.map((mone, index) => {
                                      return (
                                          <option key={index} value={mone.denominacionMoneda}>
                                              {mone.denominacionMoneda}
                                          </option>
                                      );
                                  })}
                              </select>
                          </div>
                      </GuestRatingWrapper>
                      <PriceNightWrapper onMouseEnter={hanldleAllCards}>


                          <div className="priceNightSlider">
                              <PrettoSlider
                                  value={price}
                                  onChange={getPrice}

                                  min={0}
                                  step={300}
                                  max={1000000}
                                  aria-label="pretto slider"
                                  defaultValue={0}
                              />
                          </div>
                  <div className="priceNightText">

                      <div>
                          <span>Precio</span>
                      </div>
                          <div>
                              <span>{monedaSelect}  ${price}</span>
                      </div>
                      </div>
                  </PriceNightWrapper>
                  <PriceNightWrapper onMouseEnter={hanldleAllCards}>
                      <div className="priceNightText">
                          <div>
                              <span>Buscar por palabra clave</span>
                          </div>
                      
                      </div>
                      
                      <div className="palabraClave" >
                          <input type="text" onChange={(e) => { dispatch(palabra(e.target.value)) }} />
                      </div>
                      
                  </PriceNightWrapper>
                  <GuestRatingWrapper >
                      <div>
                          <span>Tipo de Publicacion:</span>
                      </div>
                      <div className="downTextandArrow">
                          <select id="tipoMoneda" onChange={cambioPublicacion}>
                              <option>
                                  
                              </option>
                              {publicante.map((mone, index) => {
                                  
                                  return (
                                      <option key={index} value={mone.nombreTipoPublicacion}>
                                          {mone.nombreTipoPublicacion}
                                      </option>
                                  );
                              })}
                          </select>
                      </div>
                  </GuestRatingWrapper>
              </SearchBoxWrapper>
              
              {showMoreFilterCard && (
                  <MoreFilterCard
                      handleMoreFilterCard={handleMoreFilterCard }
                      left="30em" position="relative"
                      setFacilitiesforfilter={setFacilitiesforfilter}
                      setFacilitieslength={setFacilitieslength}
                      className="animated fadeIn"
                      
                      
            
          />
        )}
              {clickedCheckOut && (
                  <RatingCard
                      handleClickedCheckOut={handleClickedCheckOut}
                      clickedCheckOut={clickedCheckOut}
                      position="relative"
                      setGuestNumber={setGuestNumber} setRoomsNumber={setRoomsNumber}
                      
          />
              )}
              {guestSelect && <GuestCard right="0rem" position="relative" handleGuestSelector={handleGuestSelector} setGuestNumber={setGuestNumber} setRoomsNumber={setRoomsNumber} />}
            

      </div>
    </SearchBarWrapper>
  );
}

export const SearchBarWrapper = styled.div`
  position: relative;
  z-index: 5;
  
  height: 200px;
  background-color: rgb(245, 245, 246);
  /* border: 1px solid black;*/
   transform: scale(0.75);
 /* padding: 7rem 1%;*/
  .dottedBorder {
    border: 1px dotted black;
  }
  .search-bar-cont {
   /* display: grid;*/
    grid-template-columns: 1fr;
    width: 97rem;
  }
`;
export const BookingLogosWrapper = styled.div`
  display: grid;
  width: 100%;
  align-items: center;
  padding-top: 1rem;
  .booking-sites-text {
    h4{
      font-size: 14px;
    }
  }
  .booking-sites-logo {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr 1fr 1fr 1fr;
    grid-gap: 1rem;
    padding-left: 3rem;
    padding-top: 1rem;

    .partner_logo {
      width: 60%;
      max-height: 25px;
    }
    .logo-width {
      height: 20px;
      width: 80%;
    }
    .booking.comLogo {
      height: 18px;
      position: relative;
      top: 5px;
    }
    .agodaLogo {
      width: 42%;
      max-height: 25px;
    }
    .More {
      font-size: 12px;
      justify-content: center;
      
      color: rgb(154, 161, 165);
      top: 3px;
    }
  }
`;
export const CategoryLinksWrapper = styled.div`
  display: flex;
  .afterClick {
    background-color: white;
  }
  & > div {
    min-width: 80px;
    max-width: auto;
    height: 40px;
    padding: 1rem;
    border-top-left-radius: 1.4rem;
    border-top-right-radius: 1.4rem;
    background-color: rgb(229, 242, 246);
    text-align: center;
    .afterClickAnchor {
      color: rgb(0, 127, 173);
      font-weight: bold;
    }
    span {
      text-decoration: none;
      font-size: 13px;
      letter-spacing: 1px;
      position: relative;
      cursor: pointer;
      /* ::after{
                color: rgb(0,127,173);
                font-weight: bold;
            } */
      :hover {
        color: rgb(0, 127, 173);
        text-decoration: underline;
      }
    }
  }
  .space-left {
    margin-left: 0.7rem;
  }
  .houseAPwidth {
    width: 135px;
  }
`;
export const SearchBoxWrapper = styled.div`
  border-radius: 0 12px 12px 12px;
  width: 100%;
  height: 6rem;

  box-shadow: 1px 1px 4px rgb(205 208 210);

  padding: 0rem 0.8rem;
  background-color: white;
`;
export const SearchBarMainWrapper = styled.div`
  display: flex;
  height: 100%;
  width: 100%;
`;
export const SelectLocationWrapper = styled.div`
  width: 32%;
  display: flex;
  align-items: center;
  justify-content: start;
  border-right: 1px solid rgb(205, 208, 210);
  img {
    width: 18px;
    position: relative;
    top: 7px;
  }
  & > div {
    width: 100%;
    height: 4rem;
    input {
      width: 80%;
      outline: none;
      padding:0px !important;
      border: none;
      position: relative;
      top: 4px;
      font-size: 15px;
      ::placeholder {
        color: rgb(154, 161, 165);
      
      }
    }
    .verticleAlign {
      position: relative;
      top: 0.3rem;
    }
  }
`;
const PriceNightWrapper = styled.div`
  display: inline-block;
  border-right:1px solid rgb(205, 208, 210) !important;
  padding-right: 2rem;
  padding-left: 1rem;

  .priceNightText {
    display: grid;
    grid-template-columns: 1fr 1fr;
    & > div:nth-child(1) {
      text-align: start;
    }
    & > div:nth-child(2) {
      text-align: end;
    }
  }
  .priceNightSlider {
    .MuiSlider-root {
      color: rgb(63, 159, 193);
      width: 100%;
      cursor: pointer;
      height: 2px;
      display: inline-block;
      padding: 13px 0;
      position: relative;
      box-sizing: content-box;
      touch-action: none;
      -webkit-tap-highlight-color: transparent;
    }
  }
`;

export const PickDateWrapper = styled.div`
  width: 40%;
  padding: 0rem 1rem;
  border-right: 1px solid rgb(205, 208, 210);

  & > div {
    width: 100%;
    display: flex;
    height: 100%;
    .checkIndate {
      width: 50%;
      display: flex;

      position: relative;

      align-items: center;
      padding-left: 1px;

      height: 4rem;
      top: 10px;
      :active {
        border: 1px dotted;
      }

      img {
        width: 18px;
      }
    }
    .date-al-margin {
      position: relative;
      left: 1rem;
      grid-gap: 0.2rem;
    }
    .partitionLine {
      width: 1px;
      height: 80%;
      background-color: rgb(205, 208, 210);
      margin-top: 6px;
    }
    .arrows-margin {
      position: relative;
      left: 2rem;
    }
    .arrows-margin2 {
      position: relative;
      left: 5rem;
    }
    .date-al {
      display: grid;

      & > span:nth-child(1) {
        font-size: 12px;
        font-weight: 100;
      }
      & > span:nth-child(2) {
        font-size: 12px;
        font-weight: 600;
      }
    }
    .checkOutdate {
      width: 50%;
      display: flex;
      align-items: center;
      position: relative;

      height: 4rem;
      top: 10px;
      :active {
        border: 1px dotted;
      }
    }
  }
`;
export const SelectGuestsWrapper = styled.div`
  width: 32%;
  /* padding: 1rem; */
  padding-left: 1rem;
  & > div {
    display: flex;
    align-items: center;
    height: 100%;
    img {
      width: 18px;
      height: 18px;
      position: relative;
      top: 4px;
    }
    .guestsnumber {
      display: flex;
      width: 50%;
      :active {
        border: 1px dotted;
      }
      .guest-al {
        display: grid;
        position: relative;
        left: 1rem;

        & > span:nth-child(1) {
          font-size: 11px;
        }
        & > span:nth-child(2) {
          font-size: 12px;
          font-weight: bold;
        }
      }
    }
    button {
    transform: scale(0.8)
    width: 110%;
    height:30%;
    padding: 2rem;
    background-color: #007fad;
    border: 1px solid #007fad;
    border-bottom-color: #005f81;
    border-radius: 4rem;
    color: white;
    outline: none;
    border: none;
    font-size: 16px;
    font-weight: 700;
      :hover {
        background-color: #005f81;
        cursor: pointer;
      }
    }
  }
`;
const MoreFilteringWrapper = styled.div`
  display: grid;
  padding-left: 4px;
  .downTextandArrow {
    display: grid;
    grid-template-columns: 2fr 1fr;
  }
  :hover {
    background-color: rgb(235, 236, 237);
  }
`;
const GuestRatingWrapper = styled.div`
  display: inline-block;
  padding-left: 4px;
  padding-right: 2rem;
  border-right:1px solid rgb(205, 208, 210) !important;

  .downTextandArrow {
    display: grid;

    grid-template-columns: 2fr 1fr;
  }
  :hover {
    background-color: rgb(235, 236, 237);
  }
`;
