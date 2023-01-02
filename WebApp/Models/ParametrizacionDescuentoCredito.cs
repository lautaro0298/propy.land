using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    [Table("ParametrizacionDescuentoCredito")]
    public class ParametrizacionDescuentoCredito
    {
        /// <summary>
        /// CoeficienteDescuentoCredito es el coeficiente de descuento cuando el usuario solicita los datos del contacto
        /// DescuentoCompraSinPaquete es el coeficiente de descuento cuando el usuario compra los créditos individuales sin acceder a ningún plan de créditos
        /// </summary>
        [Key]
        public Guid IDDescuentoCredito { get; set; }
        public float CoeficienteDescuentoCredito { get; set; }
        public float DescuentoCompraSinPaquete { get; set; }

    }
}