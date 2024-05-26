export const GET_RECENTLY_DATA = "GET_RECENTLY_DATA";
export const GET_RECENTLY_REQUEST = "GET_RECENTLY_REQUEST";
export const GET_RECENTLY_FAILURE = "GET_RECENTLY_FAILURE";

export const HOTEL_SUCCESS = "HOTEL_SUCCESS";
export const HOTEL_REQUEST = "HOTEL_REQUEST";
export const HOTEL_FAILURE = "HOTEL_FAILURE";

export const HOTELSLIST = "HOTELSLIST";
export const HOTELSPRECIO = "HOTELSPRECIO";
export const HOTELSOPERACION = "HOTELSOPERACION";
export const HOTELS1 = "HOTELS1";
export const PROPIEDAD = "PROPIEDAD";
export const RESET = "RESET";


import {
  GET_RECENTLY_DATA,
  GET_RECENTLY_REQUEST,
  GET_RECENTLY_FAILURE,

  HOTEL_SUCCESS,
  HOTEL_REQUEST,
  HOTEL_FAILURE,

  HOTELSLIST,
  HOTELSPRECIO,
  HOTELSOPERACION,
  HOTELS1,
  PROPIEDAD,
  RESET,
} from './actionTypes';

export const getRecentlyData = () => ({
  type: GET_RECENTLY_DATA,
});

export const getRecentlyRequest = () => ({
  type: GET_RECENTLY_REQUEST,
});

export const getRecentlyFailure = (error) => ({
  type: GET_RECENTLY_FAILURE,
  payload: error,
});

// Add other action creators following the same pattern
