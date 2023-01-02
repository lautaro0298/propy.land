using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOValidarParametrosFiltrosBusqueda
    {
        public decimal precioDesde { get; set; }
        public decimal precioHasta { get; set; }
        public int SuperficieTerrenoMax { get; set; }
        public int SuperficieTerrenoMin { get; set; }
    }
}