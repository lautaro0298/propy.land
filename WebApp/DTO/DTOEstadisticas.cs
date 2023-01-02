using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOEstadistica
    {
        public string nombreInteresado { get; set; }
        
        public DateTime fechaHoraClickInteresado { get; set; }
        
        
        public int cantidadVisitas { get; set; }

        public bool visitaAlPublicante { get; set; }

        public bool permiteMostrarDatos { get; set; }
    }
}