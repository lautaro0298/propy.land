using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("Propiedad")]
    public class Propiedad
    {
        [Key]
        [Display(Name = "Id")]
        public Guid propiedadId { get; set; }

        public string direccionFormateada { get; set; }

        [Required]
        public string pais { get; set; }

        [Required]
        public string areaAdministrativaNivel1 { get; set; }

        [Required]
        public string areaAdministrativaNivel2 { get; set; }

        [Required]
        public int nroCalle { get; set; }

        public double latitud { get; set; }

        public double longitud { get; set; }

        [Required]
        public string identificadorUbicacionGoogle { get; set; }

        [Required]
        [Display(Name = "Antiguedad")]
        public int antiguedad { get; set; }

        [Required]
        [Display(Name ="N° Plantas")]
        public int nroPlantas { get; set; }

        [Required]
        [Display(Name ="Superficie Terreno")]
        public float superficieTerreno { get; set; }

        [Required]
        [Display(Name ="Superficie Cubierta")]
        public float superficieCubierta { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        public string calle { get; set; }

        [Display(Name ="Fecha Registro")]
        public DateTime fechaRegistro { get; set; }

        //Clave Externa Tipo Propiedad
        public Guid tipoPropiedadId { get; set; }
        [ForeignKey("tipoPropiedadId")]
        public virtual TipoPropiedad TipoPropiedad { get; set; }

        //Clave Externa Tipo Construccion
        public Guid? tipoConstruccionId { get; set; }
        [ForeignKey("tipoConstruccionId")]
        public virtual TipoConstruccion TipoConstruccion { get; set; }

        

        //Clave Externa Usuario
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        //Indico que una Propiedad puede tener una o muchas PropiedadExtras
        public virtual ICollection<PropiedadExtras> PropiedadExtras { get; set; }

        //Indico que una Propiedad puede tener una o muchas PropiedadTipoAmbiente
        public virtual ICollection<PropiedadTipoAmbiente> PropiedadTipoAmbiente { get; set; }

    }
}