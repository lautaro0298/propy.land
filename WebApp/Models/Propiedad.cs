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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid propiedadId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Dirección Formateada")]
        public string direccionFormateada { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "País")]
        public string pais { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Área Administrativa Nivel 1")]
        public string areaAdministrativaNivel1 { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Área Administrativa Nivel 2")]
        public string areaAdministrativaNivel2 { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Nro Calle")]
        public int nroCalle { get; set; }

        [Display(Name = "Latitud")]
        public double latitud { get; set; }

        [Display(Name = "Longitud")]
        public double longitud { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Identificador Ubicación Google")]
        public string identificadorUbicacionGoogle { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Antigüedad")]
        public int antiguedad { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "N° Plantas")]
        public int nroPlantas { get; set; }

        [Required]
        [Display(Name = "Superficie Terreno")]
        [Column("superficie_terreno")]
        public float superficieTerreno { get; set; }

        [Required]
        [Display(Name = "Superficie Cubierta")]
        [Column("superficie_cubierta")]
        public float superficieCubierta { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Calle")]
        public string calle { get; set; }

        [Display(Name = "Fecha Registro")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaRegistro { get; set; }

        //Clave Externa Tipo Propiedad
        public Guid tipoPropiedadId { get; set; }
        [ForeignKey("tipoPropiedadId")]
        public virtual TipoPropiedad TipoPropiedad { get; set; }

        //Clave Externa Tipo Construcción (nullable)
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
