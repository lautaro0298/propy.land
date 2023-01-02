using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaClases.DTOJSon
{
    public class DTORootObjectId
    {
        public List<DTOIdentificador> candidates { get; set; }
        public string status { get; set; }
    }
}