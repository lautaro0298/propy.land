import { store } from "./Store";
import { shallowEqual, useDispatch, useSelector } from "react-redux";
import {
  GET_RECENTLY_DATA,
  GET_RECENTLY_REQUEST,
  GET_RECENTLY_FAILURE,
  HOTEL_REQUEST,
  HOTEL_SUCCESS,
    HOTEL_FAILURE,
    HOTELSLIST,
    HOTELSPRECIO,
    HOTELSOPERACION,
    HOTELS1,
    PRICE_FILTER,
    PROPIEDAD,
    RESET
} from "./actionTypes";

import axios from "axios";


    const propiedad = (payload) => {
        return {
            type: PROPIEDAD,
            payload: payload
           
        };
    }

const hotelprecio = (payload) => {
    return {
        type: HOTELSPRECIO,
        payload: payload
    };
};
const hoteloperacion = (payload) => {
    return {
        type: HOTELSOPERACION,
        payload: payload
    };
};
const hotel1 = (payload) => {
    return {
        type: HOTELS1,
        payload: payload
    };
};
const hotelList = (payload) => {
    return {
        type: HOTELSLIST,
        payload: payload
    };
};
const getDataRequest = () => {
  return {
    type: GET_RECENTLY_REQUEST,
  };
};
const rest = (payload) => {
    return {
        type: RESET,
        payload: payload
       
    };
};
const getDataSuccess = (payload) => {
  return {
    type: GET_RECENTLY_DATA,
    payload: payload,
  };
};
const getDataFailure = (err) => {
  return {
    type: GET_RECENTLY_FAILURE,
    payload: err,
    };
   
};
export const getDetails = (dispatch) => {
  const requestAction = getDataRequest();
  dispatch(requestAction);
  axios
    .get("/recentlyVisited")
    .then((res) => {
      const successAction = getDataSuccess(res.data);
      dispatch(successAction);
    })
    .catch((err) => {
      const failureAction = getDataFailure();
      dispatch(failureAction);
    });
};
export const getAllDetails = (dispatch) => {
  const requestAction = getDataRequest();
  dispatch(requestAction);
  axios
    .get("/recentlyViewed")
    .then((res) => {
      const successAction = getDataSuccess(res.data);
      dispatch(successAction);
    })
    .catch((err) => {
      const failureAction = getDataFailure();
      dispatch(failureAction);
    });
};

const hotelRequest = () => {
  return {
    type: HOTEL_REQUEST,
  };
};
const hotelSuccess = (payload, currPage, query) => {
  return {
    type: HOTEL_SUCCESS,
    payload: payload,
    currPage,
    query
  };
};
const hotelFailure = (err, currPage, query) => {
  return {
    type: HOTEL_FAILURE,
    payload: err,
    currPage,
    query
  };
};

export const getAllHotel = (query = null, currPage = 1) => dispatch => {
  const requestAction = hotelRequest();
  dispatch(requestAction);

    return axios.get(`http://propyy.somee.com/api/Busqueda/obtenerPropiedadesParaEvaluarBusqueda`)
    .then((res) => {
        dispatch(hotelSuccess(res.data));
        dispatch(hotelprecio(res.data));

        
    })
    .catch((err) => {
      const failureAction = hotelFailure(err, currPage, query);
      dispatch(failureAction);
    });
};

export const priceFilter = (payload, query) => dispatch => {     

      let caracteristicaIds;
      let tipoPropiedadId;
    for (var i = 0; i < payload.length; i++) {
        if (tipoPropiedadId == undefined) { tipoPropiedadId = "tipoPropiedadId=" + payload[i] }
        else { tipoPropiedadId = tipoPropiedadId + "&tipoPropiedadId=" + payload[i] }
    }
    for (var i = 0; i < query.length; i++) {
        if (caracteristicaIds == undefined){ caracteristicaIds = "&caracteristicaId=" + query[i]; }
        else { caracteristicaIds = caracteristicaIds + "&caracteristicaId=" + query[i]; }
    }
    axios.get('http://propyy.somee.com/api/Busqueda/buscardortipo?'+tipoPropiedadId + caracteristicaIds).then((res) => {
            let { data } = res;
        console.log(data);
        dispatch(hotel1(data));
        dispatch(hotelprecio(data)); 
        dispatch(hotelList(data));
        dispatch(rest(true));
            dispatch(hotelSuccess(data));
        }).catch(err => {
            dispatch(hotelFailure(err, 1, query));
        })
}


export const addHotelList2 = (moneda) => (dispatch) => {
    let hotel1 = store.getState().activities.hotelOperacion;
    let hotel2 = store.getState().activities.hotel1;
    let hotel3 = store.getState().activities.hotellist
    let hotel4 = store.getState().activities.hotelPrecio
    let hotel = store.getState().activities.hotel
    if (hotel.length == 0 && (hotel == undefined || hotel1.length==0) && hotel2.length == 0 && hotel3 == undefined && hotel4.length == 0) {
        axios.get('http://propyy.somee.com/api/Busqueda/obtenerPropiedadesParaEvaluarBusqueda')
            .then((res) => {
                let arrayObjeto = res.data;

                axios.get('https://api.bluelytics.com.ar/v2/latest')
                    .then((res) => {
                        let cotizacionData = res.data;
                        let cotizacionMoneda;
                        if (moneda === 'ARS') {
                            cotizacionMoneda = cotizacionData.blue.value_sell;
                        } else {
                            cotizacionMoneda = 1 / cotizacionData.blue.value_sell;
                        }

                        const result = arrayObjeto.map(obj => {
                            if (obj.propiedad.tipoMoneda.denominacionMoneda === 'ARS' && moneda === 'USD') {
                                obj.propiedad.precioPropiedad = Math.trunc(obj.propiedad.precioPropiedad * cotizacionMoneda);
                            } else if (obj.propiedad.tipoMoneda.denominacionMoneda === 'USD' && moneda === 'ARS') {
                                obj.propiedad.precioPropiedad = Math.trunc(obj.propiedad.precioPropiedad * cotizacionMoneda);
                            }
                            obj.propiedad.tipoMoneda.denominacionMoneda = moneda;
                            return obj;
                        });
                        dispatch(rest(true));
                        dispatch(hotelSuccess(result));
                        dispatch(hotelList(result));
                    })
                    .catch(error => {
                        console.error('Error al obtener los datos de la API:', error);
                    });
            })
            .catch(error => {
                console.error('Error al obtener los datos de la API:', error);
            });
    } else {
        if (hotel.length == 0 || hotel == undefined) { if (hotel1.length == 0 || hotel1 == undefined) { if (hotel2.length == 0 || hotel2 == undefined) { if (hotel3 == undefined ||hotel3.length == 0  ) { hotel=hotel4 } else { hotel = hotel3 } } else { hotel=hotel2 } } else { hotel=hotel1 } }
            let arrayObjeto = hotel;

            axios.get('https://api.bluelytics.com.ar/v2/latest')
                .then((res) => {
                    let cotizacionData = res.data;
                    let cotizacionMoneda;
                    if (moneda === 'ARS') {
                        cotizacionMoneda = cotizacionData.blue.value_sell;
                    } else {
                        cotizacionMoneda = 1 / cotizacionData.blue.value_sell;
                    }

                    const result = arrayObjeto.map(obj => {
                        if (obj.propiedad.tipoMoneda.denominacionMoneda === 'ARS' && moneda === 'USD') {
                            obj.propiedad.precioPropiedad = Math.trunc(obj.propiedad.precioPropiedad * cotizacionMoneda);
                        } else if (obj.propiedad.tipoMoneda.denominacionMoneda === 'USD' && moneda === 'ARS') {
                            obj.propiedad.precioPropiedad = Math.trunc(obj.propiedad.precioPropiedad * cotizacionMoneda);
                        }
                        obj.propiedad.tipoMoneda.denominacionMoneda = moneda;
                        return obj;
                    });
                    dispatch(rest(false));
                    dispatch(hotelSuccess(result));
                    dispatch(hotelList(result));
                })
                .catch(error => {
                    console.error('Error al obtener los datos de la API:', error);
                });
        }
}
        
export const fliterPrecio2 = (cant) => dispatch => {
  
    
        
        let hotel = store.getState().activities.hotelPrecio;
    
    
    let filtro = hotel.filter(res => {
        return res.propiedad.precioPropiedad <= cant;

    });
    dispatch(rest(false));

    dispatch(hotelSuccess(filtro));
    dispatch(hotelList(filtro));
    
    
}
export const addHotelList = (cant ,moneda ) => dispatch => {
    axios.get('http://propyy.somee.com/api/Busqueda/buscardorPorPrecio?cant=' + cant + '&moneda=' + moneda).then((res) => {
        let { data } = res;
        hotelprecio
        dispatch(hotelprecio(data));
        dispatch(hotelSuccess(data));
    })
}
export const tipoPublicante = (tipoPublicante) => dispatch => {
    let hotel1 = store.getState().activities.hotelOperacion;
    let hotel2 = store.getState().activities.hotel1;
    let hotel3 = store.getState().activities.hotellist
    let hotel4 = store.getState().activities.hotelPrecio
    let hotel = store.getState().activities.hotel
    if (hotel.length == 0 && hotel1.lenght == undefined && hotel2.length == 0 && hotel.length == 0 && hotel3 == undefined && hotel4.length == 0) {
        axios.get('http://propyy.somee.com/api/Busqueda/buscardorPorPublicancion?Publicacion=' + tipoPublicante).then((res) => {
            let { data } = res;
            dispatch(hotelprecio(data));
            dispatch(hotelList(data));
            dispatch(hoteloperacion(data));
            dispatch(hotelSuccess(data));
               dispatch(hotel1(data));
        })
    } else {

        // if (hotel.length == 0 || hotel == undefined) { if (hotel1.length == 0 || hotel1 == undefined) { if (hotel2.length == 0 || hotel2 == undefined) { if (hotel3 == null || hotel3 == undefined || hotel3.lenght == 0) { hotel = hotel4 } else { hotel = hotel3 } } else { hotel = hotel2 } } else { hotel = hotel1 } }

        let tipoPublicanteFiltrar = tipoPublicante; //
        let resultadosFiltrados = hotel.filter(objeto => objeto.tipoPublicacion.nombreTipoPublicacion === tipoPublicanteFiltrar);
        if (resultadosFiltrados != undefined) {
            dispatch(hotelprecio(resultadosFiltrados));
            dispatch(hotelList(resultadosFiltrados));
            dispatch(hoteloperacion(resultadosFiltrados));
            dispatch(hotelSuccess(resultadosFiltrados));

            dispatch(hotel1(resultadosFiltrados));
        } else {
            dispatch(hotelprecio([]));
            dispatch(hotelList([]));
            dispatch(hoteloperacion([]));
            dispatch(hotelSuccess([]));

            dispatch(hotel1([]));
        }
    }
}
export const fliterDormitorios = (values) => dispatch => {
    let hotel2 = store.getState().activities.hotelOperacion;
    let hotel11 = store.getState().activities.hotel1;
    let hotel = store.getState().activities.hotel;
    let hotel3 = store.getState().activities.hotellist
    let hotel1 = store.getState().activities.hotelPrecio
    let cant = Object.values(values);
    let cocheras = Number(cant[0]);
    let banos = Number(cant[0]);
    let habitaciones = Number(cant[1]);
    if (hotel.length == 0 && hotel1.length == 0 && hotel2.length == 0 && hotel11.length == 0) {
        axios.get('http://propyy.somee.com/api/Busqueda/obtenerPropiedadesParaEvaluarBusqueda' ).then((res) => {
            let { data } = res;
            // Filtrar los objetos del array hotel que cumplan las condiciones
            let filtro = data.filter(objeto => {
                // Verificar si el objeto tiene la propiedad "propiedadTipoAmbiente"
                if (objeto.propiedad && objeto.propiedad.propiedadTipoAmbiente) {
                    // Filtrar los elementos de "propiedadTipoAmbiente" que cumplan las condiciones de cocheras
                    let cocherasFiltradas = objeto.propiedad.propiedadTipoAmbiente.filter(ambiente => {
                        return ambiente.tipoAmbiente.nombreTipoAmbiente === "Cocheras" && ambiente.cantidad >= cocheras;
                    });

                    // Filtrar los elementos de "propiedadTipoAmbiente" que cumplan las condiciones de habitaciones
                    let habitacionesFiltradas = objeto.propiedad.propiedadTipoAmbiente.filter(ambiente => {
                        return ambiente.tipoAmbiente.nombreTipoAmbiente === "Dormitorios" && ambiente.cantidad >= habitaciones;
                    });
                    let BanosFiltradas = objeto.propiedad.propiedadTipoAmbiente.filter(ambiente => {
                        return ambiente.tipoAmbiente.nombreTipoAmbiente === "Ba\u00f1os" && ambiente.cantidad >= banos;
                    });
                    if (banos === 0 && BanosFiltradas.length === 0) {
                        return true;
                    }
                    // Verificar si cocheras es 0 y no hay elementos de "Cocheras" en propiedadTipoAmbiente
                    if (cocheras === 0 && cocherasFiltradas.length === 0) {
                        return true;
                    }

                    // Verificar si habitaciones es 0 y no hay elementos de "Dormitorios" en propiedadTipoAmbiente
                    if (habitaciones === 0 && habitacionesFiltradas.length === 0) {
                        return true;
                    }

                    // Devolver el objeto si cumple ambas condiciones (cocheras y habitaciones)
                    return cocherasFiltradas.length > 0 && habitacionesFiltradas.length > 0 && BanosFiltradas.length > 0;
                } else {
                    // Si el objeto no tiene "propiedadTipoAmbiente", devolverlo si cocheras o habitaciones es 0
                    return cocheras === 0 || habitaciones === 0 || banos ===0;
                }
            });
            dispatch(hotelSuccess(filtro));

            dispatch(hotelList(filtro));
        })
    }
    else {
        if (hotel.length == 0) {
            if (hotel3 == undefined) {
                if (hotel11.lengt == 0) {
                    if (hotel1.length == 0) { hotel = hotel2 } else (hotel = hotel1)
                } else { hotel = hotel11 }
            } else { hotel = hotel3 }
        }
        
        let resultadosFiltrados  = hotel.filter(objeto => {
            // Verificar si el objeto tiene la propiedad "propiedadTipoAmbiente"
            if (objeto.propiedad && objeto.propiedad.propiedadTipoAmbiente) {
                // Filtrar los elementos de "propiedadTipoAmbiente" que cumplan las condiciones de cocheras
                let cocherasFiltradas = objeto.propiedad.propiedadTipoAmbiente.filter(ambiente => {
                    return ambiente.tipoAmbiente.nombreTipoAmbiente === "Cocheras" && ambiente.cantidad >= cocheras;
                });

                // Filtrar los elementos de "propiedadTipoAmbiente" que cumplan las condiciones de habitaciones
                let habitacionesFiltradas = objeto.propiedad.propiedadTipoAmbiente.filter(ambiente => {
                    return ambiente.tipoAmbiente.nombreTipoAmbiente === "Dormitorios" && ambiente.cantidad >= habitaciones;
                });
                let BanosFiltradas = objeto.propiedad.propiedadTipoAmbiente.filter(ambiente => {
                    return ambiente.tipoAmbiente.nombreTipoAmbiente === "Ba\u00f1os" && ambiente.cantidad >= banos;
                });
                if (banos === 0 && BanosFiltradas.length === 0) {
                    return true;
                }
                // Verificar si cocheras es 0 y no hay elementos de "Cocheras" en propiedadTipoAmbiente
                if (cocheras === 0 && cocherasFiltradas.length === 0) {
                    return true;
                }

                // Verificar si habitaciones es 0 y no hay elementos de "Dormitorios" en propiedadTipoAmbiente
                if (habitaciones === 0 && habitacionesFiltradas.length === 0) {
                    return true;
                }

                // Devolver el objeto si cumple ambas condiciones (cocheras y habitaciones)
                return cocherasFiltradas.length > 0 && habitacionesFiltradas.length > 0 &&BanosFiltradas.lenght > 0 ;
            } else {
                // Si el objeto no tiene "propiedadTipoAmbiente", no cumple las condiciones
                return false;
            }
        });
        dispatch(hotelSuccess(resultadosFiltrados));
        dispatch(hotelList(resultadosFiltrados));
    
    }
   
    
}

export const fliterPrecio = () => dispatch => {
    let hotel = store.getState().activities.hotel;
    hotel.sort(((a, b) => a.propiedad.precioPropiedad - b.propiedad.precioPropiedad));
    dispatch(hotelSuccess(hotel));
    dispatch(hotelList(hotel));
    
}
export const fliterviejo = () => dispatch => {
    let hotel = store.getState().activities.hotel;
    let dia = hotel[0].fechaInicioPublicacion;
    let dia1 = new Date(dia).getTime();
    console.log(dia1);
    let corto = hotel.sort((a, b) => new Date(a.fechaInicioPublicacion).getTime() - new Date(b.fechaInicioPublicacion).getTime())
    console.log(corto);
    dispatch(hotelSuccess(hotel));
    dispatch(hotelList(hotel));
    
}
export const fliterReciente = () => dispatch => {
    let hotel = store.getState().activities.hotel;

    console.log(hotel.sort((a, b) => new Date(a.fechaInicioPublicacion).getTime() - new Date(b.fechaInicioPublicacion).getTime()));
    hotel = hotel.reverse();
    dispatch(hotelSuccess(hotel));
    dispatch(hotelList(hotel));

}
export const hotellist = (hotellist) => dispatch => {
    dispatch(hotelList(hotellist));
   
}

export const fliterPrecio1 = () => dispatch => {
    dispatch(fliterPrecio());
    let hotel = store.getState().activities.hotel;
   hotel= hotel.reverse();
    dispatch(hotelSuccess(hotel));
    dispatch(hotelList(hotel));
    console.log(hotel)
}
export const sortHotelData = (tipoPropiedad) => dispatch => {
  dispatch(hotelRequest())
    
    axios.get(`http://propyy.somee.com/api/Busqueda/buscardortipoPropiedad?tipoPropiedadId=` +tipoPropiedad).then((res) => {
        let filteredData
        let { data } = res;
        let hotel = store.getState().activities.hotelOperacion;
        console.log(data);
        if (hotel != 0) {
             filteredData = data.filter((itemData) => {
                return hotel.some((itemHotel) => itemHotel.propiedadId === itemData.propiedadId && itemHotel.publicacionId === itemData.publicacionId);
            });
        } else { filteredData = data }
     

        dispatch(hotel1(filteredData));
        dispatch(hotelList(filteredData));
        dispatch(hotelSuccess(filteredData));
        
        dispatch(propiedad(tipoPropiedad))
        dispacth(rest(true))
    }).catch(err => {
      dispatch(hotelFailure(err, 1));
    })

}
export const palabra = (Palabra) => dispatch => {
    dispatch(hotelRequest())
    if (Palabra == undefined || Palabra == null || Palabra == "") {
        axios.get(`http://propyy.somee.com/api/Busqueda/obtenerPropiedadesParaEvaluarBusqueda`)
        .then((res) => {
            dispatch(hotelSuccess(res.data));
        })
        .catch((err) => {
            const failureAction = hotelFailure(err, currPage, query);
            dispatch(failureAction);
        }); } else {
        axios.get(`http://propyy.somee.com/api/Busqueda/buscardorPorPalabra?Palabra=` + Palabra).then((res) => {
            let { data } = res;
            dispatch(hotelSuccess(data));
        }).catch(err => {
            dispatch(hotelFailure(err, 1));
        })
    }
    
}