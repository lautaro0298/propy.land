using System;

namespace WebApp.CodigosBusqueda
{
    public static class GuiaBusqueda
    {
        public enum Ubicacion
        {
            NoEligioUbicacion,
            EligioPais,
            EligioPaisProvincia,
            EligioPaisProvinciaCiudad,
            EligioProvincia,
            EligioProvinciaCiudad,
            EligioCiudad,
            EligioPaisCiudad,
            EligioRadioEspecifico
        }

        public enum Paso2
        {
            EligioTipoPropiedad,
            EligioTipoPropiedadTipoOperacion,
            EligioDefault,
            EligioTipoOperacion,
            EligioTipoConstruccion,
            EligioTipoPropiedadTipoConstruccion,
            EligioTipoConstruccionTipoOperacion,
            EligioTipoConstruccionTipoPropiedadTipoOperacion
        }

        public enum Paso3
        {
            EligioCantidadBaños,
            EligioCantidadBañosCantidadDormitorios,
            EligioCantidadBañosCantidadDormitoriosCantidadCocheras,
            EligioCantidadBañosCantidadCocheras,
            NoEligioBDC,
            EligioCantidadDormitoriosCantidadCocheras,
            EligioCantidadDormitorios,
            EligioCantidadCocheras
        }

        public enum Paso4
        {
            EligioAntiguedad,
            EligioCantidadPlantas,
            EligioAntiguedadCantidadPlantas,
            NoEligioAntiguedadCantidadPlantas
        }

        /// <summary>
        /// Funcionamiento de Motor de Búsqueda:
        /// 1) Se busca por pais, provincia y ciudad.
        /// 2) De los resultados del paso 1, se filtra por Tipo de Propiedad, Tipo de Operacion y Tipo de Construcción.
        /// 3) De los resultados del paso 2, se filtra por cantidad de baños, cantidad de dormitorios, cantidad de cocheras.
        /// 4) De los resultados del paso 3, se filtra por años de antiguedad y cantidad de plantas.
        /// 
