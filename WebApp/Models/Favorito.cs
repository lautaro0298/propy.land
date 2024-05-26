using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    [Table("Favorito")]
    public class Favorito
    {
        [Key]
        [Required]
        [StringLength(100)]
        public string favoritoId { get; set; }

        [Required]
        public bool activo { get; set; }

        [Required]
        public string usuarioId { get; set; }

        [ForeignKey("usuarioId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public Guid publicacionId { get; set; }

        [ForeignKey("publicacionId")]
        public virtual Publicacion? Publicacion { get; set; }

        [Index("IX_UsuarioPublicacion", 1, 2), Required]
        public virtual ApplicationUser? ApplicationUserWithPublicacion
        {
            get
            {
                return ApplicationUser;
            }
            set
            {
                ApplicationUser = value;
                Publicacion = null; // Reset Publicacion when ApplicationUser is set
            }
        }
    }
}
