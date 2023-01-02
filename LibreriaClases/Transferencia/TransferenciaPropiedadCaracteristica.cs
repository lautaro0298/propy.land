using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaPropiedadCaracteristica
    {
        public string tipoPropiedadCaracteristicaID { get; set; }
        public string caracteristicaId { get; set; }
        public string TipopropiedadId { get; set; }
        public bool activo { get; set; }

        public virtual TransferenciaCaracteristica caracteristicas { get; set; }
        public virtual TransferenciaTipoPropiedad tipoPropiedad { get; set; }
    }
}
