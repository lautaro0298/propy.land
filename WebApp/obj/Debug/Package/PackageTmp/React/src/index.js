import React, { useState} from "react";
import ReactDOM from "react-dom";
import "./index.css";

import reportWebVitals from "./reportWebVitals";
import { BrowserRouter } from "react-router-dom";
import Routes from "./Routes/Routes";



ReactDOM.render(
    
    <React.StrictMode>
        <BrowserRouter>
                <Routes />
        </BrowserRouter>
    </React.StrictMode>
    
  ,
  document.getElementById("vista")
);

reportWebVitals();
