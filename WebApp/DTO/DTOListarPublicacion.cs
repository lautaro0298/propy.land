using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOListarPublicacion
    {
        [Display(Name ="ID Publicación")]
        public Guid publicacionId { get; set; }
        [Display(Name ="Fecha Inicio Publicación")]
        public DateTime fechaInicioPublicacion { get; set; }
        [Display(Name ="Fecha Fin Publicación")]
        public DateTime fechaFinPublicacion { get; set; }
        [Display(Name ="Estado Actual")]
        public string estado { get; set; }
        [Display(Name ="Tipo Operación")]
        public string tipoOperacion { get; set; }
        [Display(Name ="Propiedad")]
        public string propiedad { get; set; }
        [Display(Name ="Dirección")]
        public string direccion { get; set; }
        [Display(Name ="Antiguedad en años")]
        public int antiguedad { get; set; }

    }
}