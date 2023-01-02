using System;
using System.Collections.Generic;
using System.Text;

namespace LibreriaClases
{
    public static class CodigosEstados
    {
        public enum Estados {
            activa,
            inactivaPorFaltaDeCreditos,
            inactivaPorUsuario
        }
    }
}
