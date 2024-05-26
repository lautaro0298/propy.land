using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Models
{
    [Index(nameof(PublicacionId), nameof(UsuarioId), IsUnique = true)]
    public class SolicitudContactoVisitante
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SolicitudContactoVisitanteId { get; set; }

        [Required]
        public int CantidadVecesRealizoSolicitud { get; set; }

        [Required]
        public DateTime FechaSolicitud { get; set; }

        [Required]
        public string PublicacionId { get; set; }
        public virtual Publicacion Publicacion { get; set; }

        [Required]
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }

    public class Publicacion
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PublicacionId { get; set; }

        // Add other properties and configurations for the Publicacion entity.
    }

    public class Usuario
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UsuarioId { get; set; }

        // Add other properties and configurations for the Usuario entity.
    }
}
