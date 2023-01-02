using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOUbicacionGoogle
    {
        public string pais { get; set; }
        public string areaAdministrativaNivel1 { get; set; }
        public string areaAdministrativaNivel2 { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public string ubicacionFormateada { get; set; }

    }
}
