import React from "react";
import { Route, Switch } from "react-router-dom";
import { useState } from "react"
import AllHotels from "../Components/hotels/AllHotels";
import { store } from "../store/Store";
import { Provider } from "react-redux"
export const Routes = () => {
   
  return (
    <div>
      <Switch>
              <Provider store={store} >
              <AllHotels />
              </Provider>
      </Switch>
   
    </div>
  );
};

export default Routes;
