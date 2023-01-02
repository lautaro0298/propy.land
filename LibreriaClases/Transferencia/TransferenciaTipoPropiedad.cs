using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaTipoPropiedad
    {
        public string tipoPropiedadId { get; set; }
        public string nombreTipoPropiedad { get; set; }
        public bool activo { get; set; }
        public virtual List<TransferenciaPropiedadCaracteristica> tipoPropiedadCaracteristicas { get; set; }
        public virtual List<TransferenciaTipoPropiedadTipoAmbiente> TipoPropiedadTipoAmbiente { get; set; }
    }
}
