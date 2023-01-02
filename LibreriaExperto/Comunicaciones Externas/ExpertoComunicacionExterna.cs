using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.DTOJSon;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace LibreriaExperto.Comunicaciones_Externas
{
    public static class ExpertoComunicacionExterna
    {
        public static (ErrorPropy, DTOUbicacionGoogle) ObtenerParametrosUbicacionGoogleMaps(string ubicacion, HttpClient clienteHttp,bool busqueda)
        {
            ErrorPropy error = new ErrorPropy();
            string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + ubicacion + "&key=AIzaSyDHXJNkL77-_eh9GRL1pZr1EAHrAh_uQR4";
            var tareaObtenerParametrosUbicacion = clienteHttp.GetAsync(url);
            tareaObtenerParametrosUbicacion.Wait();
            if (!tareaObtenerParametrosUbicacion.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerParametrosUbicacion.Result.StatusCode.ToString());
            }
            
            DTOUbicacionGoogle ubicacionGoogle = new DTOUbicacionGoogle();

            DTORootObjectAddress respuesta = tareaObtenerParametrosUbicacion.Result.Content.ReadAsAsync<DTORootObjectAddress>().Result;
            if (respuesta.status != "OK")
            {
                error.codigoError = -1;
                error.descripcionError = "Debe ingresar una dirección válida.";
                return (error, null);
            }
            ubicacionGoogle.latitud = respuesta.results[0].geometry.location.lat;
            ubicacionGoogle.longitud = respuesta.results[0].geometry.location.lng;
            ubicacionGoogle.ubicacionFormateada = respuesta.results[0].formatted_address;
            for (int i = 0; i < respuesta.results[0].address_components.Count; i++)
            {
                switch (respuesta.results[0].address_components[i].types[0])
                {
                    
                    case "administrative_area_level_2":
                        ubicacionGoogle.areaAdministrativaNivel2 = respuesta.results[0].address_components[i].long_name;
                        if (String.IsNullOrEmpty(ubicacionGoogle.areaAdministrativaNivel2) && busqueda==false)
                        {
                            error.codigoError = -1;
                            error.descripcionError = "Debe ingresar una dirección válida. Indique la calle y la numeración exacta de su local gastronómico.";
                            return (error, null);
                        }
                        break;
                    case "administrative_area_level_1":
                        ubicacionGoogle.areaAdministrativaNivel1 = respuesta.results[0].address_components[i].long_name;
                        if (String.IsNullOrEmpty(ubicacionGoogle.areaAdministrativaNivel1)&&busqueda==false)
                        {
                            error.codigoError = -1;
                            error.descripcionError = "Debe ingresar una dirección válida. Indique la calle y la numeración exacta de su local gastronómico.";
                            return (error, null);
                        }
                        break;
                    case "country":
                        ubicacionGoogle.pais = respuesta.results[0].address_components[i].long_name;
                        if (String.IsNullOrEmpty(ubicacionGoogle.pais)&&busqueda==false)
                        {
                            error.codigoError = -1;
                            error.descripcionError = "Debe ingresar una dirección válida. Indique la calle y la numeración exacta de su local gastronómico.";
                            return (error, null);
                        }
                        break;
                    
                }
            }
            return (error, ubicacionGoogle);
        }
    }
}
