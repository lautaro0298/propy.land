using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOPosicion
    {
        public double latitud { get; set; }
        public double longitud { get; set; }
        public DTOPosicion(double lat, double lng) {
            latitud = lat;
            longitud = lng;
        }
    }
}