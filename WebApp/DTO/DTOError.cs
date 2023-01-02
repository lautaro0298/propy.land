using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOError
    {
        public int codigoError { get; set; }
        public List<string> descripcionError { get; set; }

        public DTOError() {
            descripcionError = new List<string>();
        }
    }
}