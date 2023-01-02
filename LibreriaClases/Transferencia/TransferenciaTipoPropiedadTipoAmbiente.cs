using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaTipoPropiedadTipoAmbiente
    {
        public string tipoPropiedadTipoAmbienteId { get; set; }
        public bool activo { get; set; }
        public string tipoAmbienteId { get; set; }
        public string tipoPropiedadId { get; set; }
        public virtual TipoAmbiente TipoAmbiente { get; set; }

        public static implicit operator List<object>(TransferenciaTipoPropiedadTipoAmbiente v)
        {
            throw new NotImplementedException();
        }
    }
}
