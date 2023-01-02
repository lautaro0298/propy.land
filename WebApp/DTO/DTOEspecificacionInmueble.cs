using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOEspecificacionInmueble
    {
        public Guid publicacionId { get; set; }
        public string ubicacion { get; set; }
        public string precioPropiedad { get; set; }
        public int cantidadBaños { get; set; }
        public int cantidadDormitorios { get; set; }
        public int cantidadCocheras { get; set; }
        public int cantidadAmbientes { get; set; }
        public List<string> extras { get; set; }
        public string tipoPropiedad { get; set; }
        public string tipoOperacion { get; set; }
        public string tipoConstrucción { get; set; }
        public string tipoMoneda { get; set; }
        public List<string> rutasImagenesBD { get; set; }
        public string PrecioNumberFormat { get; set; }
        public string comentarios { get; set; }

        public DTOEspecificacionInmueble() {
            extras = new List<string>();
            rutasImagenesBD = new List<string>();
        }
    }
}