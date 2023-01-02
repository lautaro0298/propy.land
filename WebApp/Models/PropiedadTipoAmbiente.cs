using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    
    public class PropiedadTipoAmbiente
    {
        [Key]
        public Guid propiedadTipoAmbienteId { get; set; }

        [Display(Name ="Cantidad Ambientes")]
        [Required]
        public int cantidadAmbientes { get; set; }

        //Clave Externa
        [ForeignKey("TipoAmbiente")]
        public Guid tipoAmbienteId { get; set; }
        public virtual TipoAmbiente TipoAmbiente { get; set; }

        //Clave Externa
        [ForeignKey("Propiedad")]
        public Guid propiedadId { get; set; }
        public virtual Propiedad Propiedad { get; set; }
    }
}