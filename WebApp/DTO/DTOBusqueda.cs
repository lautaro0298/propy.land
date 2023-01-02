using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOBusqueda
    {

        public string pais { get; set; }
        public string areaAdministrativaNivel1 { get; set; }
        public string areaAdministrativaNivel2 { get; set; }
        public string tipoOperacion { get; set; }
        
        public string tipoConstruccion { get; set; }
        public decimal precio { get; set; }
        public string tipoPropiedad { get; set; }
        public int cantidadBaños { get; set; }
        public int cantidadDormitorios { get; set; }
        public int cantidadCocheras { get; set; }
        public int antiguedad { get; set; }
        public int cantidadPlantas { get; set; }
        public List<string> extras { get; set; }
        public decimal precioDesde { get; set; }
        public decimal precioHasta { get; set; }

        public string rutaRelativa { get; set; }
        public double latitudOrigen { get; set; }
        public double longitudOrigen { get; set; }
        public int radioElegido { get; set; }

        public int superficieTerrenoMax { get; set; }
        public int superficieTerrenoMin { get; set; }
        public int cantidadAmbientes { get; set; }
    }
}