using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class NuevaPublicacionViewModel
    {
        [Required]
        [Display(Name = "Precio Propiedad")]
        public decimal precioPropiedad { get; set; }
        [Display(Name = "País")]
        [Required]
        public Guid pais { get; set; }

        [Display(Name ="Tipo de Moneda")]
        public string tipoMoneda { get; set; }

        [Display(Name = "Años de Antiguedad")]
        public int antiguedad { get; set; }

        [Display(Name ="Imagen")]
        public byte[] imagen { get; set; }

        [Display(Name ="Superficie del Terreno")]
        public float superficieTerreno { get; set; }

        [Display(Name ="Superficie Cubierta")]
        public float superficieCubierta { get; set; }

        
        public List<Guid> extras { get; set; }

        [Display(Name ="Número de Plantas")]
        public int nroPlantas { get; set; }

        [Display(Name ="Provincia")]
        [Required]
        public Guid provincia { get; set; }
        [Display(Name ="Localidad")]
        [Required]
        public Guid localidad { get; set; }
        
        [Required]
        [Display(Name ="Dirección")]
        public string direccion { get; set; }
        [Display(Name ="Tipo Propiedad")]
        public Guid tipoPropiedad { get; set; }

        [Display(Name ="Observaciones")]
        public string observaciones { get; set; }

        [Display(Name ="Tipo Ambiente")]
        public List<Guid> tipoAmbiente { get; set; }

        public List<int> cantidadAmbientes { get; set; }

        [Display(Name ="Tipo Propietario")]
        public Guid tipoUsuario { get; set; }
        [Display(Name ="Tipo Operación")]
        public Guid tipoPublicacion { get; set; }
        

        [Required]
        [Display(Name ="Tipo de Construcción:")]
        public Guid tipoConstruccion { get; set; }
    }
}