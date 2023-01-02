using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOPropiedad
    {
        public string propiedadId { get; set; }
        public decimal precioPropiedad { get; set; }
        public string direccionPropiedad { get; set; }
        public int nroPisos { get; set; }
    }
}