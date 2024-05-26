using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    /// <summary>
    /// Represents a data transfer object for TP and TA.
    /// </summary>
    public class DTO_TP_Y_TA
    {
        /// <summary>
        /// Gets or sets the list of DTO Tipo Ambiente objects.
        /// </summary>
        public List<DTOTipoAmbiente> DTOTipoAmbientes { get; set; }

        /// <summary>
        /// Gets or sets the list of DTO Tipo Propiedad objects.
        /// </summary>
        public List<DTOTipoPropiedad> DTOTipoPropiedades { get; set; }

        /// <summary>
        /// Prevents direct instantiation of the class.
        /// </summary>
        private DTO_TP_Y_TA()
        {
        }

        /// <summary>
        /// Initializes a new instance of the DTO_TP_Y_TA class.
        /// </summary>
        public DTO_TP_Y_TA(List<DTOTipoAmbiente> dtosTipoAmbientes, List<DTOTipoPropiedad> dtosTipoPropiedades)
        {
            if (dtosTipoAmbientes == null)
            {
                throw new ArgumentNullException(nameof(dtosTipoAmbientes));
            }

            if (dtosTipoPropiedades == null)
            {
                throw new ArgumentNullException(nameof(dtosTipoPropiedades));
            }

            DTOTipoAmbientes = dtosTipoAmbientes;
            DTOTipoPropiedades = dtosTipoPropiedades;
        }
    }
}
