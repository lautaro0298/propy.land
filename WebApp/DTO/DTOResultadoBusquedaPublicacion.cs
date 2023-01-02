using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.DTO
{
    public class DTOResultadoBusquedaPublicacion
    {
        public Guid publicacionId { get; set; }
        public string propiedadId { get; set; }
        public string direccionPropiedad { get; set; }
        public int nroCalle { get; set; }
        public String precioPropiedad { get; set; }
        public int añosAntiguedad { get; set; }
        public int nroPlantas { get; set; }
        public string pais { get; set; }
        public string provincia { get; set; }
        public string ciudad { get; set; }
        public DateTime fechaInicioPublicacion { get; set; }
        public string tipoPropiedad { get; set; }
        public string tipoMoneda { get; set; }
        public string tipoOperacion { get; set; }
        public List<byte[]> imagen = new List<byte[]>();
        public List<string> rutaImagenBD = new List<string>();

        

    }
}