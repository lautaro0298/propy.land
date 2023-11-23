using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOPublicacion
    {
        public string publicacionId { get; set; }
        public string fechaInicioPublicacion { get; set; }
        public string fechaFinPublicacion { get; set; }
        public string ubicacionPropiedad { get; set; }
        public string pais { get; set; }
        public string provincia { get; set; }
        public string departamento { get; set; }
        public string precioPropiedad { get; set; }
        public DTOPropiedad propiedad { get; set; }
        
        public int estado { get; set; }
        public string tipoMoneda { get; set; }

    }
}
