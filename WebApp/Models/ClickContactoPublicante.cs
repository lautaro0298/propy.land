using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("ClickContactoPublicante")]
    public class ClickContactoPublicante
    {
        [Key]
        public Guid clickContactoPublicanteId { get; set; }

        public DateTime fechaHoraClickContactoPublicante { get; set; }

        //Clave Externa Usuario
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //Clave Externa Publicacion
        [ForeignKey("Publicacion")]
        public Guid publicacionId { get; set; }
        public virtual Publicacion Publicacion { get; set; }
    }
}