using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace API_Persistencia.Models
{
    public class PublicacionCaracteristica
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PublicacionCaracteristicaId { get; set; }

        [Required]
        public string CaracteristicaId { get; set; }
        public Caracteristica Caracteristica { get; set; }

        [Required]
        public string PublicacionId { get; set; }
        public Publicacion Publicacion { get; set; }

        [Required]
        public bool Required { get; set; }

        [Required]
        public string Value { get; set; }

        public ICollection<PublicacionCaracteristicaValor> Valores { get; set; }
    }

    public class PublicacionCaracteristicaValor
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PublicacionCaracteristicaValorId { get; set; }

        [Required]
        public string PublicacionCaracteristicaId { get; set; }
        public PublicacionCaracteristica PublicacionCaracteristica { get; set; }

        [Required]
        public string Valor { get; set; }
    }
}
