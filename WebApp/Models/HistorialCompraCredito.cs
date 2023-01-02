using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("HistorialCompraCredito")]
    public class HistorialCompraCredito
    {
        [Key]
        public Guid HistorialCompraCreditoID { get; set; }
        public DateTime FechaCompraCredito { get; set; }

        public Guid CreditoPlanID { get; set; }
        [ForeignKey("CreditoPlanID")]
        public virtual CreditoPlan CreditoPlan { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser Usuario { get; set; }
    }
}