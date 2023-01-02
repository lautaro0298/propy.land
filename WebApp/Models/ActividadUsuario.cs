using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("ActividadUsuario")]
    public class ActividadUsuario
    {
        [Key]
        public Guid IDActividadUsuario { get; set; }
        public string descripcionActividad { get; set; }
        public DateTime fechaActividad { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}