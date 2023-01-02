using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaClases.DTOJSon
{
    public class DTOAddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }
}