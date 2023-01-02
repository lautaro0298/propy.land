using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOFavorito
    {
        public string publicacionId { get; set; }
        public string ubicación { get; set; }
        public List<string> tipoPropiedad { get; set; }
        public string tipoPublicacion { get; set; }
        public string precioPropiedad { get; set; }
        public string tipoMoneda { get; set; }
    }
}
