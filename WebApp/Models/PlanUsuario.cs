using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("PlanUsuario")]
    public class PlanUsuario
    {
        [Key]
        public Guid planUsuarioID { get; set; }
        public DateTime fechaContratacion { get; set; }
        public bool activo { get; set; }
        public float TotalCreditosActivos { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Usuario { get; set; }
        public Guid planID { get; set; }
        [ForeignKey("planID")]
        public virtual Plan Plan { get; set; }


    }
}