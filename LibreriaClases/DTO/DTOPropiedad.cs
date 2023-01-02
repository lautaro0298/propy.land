using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOPropiedad
    {
        public string publicacionId { get; set; }
        public string ubicación { get; set; }
        public string tipoMoneda { get; set; }
        public string precioPropiedad { get; set; }
        public string fechaInicioPublicacion { get; set; }
        public List<string> tipoPropiedad { get; set; }
        public string tipoPublicante { get; set; }
        public string tipoPublicacion { get; set; }
        public string imagen { get; set; }
    }
}
