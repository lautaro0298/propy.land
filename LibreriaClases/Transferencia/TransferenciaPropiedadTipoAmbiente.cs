using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaPropiedadTipoAmbiente
    {
        public string propiedadTipoAmbienteId { get; set; }
        public bool activo { get; set; }
        public int cantidad { get; set; }
        public string propiedadId { get; set; }
        public string tipoAmbienteId { get; set; }
        public virtual TipoAmbiente TipoAmbiente { get; set; }
    }
}
