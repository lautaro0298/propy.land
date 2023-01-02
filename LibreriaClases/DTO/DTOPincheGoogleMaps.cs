using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOPincheGoogleMaps
    {
        public string publicacionId { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string precioPropiedad { get; set; }
        public string ubicacion { get; set; }
        public List<string> tipoPropiedad { get; set; }
        public string tipoMoneda { get; set; }
    }
}
