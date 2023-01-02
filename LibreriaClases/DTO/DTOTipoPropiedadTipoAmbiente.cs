using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOTipoPropiedadTipoAmbiente
    {
        public List<DTOTipoAmbiente> dTOTipoAmbientes { get; set; }
        public DTOTipoPropiedad dTOTipoPropiedades { get; set; }


        public DTOTipoPropiedadTipoAmbiente()
        {
            dTOTipoAmbientes = new List<DTOTipoAmbiente>();
        }
    }
}
