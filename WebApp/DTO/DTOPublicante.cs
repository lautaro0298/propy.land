using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOPublicante
    {
        public string nombrePublicante { get; set; }
        public string apellidoPublicante { get; set; }
        public string email { get; set; }
        public long telefono { get; set; }
        public string tipoPublicante { get; set; }
    }
}