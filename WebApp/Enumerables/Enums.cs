using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Enumerables
{
    public static class Enums
    {
        public enum CodigosError {
            codNoError,
            codErrorNuevaPublicacion,
            codErrorEditarPublicacion,
            codErrorBajaPublicacion,
            codErrorEnvioParametrosRealizarBusqueda,
            codErrorSuperficieTerreno,
            codErrorSuperficieTerrenoMinimaMayorMaxima,
            codErrorContratarPlan,
            codErrorAgregarFavorito,
            codErrorQuitarFavorito
        }
    }
}