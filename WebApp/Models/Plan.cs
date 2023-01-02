using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("Plan")]
    public class Plan
    {
        [Key]
        public Guid planId { get; set; }
        public string itemId { get; set; }
        public string nombrePlan { get; set; }
        public string descripcionPlan { get; set; }
        public string moneda { get; set; }
        public int creditos { get; set; }
        public string vencimientoCreditos { get; set; }
        public bool subirVideos { get; set; }
        public decimal precioPlan { get; set; }
        public bool activo { get; set; }
        public int cantidadMaxImagenesPermitidasPorPub { get; set; }
        public bool accesoEstadisticasPremium { get; set; }
        public virtual ICollection<ApplicationUser> Usuario { get; set; }
    }
}