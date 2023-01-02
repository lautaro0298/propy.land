using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
   [Table("CreditoPlan")]
    public class CreditoPlan
    {
        [Key]
        public Guid CreditoPlanID{ get; set; }
        public string moneda { get; set; }
        public bool activo { get; set; }
        public decimal precioPlanCredito { get; set; }
        public int cantidadTotalCreditoPorPaquete { get; set; }
    }
}