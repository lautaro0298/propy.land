import React from "react";
import "./AllHotels.css";
import styled from "styled-components";
import { shallowEqual, useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { SearchBar } from "../Search-Bar/SearchBar";
import { getAllHotel, fliterPrecio } from '../../store/actions';
import Mapa from "../Map/Mapa";
import StaticMap from "../Map/StaticMap";
import { GoogleMap, InfoBox } from '@react-google-maps/api';
const initState = {
    data: [],
    isLoading: false,
    isError: false,
    hotel: [],
    hotLoad: false,
    hotErr: false,
    currPage: 1,
    currQuery: null
};
export default function AllHotels() {
  
    
    // aca usamos la context de react 
    const dispatch = useDispatch();
    let hotelState = useSelector((state) => state.activities, shallowEqual);
    let hotel = hotelState.hotel;
    let hotellist = hotelState.hotelsList;
    function extra(data) {
        data.propiedad.propiedadTipoAmbiente.forEach((extr) => { return (<label>(extr.tipoAmbiente.nombreTipoAmbiente) : (extr.cantidad)</label>)})}
    const publicaciones = (data) => {
        if (data != undefined && data != null) {
           
            return (
                <div key={data.publicacionId} className="hotel-info-div">

                    <img src={data.propiedad.imagenPropiedad[0].rutaImagenPropiedad} alt="img-hotel" />


                    <div className="about-hotel1">
                        <div className="fecha">
                            Fecha de publicacion: {new Date(data.fechaFinPublicacion).toLocaleDateString()}
                        </div>
                        <div className="location">
                            <label>
                                Precio: $ {data.propiedad.precioPropiedad}  {data.propiedad.tipoMoneda.denominacionMoneda}
                            </label>
                        </div>
                        <div className="descripcion">
                            <label>
                                Descripcion:  {data.propiedad.descripcionPropiedad}
                            </label>
                        </div>

                        <div className="rating">
                            < div className="revwrap">
                                <div className="rev">
                                    <b> Ubicacion:<br /> {data.propiedad.ubicacion}</b>
                                </div>
                                <br />
                            </div>
                        </div>
                       <br/>
                       
                    </div>
                    <div className="tipoPublicacion">
                        <label>
                            Tipo de publicacion:  {data.tipoPublicacion.nombreTipoPublicacion}
                        </label>
                        {/*<div className="extras">*/}
                        {/*        Extras:{*/}
                                
                        {/*            extra(data)*/}
                                
                        {/*   }*/}
                        {/*</div>*/}
                    </div>
                    
                    <div className="about-hotel">
                        <div className="view-detail-div">
                            <div className="deal">

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

                    </div>
                </div>
            );
        }
    }
    function elmapa() {
        if (hotel != undefined) {
            if (hotel.length > 0) {
                return (
                     <Map />);
            }
        }
    }
    useEffect(() => {
        hotel = dispatch(getAllHotel("", 1))
        
    }, []);

    

    const handleFilter = (e) => {
       hotel= dispatch(fliterPrecio(e.target.value))
        
  };
    
  return (
      
          <div class="dev" style={{
              position: "absolute",
              backgroundColor: "rgb(245,245,246)"
          }}>
      <SearchBar />
      <div className="allhotelwrap" >
        <div className="parent-container-allhotels">
                      <div className="sort-div">
                          Ordenar por :<select name="hotels" onChange={handleFilter}>
              <br/>
              <option value="mayor" name="Our_recomn">
                Precio: Mayor - Menor
              </option>
              <option value="menor" name="Rating_recomn">
                Precio: Menor -Mayor 
              </option>
            </select>
          </div>
                  <br />
                      <br />
        <div class="contenedor">
                          {hotellist.length > 0 && hotellist.map((data) => (
                              publicaciones(data)
              
              
                          ))}
                          
                          <div className="hotel-info-div" style={{ backgroundColor: "#ebeced"}}>

                              


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
                          {/*<div className="hotel-info-div" style={{ backgroundColor: "#ebeced" }}>*/}




                          {/*    <div className="about-hotel1">*/}
                          {/*        <div className="fecha">*/}

                          {/*        </div>*/}
                          {/*        <div className="location">*/}
                          {/*            <label>*/}

                          {/*            </label>*/}
                          {/*        </div>*/}
                          {/*        <div className="descripcion">*/}
                          {/*            <label>*/}

                          {/*            </label>*/}
                          {/*        </div>*/}

                          {/*        <div className="rating">*/}

                          {/*            < div className="revwrap">*/}
                          {/*                <div className="rev">*/}
                          {/*                    <b> <br /> </b>*/}


                          {/*                </div>*/}
                          {/*            </div>*/}
                          {/*        </div>*/}
                          {/*    </div>*/}
                          {/*    <div className="">*/}
                          {/*        <div className="">*/}
                          {/*            <div className="">*/}
                          {/*            </div>*/}
                          {/*        </div>*/}

                          {/*    </div>*/}
                          {/*</div>*/}
                          </div>
                
              </div>
             {/* {elmapa()}*/}
                  {hotel.length > 0 && <Mapa />}
                  {hotel.length <= 0 && <StaticMap />}
                  
                  </div>
              </div>

  );
}

const dev = styled.div`
 position: absolute;
`;

