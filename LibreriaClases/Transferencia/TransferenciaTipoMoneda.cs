using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.Transferencia
{
    /// <summary>
    /// Represents a type of currency used in a transfer.
    /// </summary>
    public class CurrencyTypeTransfer
    {
        /// <summary>
        /// The unique identifier of the currency type.
        /// </summary>
        public string CurrencyTypeId { get; set; }

        /// <summary>
        /// The denomination of the currency.
        /// </summary>
        public string CurrencyDenomination { get; set; }

        /// <summary>
        /// Indicates whether the currency type is active or not.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
