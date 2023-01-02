using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("EstadoPublicacion")]
    public class EstadoPublicacion
    {
        [Key]
        public Guid estadoPublicacionId { get; set; }
        [Required]
        [StringLength(250)]
        [Display(Name ="Estado")]
        public string nombreEstadoPublicacion { get; set; }

        [Required]
        public bool activo { get; set; }
    }
}