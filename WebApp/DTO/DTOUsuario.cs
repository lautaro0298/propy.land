using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOUsuario
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string tipoDocumento { get; set; }
        public long nroDocumento { get; set; }
        public long nroTelefono { get; set; }
        public bool permitirSerContactadoPublicante { get; set; }
        public bool permitirSerNotificado { get; set; }
    }
}