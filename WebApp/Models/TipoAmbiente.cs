using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("TipoAmbiente")]
    public class TipoAmbiente
    {
        [Key]
        [Display(Name ="Id")]
        public Guid tipoAmbienteId { get; set; }
        [Display(Name ="Tipo Ambiente")]
        public string nombreTipoAmbiente { get; set; }
        public bool activo { get; set; }

        //Indico que un TipoAmbiente puede tener una o muchas PropiedadTipoAmbiente
        public virtual ICollection<PropiedadTipoAmbiente> PropiedadTipoAmbiente { get; set; }
    }
}