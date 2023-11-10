import React from "react";
import "./AllHotels.css";
import styled from "styled-components";
import { shallowEqual, useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { SearchBar } from "../Search-Bar/SearchBar";
import { getAllHotel, fliterPrecio, fliterPrecio1, fliterReciente, fliterviejo, restFuntion } from '../../store/actions';
import Mapa from "../Map/Mapa";
import { RatingView } from "react-simple-star-rating";
import imagen from "../../Logos/placeholder.png";
import imagen0 from "../../../public/down-arrow.png";
import StaticMap from "../Map/StaticMap";
import { Link } from "react-router-dom";
import { GoogleMap, InfoBox } from '@react-google-maps/api';
import axios from "axios";
const initState = {
    data: [],
    isLoading: false,
    isError: false,
    hotel: [],
    hotLoad: false,
    hotErr: false,
    currPage: 1,
    currQuery: null,
    rest: false
};
export default function AllHotels() {

    const MiContexto = React.createContext();
    // aca usamos la context de react 
    const dispatch = useDispatch();
    const Caracteristica = ({ caracteristicaId }) => {
        const [nombreCaracteristica, setNombreCaracteristica] = useState("");

        useEffect(() => {
            // Realizar la solicitud de la característica cuando el componente se monte
            const sessionId = document.getElementById('root').dataset.sessionId;

            axios
                .get(`https://propyy.somee.com/api/Caracteristica/obtenerPorID/${caracteristicaId}`)
                .then((response) => {
                    if (response.data) {
                        setNombreCaracteristica(response.data.nombreCaracteristica);
                    } else {
                        setNombreCaracteristica("Caracteristica no disponible"); // Manejar el caso de error
                    }
                })
                .catch((error) => {
                    console.error("Error al obtener la caracteristica:", error);
                    setNombreCaracteristica(""); // Manejar el caso de error
                });
        }, [caracteristicaId]);

        return <span>{nombreCaracteristica}</span>;
    };
    const [isFavorited, setIsFavorited] = useState({});

    const checkIfFavorited = (publicacionId) => {
        const sessionId = document.getElementById('root').dataset.sessionId;
        if (!sessionId) {
            return Promise.resolve(false);
        }
        return axios.get(`/Usuario/ConsultarIsFavorito?publicacionId=${publicacionId}`, {
            headers: {
                Authorization: `Bearer ${sessionId}`,
            },
        }).then(response => response.data)
            .catch(error => {
                console.error("Error al verificar si es favorito:", error);
                return false;
            });
    };
    let hotelState = useSelector((state) => state.activities, shallowEqual);
    let hotel = hotelState.hotel;
    let hotellist = hotelState.hotelsList;
    const [publicationsToShow, setPublicationsToShow] = useState(3); // Número inicial de publicaciones a mostrar
    const publicationsPerPage = 3; // Número de publicaciones a cargar cada vez que se haga clic en "Mostrar más"
    // Implementa la función que muestra las publicaciones
    const peticion = (caracteristicaId) => {
        return axios.get(`https://propyy.somee.com/api/Caracteristica/obtenerPorID/${caracteristicaId}`)
            .then(response => response.data)
            .catch(error => {
                console.error("", error);
                return null; // Manejo de error, puedes retornar un valor predeterminado en caso de error
            });
    };
    useEffect(() => {
        if (hotellist.length === 0) {
            dispatch(restFuntion(true));
        }
    }, [hotellist]);
    const showPublications = (data) => {
        if (data != undefined && data != null) {

            // Filtra las publicaciones a mostrar en base al número definido en publicationsToShow
            const publicationsToRender = hotellist.slice(0, publicationsToShow);

            return (
                <div>
                    {/* Muestra las publicaciones */}
                    {publicationsToRender.map((data) => publicaciones(data))}

                    {/* Muestra el botón "Mostrar más" solo si hay más publicaciones por cargar */}
                    {publicationsToShow < hotellist.length && (
                        <button class="btn btn-success" onClick={() => setPublicationsToShow(publicationsToShow + publicationsPerPage)} style={{ position: "relative", left: "470px", top: "40px" }}>
                            Mostrar m{'\u00e1'}s
                        </button>
                    )}
                </div>
            );
        } else {

            /* dispatch(restFuntion(true))*/
            return (<h1>Sin resultados</h1>)
        }
    };
    const [data, setData] = useState(null);

    const updateIsFavorited = (publicacionId) => {
        checkIfFavorited(publicacionId)
            .then((isFavorited) => {
                setIsFavorited(prevState => ({
                    ...prevState,
                    [publicacionId]: isFavorited === "True", // Convertir a booleano
                }));
            })
            .catch((error) => {
                console.error("Error al verificar si es favorito:", error);
                setIsFavorited(prevState => ({
                    ...prevState,
                    [publicacionId]: false,
                }));
            });
    };

    useEffect(() => {
        if (hotellist.length > 0) {
            hotellist.forEach((data) => {
                if (data && data.publicacionId) {
                    if (isFavorited[data.publicacionId] === undefined) {
                        updateIsFavorited(data.publicacionId);
                    }
                }
            });
        }
    }, [hotellist]);
    function extra(data) {
        data.propiedad.propiedadTipoAmbiente.forEach((extr) => { return (<label>(extr.tipoAmbiente.nombreTipoAmbiente) : (extr.cantidad)</label>) })
    }
    const publicaciones = (data) => {
        if (data != undefined && data != null && data != 0) {
    




                return (
                    <div key={data.publicacionId} className="hotel-info-div">
                        <img src={data.propiedad.imagenPropiedad[0].rutaImagenPropiedad} alt="img-hotel" />

                        <div className="about-hotel1" style={{ overflow: "hidden" }}>

                            <div className="rat-div">

                                <div className="h3">
                                    <h2>{data.tipoPublicacion.nombreTipoPublicacion}</h2>
                                </div>
                            </div>
                            <hr></hr>
                            <div className="location">
                                <label>
                                    <img src={imagen} alt="arrow" style={{
                                        height: "20px",
                                        width: "20px"
                                    }} />
                                    Ubicaci{'\u00f3'}n {data.propiedad.ubicacion}
                                    <div className="arrow">
                                        <img src={imagen0} alt="arrow" />
                                    </div>
                                </label>
                            </div>
                            <hr></hr>
                            {data.caracteristicas !== null && (
                                <div className="caracteristicas" style={{ display: "flex" }}>
                                    <h4>Caracter{'\u00ED'}sticas:</h4>
                                    <ul>
                                        {data.caracteristicas.slice(0, 3).map((caracteristica, index) => (
                                            <li key={index}>
                                                {caracteristica.caracteristicaId !== null && (
                                                    <Caracteristica caracteristicaId={caracteristica.caracteristicaId} />
                                                )}
                                            </li>
                                        ))}
                                    </ul>
                                </div>
                            )}
                        </div>
                        <div className="about-hotel">
                            <div className="view-detail-div" style={{ display: "block" }}>
                                <div className="redi">
                                    <p>Descripci{'\u00f3'}n</p>
                                </div>
                                <div className="fact">
                                    <div> <p> {data.propiedad.descripcionPropiedad} </p></div>

                                </div>
                                <div className="deal">
                                    <div className="h3p">
                                        <h3 > $ {data.propiedad.precioPropiedad} {data.propiedad.tipoMoneda.denominacionMoneda}</h3>
                                    </div>
                                    <div className="btn">

                                        <a class="btn btn-success" href={`/VisitaInmueble/VisitarPublicacion?publicacionId=${data.publicacionId}`}
                                            onClick={(e) => {
                                                console.log(e.target);
                                            }}
                                        >
                                            Visitar
                                        </a>
                                    </div>
                                </div>
                            </div>
                            <div style={{ display: "flex", justifyContent: "space-evenly", alignItems: "center" }} >
                                <div className="agoda-price-div1">
                                    <div className="hot">
                                        Fecha de publicacion:

                                    </div>

                                    <div className="hotp">
                                        {new Date(data.fechaInicioPublicacion).toLocaleDateString()}
                                    </div>

                                </div>
                                <div> <button class="btn btn-outline-danger" onclick="">
                                    {isFavorited[data.publicacionId] ? (
                                        <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor" class="bi bi-heart-fill" viewBox="0 0 16 16">
                                            <path fill-rule="evenodd" d="M8 1.314C12.438-3.248 23.534 4.735 8 15-7.534 4.736 3.562-3.248 8 1.314z" />
                                        </svg>
                                    ) : (
                                        <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor" class="bi bi-heart-fill" viewBox="0 0 16 16" >
                                            <path d="m8 2.748-.717-.737C5.6.281 2.514.878 1.4 3.053c-.523 1.023-.641 2.5.314 4.385.92 1.815 2.834 3.989 6.286 6.357 3.452-2.368 5.365-4.542 6.286-6.357.955-1.886.838-3.362.314-4.385C13.486.878 10.4.28 8.717 2.01L8 2.748zM8 15C-7.333 4.868 3.279-3.04 7.824 1.143c.06.055.119.112.176.171a3.12 3.12 0 0 1 .176-.17C12.72-3.042 23.333 4.867 8 15z" />
                                        </svg>
                                    )}

                                </button></div></div>
                        </div>
                    </div>

                );


            }
        
    }
    
    

    useEffect(() => {
        hotel = dispatch(getAllHotel("", 1))

    }, []);

    return (
        /*<MiContexto.Provider value={nombre}>*/
        <div class="dev" style={{
            position: "absolute",
            zoom: "86.9%",
            backgroundColor: "rgb(245,245,246)"
        }}>
            <SearchBar />
            <div className="allhotelwrap" >
                <div className="parent-container-allhotels">
                    <div className="sort-div">
                        <label> Ordenar por : </label><select name="hotels" onChange={(e) => {
                            if (e != undefined) {
                                if (e.target != null && e.target != undefined) {
                                    var select = e.target.value
                                    if (select == "menor") {

                                        hotel = dispatch(fliterPrecio())
                                    } if (select == "mayor") { hotel = dispatch(fliterPrecio()) }
                                    if (select == "menor") { hotel = dispatch(fliterPrecio1()) }
                                    if (select == "reciente") { hotel = dispatch(fliterReciente()) }
                                    if (select == "antigua") { hotel = dispatch(fliterviejo()) }
                                }
                            }
                        }}>

                            <option value="default" name="default"  >

                            </option>
                            <option value="mayor" name="Our_recomn" >
                                Precio , de menor a mayor
                            </option>
                            <option value="menor" name="Rating_recomn" >
                                Precio, de mayor a menor
                            </option>
                            <option value="reciente" name="Rating_recomn">
                                Publicaci{'\u00f3'}n m{'\u00e1'}s reciente
                            </option>
                            <option value="antigua" name="Rating_recomn">
                                Publicaci{'\u00f3'}n m{'\u00e1'}s antigua
                            </option>
                            <option value="fav" name="Rating_recomn" >
                                Favoritos
                            </option>
                            {/*<option value="Des" name="Rating_recomn" >*/}
                            {/*    Destacados*/}
                            {/*</option>*/}
                        </select>
                    </div>
                    <br />
                    <br />
                    <div class="contenedor">
                        {hotellist.length == 0 &&
                            <h1 >Sin resultados</h1>

                        }
                        {showPublications(hotellist)}
                        <div className="hotel-info-div" style={{ backgroundColor: "#ebeced" }}>




                            <div className="about-hotel1">
                                <div className="fecha">

                                </div>
                                <div className="location">
                                    <label>

                                    </label>
                                </div>
                                <div className="descripcion">
                                    <label>

                                    </label>
                                </div>

                                <div className="rating">

                                    < div className="revwrap">
                                        <div className="rev">
                                            <b> <br /> </b>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="">
                                <div className="">
                                    <div className="">
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div className="hotel-info-div" style={{ backgroundColor: "#ebeced" }}>




                            <div className="about-hotel1">
                                <div className="fecha">

                                </div>
                                <div className="location">
                                    <label>

                                    </label>
                                </div>
                                <div className="descripcion">
                                    <label>

                                    </label>
                                </div>

                                <div className="rating">

                                    < div className="revwrap">
                                        <div className="rev">
                                            <b> <br /> </b>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="">
                                <div className="">
                                    <div className="">
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div className="hotel-info-div" style={{ backgroundColor: "#ebeced" }}>




                            <div className="about-hotel1">
                                <div className="fecha">

                                </div>
                                <div className="location">
                                    <label>

                                    </label>
                                </div>
                                <div className="descripcion">
                                    <label>

                                    </label>
                                </div>

                                <div className="rating">

                                    < div className="revwrap">
                                        <div className="rev">
                                            <b> <br /> </b>


                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div className="">
                                <div className="">
                                    <div className="">
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
                {/* {elmapa()}*/}
                {hotel.length > 0 && <Mapa style={{ position: "fixed" }} />}
                {hotel.length <= 0 && <StaticMap />}
                {/*   <StaticMap />*/}
            </div>
        </div>
        /*</MiContexto.Provider>*/

    );
}

const dev = styled.div`
 position: absolute;
`;

