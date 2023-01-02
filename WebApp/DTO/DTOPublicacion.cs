using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOPublicacion
    {
        [Display(Name ="Publicación ID")]
        public Guid publicacionId { get; set; }

        public DateTime fechaInicioPublicacion { get; set; }

        [Display(Name ="Precio Propiedad")]
        public decimal precioPropiedad { get; set; }
    }
}