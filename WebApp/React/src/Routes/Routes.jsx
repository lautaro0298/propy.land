import React from "react";
import { Route, Switch } from "react-router-dom";
import { useState } from "react"
import AllHotels from "../Components/hotels/AllHotels";
import { store } from "../store/Store";
import { Provider } from "react-redux"
import{ LoadScript } from "@react-google-maps/api"

export const Routes = () => {
   
  return (
    <div>
          <Switch>
              <LoadScript
                  googleMapsApiKey="AIzaSyAXxPwQsLSfF4gC7VtJdl9GIIcyAzVdmhk"
                  libraries={["places"]}
              >

              <Provider store={store} >
              <AllHotels />
              </Provider>
              </LoadScript>
          </Switch>
       
              

    </div>
  );
};

export default Routes;
