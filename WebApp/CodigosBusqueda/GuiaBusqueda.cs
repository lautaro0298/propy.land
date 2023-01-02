using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.CodigosBusqueda
{
    public static class GuiaBusqueda
    {
        /// <summary>
        /// Funcionamiento de Motor de Búsqueda:
        /// 1) Se busca por pais, provincia y ciudad.
        /// 2) De los resultados del paso 1, se filtra por Tipo de Propiedad, Tipo de Operacion y Tipo de Construcción.
        /// 3) De los resultados del paso 2, se filtra por cantidad de baños, cantidad de dormitorios, cantidad de cocheras.
        /// 4) De los resultados del paso 3, se filtra por años de antiguedad y cantidad de plantas.
        /// 5) De los resultados del paso 4, se filtra por extras.
        /// </summary>
        #region Constantes de paso 2
        public static int eligioTipoPropiedad = 8;
        public static int eligioTipoPropiedadTipoOperacion = 9;
        public static int eligioDefault = 10;//Este caso se da cuando el sistema solo toma los parámetros de ubicacion para realizar busquedas dado que el usuario no ingreso otro parámetro.
        public static int eligioTipoOperacion = 11;
        public static int eligioTipoConstruccion = 12;
        public static int eligioTipoPropiedadTipoConstruccion = 13;
        public static int eligioTipoConstruccionTipoOperacion = 14;
        public static int eligioTipoConstruccionTipoPropiedadTipoOperacion = 15;
        #endregion

        #region Constantes de paso 1
        public static int noEligioUbicacion = 0;
        public static int eligioPais = 1;
        public static int eligioPaisProvincia = 2;
        public static int eligioPaisProvinciaCiudad = 3;
        public static int eligioProvincia = 4;
        public static int eligioProvinciaCiudad = 5;
        public static int eligioCiudad = 6;
        public static int eligioPaisCiudad = 7;
        public static int eligioRadioEspecifico = 8;
        #endregion

        #region Constantes de paso 3
        public static int eligioCantidadBaños = 16;
        public static int eligioCantidadBañosCantidadDormitorios = 17;
        public static int eligioCantidadBañosCantidadDormitoriosCantidadCocheras = 18;
        public static int eligioCantidadBañosCantidadCocheras = 19;
        public static int noEligioBDC = 20;//No eligio ni baño, ni dormitorio, ni cochera
        public static int eligioCantidadDormitoriosCantidadCocheras = 21;
        public static int eligioCantidadDormitorios = 22;
        public static int eligioCantidadCocheras = 23;
        #endregion

        #region Constantes de paso 4
        public static int eligioAntiguedad = 24;
        public static int eligioCantidadPlantas = 25;
        public static int eligioAntiguedadCantidadPlantas = 26;
        public static int noEligioAntiguedadCantidadPlantas = 27;
        #endregion
    }
}