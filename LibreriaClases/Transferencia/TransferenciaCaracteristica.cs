using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaCaracteristica
    {
        public string caracteristicaId { get; set; }
        public string nombreCaracteristica { get; set; }
        public bool activo { get; set; }
        //public string TipoPropiedadCaracteristicaID { get; set; }
        public virtual List<TransferenciaPropiedadCaracteristica> propiedadCaracteristica { get; set; }
    }
}
