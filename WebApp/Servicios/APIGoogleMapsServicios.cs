using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WebApp.DTOJSon;

namespace WebApp.Servicios
{
    public static class APIGoogleMapsServicios
    {
        public static DTORootObjectId SolicitarIdentificadorUbicacion(string url) {
            string respuesta = ComunicacionApiServicios.GetHTTP(url);
            DTORootObjectId rootObj = JsonConvert.DeserializeObject<DTORootObjectId>(respuesta);
            return rootObj;
        }
        public static DTORootObjectAddress SolicitarUbicacionCompleta(string url) {
            string respuesta = ComunicacionApiServicios.GetHTTP(url);
            DTORootObjectAddress rootObjAddress = JsonConvert.DeserializeObject<DTORootObjectAddress>(respuesta);
            return rootObjAddress;
        }
        
    }
}