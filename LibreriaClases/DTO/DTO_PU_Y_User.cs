using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases.DTO
{
    public class DTO_PU_Y_User
    {
        public List<DTOPlanUsuario> dTOs { get; set; }
        public TransferenciaUsuario User { get; set; }

        public DTO_PU_Y_User()
        {
            dTOs = new List<DTOPlanUsuario>();
        }
    }
}
