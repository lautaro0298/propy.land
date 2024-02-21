import React from "react";
import ReactDOM from "react-dom";
import  InfoWindow   from '@react-google-maps/api';

export default function InfoWindowEx({ lugar }) {


  //function  componentDidUpdate(prevProps) {
  //      if (this.props.children !== prevProps.children) {
  //          ReactDOM.render(
  //              React.Children.only(this.props.children),
  //              this.contentElement
  //          );
  //          this.infoWindowRef.current.infowindow.setContent(this.contentElement);
  //      }
  //  }

    return (
        <InfoWindow position={{ lat: lugar.latitud, lng: lugar.longitud }} >
            <h1>hola
                </h1>
            </InfoWindow>

    );
}