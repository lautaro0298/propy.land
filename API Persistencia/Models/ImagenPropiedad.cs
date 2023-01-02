using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class ImagenPropiedad
    {
        public string ImagenPropiedadId { get; set; }
        public string rutaImagenPropiedad { get; set; }
        public bool activo { get; set; }
        public string propiedadId { get; set; }
    }
}
