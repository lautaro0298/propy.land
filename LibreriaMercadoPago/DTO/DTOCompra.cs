using LibreriaClases.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaMercadoPago.DTO
{
    public class DTOCompra
    {
        public List<MercadoPago.Resources.Preference> preferences {get;set;}
        public List<DTOPlan> dTOPlanes { get; set; }
        public List<DTOCredito> dTOCreditos { get; set; }
        public DTOCompra()
        {
            preferences = new List<MercadoPago.Resources.Preference>();
            dTOPlanes = new List<DTOPlan>();
            dTOCreditos = new List<DTOCredito>();
        }
    }
}
    
