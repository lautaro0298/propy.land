using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTO_C_Y_A
    {
        public List<DTOTipoAmbiente> dTOTipoAmbientes { get; set; }
        public List<DTOCaracteristica> dTOCaracteristicas { get; set; }

        public DTO_C_Y_A()
        {
            dTOTipoAmbientes = new List<DTOTipoAmbiente>();
            dTOCaracteristicas = new List<DTOCaracteristica>();
        }
    }
}
