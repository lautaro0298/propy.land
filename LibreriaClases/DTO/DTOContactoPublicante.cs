using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOContactoPublicante
    {
        public string nombreCompletoPublicante { get; set; }
        public string telefonoContactoPrincipal { get; set; }
        public string telefonoContactoAlternativo { get; set; }
        public string email { get; set; }
        public string tipoPublicante { get; set; }
    }
}
