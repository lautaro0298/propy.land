using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOPublicaciones
    {
        public int cantidadCreditosActivos { get; set; }
        public List<DTOPublicacion> publicaciones { get; set; }
        public DTOPublicaciones() {
            publicaciones = new List<DTOPublicacion>();
        }
    }
}
