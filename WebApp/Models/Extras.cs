using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Extras
    {
        [Key]
        public Guid extraId { get; set; }
        [Display(Name ="Tipo Extra")]
        public string nombreExtra { get; set; }
        public bool activo { get; set; }

        //Indico que una Extras puede tener una o muchas PropiedadExtras
        public virtual ICollection<PropiedadExtras> PropiedadExtras { get; set; }
    }
}