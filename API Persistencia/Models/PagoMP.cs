using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Persistencia.Models
{
    public class PagoMP
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
        public virtual Usuario payer { get; set; }
        public string UsuarioId { get; set; }
    }
}
