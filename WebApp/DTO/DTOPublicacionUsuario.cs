using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOPublicacionUsuario
    {
        public string ubicacion { get; set; }
        public string precio { get; set; }
        public string rutaImagenBD { get; set; }
        public DateTime fechaInicioPublicacion { get; set; }
        public string estado { get; set; }
        public Guid publicacionId { get; set; }
    }
}