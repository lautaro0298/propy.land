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
  PRICE_FILTER
} from "./actionTypes";

import axios from "axios";
import AllHotels from "../Components/hotels/AllHotels";
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

    return axios.get(`https://localhost:50001/api/Busqueda/obtenerPropiedadesParaEvaluarBusqueda`)
    .then((res) => {
      dispatch(hotelSuccess(res.data));
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
    axios.get('https://localhost:50001/api/Busqueda/buscardortipo?'+tipoPropiedadId + caracteristicaIds).then((res) => {
            let { data } = res;
            console.log(data);
            dispatch(hotelSuccess(data));
        }).catch(err => {
            dispatch(hotelFailure(err, 1, query));
        })
}
export const addHotelList = (cant ,moneda ) => dispatch => {
    axios.get('https://localhost:50001/api/Busqueda/buscardorPorPrecio?cant=' + cant + '&moneda=' + moneda).then((res) => {
        let { data } = res;
        dispatch(hotelSuccess(data));
    })
}
export const tipoPublicante = (tipoPublicante) => dispatch => {
    axios.get('https://localhost:50001/api/Busqueda/buscardorPorPublicancion?Publicacion='  + tipoPublicante).then((res) => {
            let { data } = res;
            dispatch(hotelSuccess(data));
        })
}
export const fliterDormitorios = (values) => dispatch => {
    let hotel = store.getState().activities.hotel;
    let cant = Object.values(values);
    let cocheras=Number(cant[0]);
    let habitaciones = Number(cant[1]);
    axios.get('https://localhost:50001/api/Busqueda/buscardorAmbientes?cantCocheras=' + cocheras + '&caracteristicaId=' + habitaciones).then((res) => {
        let { data } = res;       
        let filtro = hotel.filter(res => { return data.find(element => { return element == res.propiedadId }) })
        dispatch(hotelSuccess(filtro));  
    })
}
export const fliterPrecio = () => dispatch => {
    let hotel = store.getState().activities.hotel;
    hotel.sort(((a, b) => a.propiedad.precioPropiedad - b.propiedad.precioPropiedad));
    dispatch(hotelSuccess(hotel));
    dispatch(hotelList(hotel));
    console.log(hotel)
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
  
    axios.get(`https://localhost:50001/api/Busqueda/buscardortipoPropiedad?tipoPropiedadId=` +tipoPropiedad).then((res) => {
      let { data } = res;
      dispatch(hotelSuccess(data));
    }).catch(err => {
      dispatch(hotelFailure(err, 1));
    })

}
export const palabra = (Palabra) => dispatch => {
    dispatch(hotelRequest())
    if (Palabra == undefined || Palabra == null || Palabra == "") {
        axios.get(`https://localhost:50001/api/Busqueda/obtenerPropiedadesParaEvaluarBusqueda`)
        .then((res) => {
            dispatch(hotelSuccess(res.data));
        })
        .catch((err) => {
            const failureAction = hotelFailure(err, currPage, query);
            dispatch(failureAction);
        }); } else {
        axios.get(`https://localhost:50001/api/Busqueda/buscardorPorPalabra?Palabra=` + Palabra).then((res) => {
            let { data } = res;
            dispatch(hotelSuccess(data));
        }).catch(err => {
            dispatch(hotelFailure(err, 1));
        })
    }
    
}