using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    public class TransferenciaPagoMP
    {
        public string PagoMPId { get; set; }
        public string idPago { get; set; }
        public string currency_id { get; set; }
        public DateTime date_approved { get; set; }
        public DateTime date_created { get; set; }
        public string operation_type { get; set; }
        public string payment_type_id { get; set; }
        public string status { get; set; }
        public float transaction_amount { get; set; }
        public virtual TransferenciaUsuario payer { get; set; }
        public string UsuarioId { get; set; }
    }
}
