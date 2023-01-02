using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOFavorito
    {
        public Guid publicacionId { get; set; }
        public string direccionPropiedad { get; set; }
        public string precio { get; set; }
        public string rutaImagen { get; set; }
        public string usuarioId { get; set; }
    }
}