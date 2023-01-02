using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOPosicion
    {
        public double latitud { get; set; }
        public double longitud { get; set; }
        public DTOPosicion(double lat, double lng)
        {
            latitud = lat;
            longitud = lng;
        }
    }
}
