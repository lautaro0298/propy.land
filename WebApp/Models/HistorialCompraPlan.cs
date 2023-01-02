using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("HistorialCompraPlan")]
    public class HistorialCompraPlan
    {
        [Key]
        public Guid HistorialCompraPlanID { get; set; }
        public DateTime FechaCompra { get; set; }

        public Guid PlanID { get; set; }
        [ForeignKey("PlanID")]
        public virtual Plan Plan { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser Usuario { get; set; }
    }
}