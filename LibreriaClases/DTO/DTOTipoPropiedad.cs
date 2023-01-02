using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOTipoPropiedad
    {
        public string tipoPropiedadId { get; set; }
        public string nombreTipoPropiedad { get; set; }

        public bool activo { get; set; } 
        public ICollection<DTOTipoPropiedad> dTOTipoPropiedads { get; set; }
        public List<TransferenciaTipoPropiedadTipoAmbiente> dTOTipoAmbiente { get; set; }
    }
}
