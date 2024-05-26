import React from "react";
import { Route, Switch, BrowserRouter } from "react-router-dom";
import { useState } from "react"
import AllHotels from "../Components/hotels/AllHotels";
import { store } from "../store/Store";
import { Provider } from "react-redux"

export const Routes = () => {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <Switch>
          <Route path="/" component={AllHotels} />
        </Switch>
      </BrowserRouter>
    </Provider>
  );
};

export default Routes;
