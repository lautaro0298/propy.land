using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaClases.DTOJSon
{
    public class DTOResult
    {
        public List<DTOAddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public DTOGeometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }
    }
}