import React from "react";
import { GoogleMap, LoadScript } from '@react-google-maps/api';

function Map() {
    const center = {

        lat: -34.0000000,
        lng: -64.000000
    };
  return (
      //<div>
      //    <h1>Sin resultados </h1>
      //    </div>
      <LoadScript
          googleMapsApiKey="AIzaSyDHXJNkL77-_eh9GRL1pZr1EAHrAh_uQR4"
      >
      <div style={{ position: "fixed", left: "900px" }}>
          <GoogleMap id="google-map" style={{ position: "fixed" }} class="google-map" mapContainerStyle={{

              position: 'fixed',
              width: "600px",
              height: "400px",

              /*left: "900px",*/

          }} center={center} zoom={3}
               >

              
          </GoogleMap>

      </div>
          </LoadScript 
          >

  );
}

export default React.memo(Map);