using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Servicios
{
    public static class UsuarioServicios
    {
        
        public static bool VerificarCUIL(string cuil) {
            int[] factores = new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int acumulado = 0;
            
             
            int digitoVerificador = 0;
            if (String.IsNullOrEmpty(cuil) || cuil.Length != 11)
            {
                return false;
            }
            else {
                for (int i=0; i<factores.Length;i++) {
                    acumulado += int.Parse(cuil[i].ToString()) * factores[i];
                }
                digitoVerificador = 11 - (acumulado % 11);
            }
            if (digitoVerificador == 11) {
                digitoVerificador = 0;
            }
            if (int.Parse(cuil[10].ToString())!=digitoVerificador) {
                return false;
            }

            return true;
        }
    }
}