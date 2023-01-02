using LibreriaClases;
using LibreriaClases.Transferencia;
using LibreriaExperto.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LibreriaExperto.Seguridad
{
    public static class ControlAcceso
    {
        public static bool Autorizacion(object obj) {
            if (obj != null) {
                return true;
            }
            return false;
        }
        
    }
}
