
import {
  GET_RECENTLY_DATA,
  GET_RECENTLY_FAILURE,
  GET_RECENTLY_REQUEST,
  HOTEL_REQUEST,
  HOTEL_SUCCESS,
  HOTEL_FAILURE,
    HOTELSLIST,
    HOTELSOPERACION,
    HOTELS1,
    PRICE_FILTER,
    HOTELSPRECIO,
    PROPIEDAD,
    RESET
} from "./actionTypes";
const initState = {
  data: [],
  propiedad:"",
  isLoading: false,
  isError: false,
  hotelsList:[],
    hotel: [],
    hotelPrecio: [],
    hotelOperacion: [],
    hotel1: [],
  hotLoad:false,
    hotErr: false,
    reset: false,
  currPage: 1,
  currQuery: null
};

export const RecentlyReducer = (state = initState, { type, payload,currPage, query }) => {

  switch (type) {
    case GET_RECENTLY_DATA: {
      return {
        ...state,
        data: payload,
        isLoading: false,
      };
      }

      case RESET: {
    return {
        ...state,
        reset: payload
    };
        }
    case GET_RECENTLY_REQUEST: {
      return {
        ...state,
        isLoading: true,
        isError: false,
      };
    }
    case GET_RECENTLY_FAILURE: {
      return {
        ...state,
        isLoading: false,
        isError: payload,
      };
    }
    case HOTEL_REQUEST: {
      
      return {
        ...state,
        hotLoad: true,
      };
      }
      case HOTELSPRECIO: {
          return {
              ...state,
              hotelPrecio: payload
          };
      }
      case HOTELSOPERACION: {
          return {
              ...state,
              hotelOperacion: payload
          };
      }
      case HOTELS1: {
          return {
              ...state,
              hotel1: payload
          };
      }
      case HOTELSLIST:{
          return {
              ...state,
              hotelsList:payload
          };
      }
    case HOTEL_SUCCESS: {
      
      return {
        ...state,
        hotLoad: false,
        hotel: payload,
        currPage: currPage,
        query: query

      };
    }
    case HOTEL_FAILURE: {    
      return {
        ...state,
        hotLoad: false,
        
        currPage: currPage,
        query: query
      };
      }
      case PROPIEDAD: {
          return {
              ...state,
              propiedad: payload,
              hotErr: true
            
          };
      }
    default:
      return state;
  }
};
