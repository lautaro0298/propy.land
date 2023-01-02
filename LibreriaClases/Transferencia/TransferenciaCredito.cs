using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaCredito
    {
        public string PaqueteID { get; set; }
        public int CantidadCreditos { get; set; }
        public decimal Precio { get; set; }
        public bool Activo { get; set; }
        public string NombrePack { get; set; }
        public string TipoMonedaID { get; set; }
        public virtual TransferenciaTipoMoneda TipoMoneda { get; set; }
    }
}
