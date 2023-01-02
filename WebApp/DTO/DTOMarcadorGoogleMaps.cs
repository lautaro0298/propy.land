using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOMarcadorGoogleMaps
    {
        public Guid publicacionId { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string precioPropiedad { get; set; }
        public string ubicacion { get; set; }
        public string tipoPropiedad { get; set; }
        public string tipoMoneda { get; set; }
    }
}