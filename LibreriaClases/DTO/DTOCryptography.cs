using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTOCryptography
    {
        public string OriginalText { get; set; }
        public byte[] OriginalTextEncrypted { get; set; }
        public byte[] Key { get; set; }
        public byte[] Vector { get; set; }

    }
}
