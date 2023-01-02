using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DTO;

using WebApp.Enumerables;


namespace WebApp.Servicios
{
    public class ValidacionErroresServicios
    {
        public static DTOError ValidarParámetrosNuevaPublicación(DTOValidarParametrosNuevaPublicacion datos)
        {
            DTOError dtoError = new DTOError();
            dtoError.codigoError = (int)Enums.CodigosError.codNoError;
            if (String.IsNullOrEmpty(datos.direccion))
            {
                dtoError.codigoError = (int)Enums.CodigosError.codErrorNuevaPublicacion;
                dtoError.descripcionError.Add(NotificacionesServicios.faltaDireccion);
            }
            if (datos.precio == 0)
            {
                dtoError.codigoError = (int)Enums.CodigosError.codErrorNuevaPublicacion;
                dtoError.descripcionError.Add(NotificacionesServicios.precioMayorCero);
            }
            if (datos.superficieCubierta > datos.superficieTerreno)
            {
                dtoError.codigoError = (int)Enums.CodigosError.codErrorNuevaPublicacion;
                dtoError.descripcionError.Add(NotificacionesServicios.superficieTerrenoEsMenorSuperficieCubierta);
            }
            if (datos.tipoPropiedad == "-1")
            {
                dtoError.codigoError = (int)Enums.CodigosError.codErrorNuevaPublicacion;
                dtoError.descripcionError.Add(NotificacionesServicios.errorTipoPopiedad);
            }
            if (datos.tipoConstruccion == "-1")
            {
                dtoError.codigoError = (int)Enums.CodigosError.codErrorNuevaPublicacion;
                dtoError.descripcionError.Add(NotificacionesServicios.errorTipoConstruccion);
            }
            if (datos.imagenes.Count==0) {
                dtoError.codigoError = (int)Enums.CodigosError.codErrorNuevaPublicacion;
                dtoError.descripcionError.Add(NotificacionesServicios.errorCantidadImagenes);
            }
            foreach (var imagen in datos.imagenes)
            {
                bool validacionOk = ImagenServicios.ValidarImagen(imagen);
                if (validacionOk == false)
                {
                    dtoError.codigoError = (int)Enums.CodigosError.codErrorNuevaPublicacion;
                    dtoError.descripcionError.Add(NotificacionesServicios.errorExtensionImagen + imagen.FileName);
                    break;
                }
            }
            return dtoError;
        }
        public static DTOError ValidarEnvioParametrosFiltroBusqueda(DTOValidarParametrosFiltrosBusqueda datos)
        {
            DTOError dtoError = new DTOError();
            dtoError.codigoError = (int)Enums.CodigosError.codNoError;
            if (datos.precioHasta < datos.precioDesde)
            {
                dtoError.codigoError = (int)Enums.CodigosError.codErrorEnvioParametrosRealizarBusqueda;
                dtoError.descripcionError.Add(NotificacionesServicios.precioHastaEsMenorPrecioDesde);
            }
            if (datos.SuperficieTerrenoMax < 0 || datos.SuperficieTerrenoMin < 0)
            {
                dtoError.codigoError = (int)Enums.CodigosError.codErrorSuperficieTerreno;
                dtoError.descripcionError.Add(NotificacionesServicios.errorSuperficieTerreno);
            }
            if (datos.SuperficieTerrenoMin > datos.SuperficieTerrenoMax)
            {
                dtoError.codigoError = (int)Enums.CodigosError.codErrorSuperficieTerrenoMinimaMayorMaxima;
                dtoError.descripcionError.Add(NotificacionesServicios.errorSuperficieTerrenoMinimaMayorMaxima);
            }

            return dtoError;
        }
        public static void PrevenirErroresNull(ref String precioDesde, ref String precioHasta, ref string SupTMax, ref string SupTMin, ref string clientAddress, ref int radio)
        {
            //verifico null de superficie
            if (String.IsNullOrEmpty(SupTMax) && String.IsNullOrEmpty(SupTMin)) 
            {
                SupTMax = "0";
                SupTMin = "0";
            }
            else
            {
                if (String.IsNullOrEmpty(SupTMax))
                {
                    SupTMax = "0";
                }
                else
                {
                    if (String.IsNullOrEmpty(SupTMin))
                    {
                        SupTMin = "0";
                    }
                }
            }

            //verifico null de precios
            if (String.IsNullOrEmpty(precioDesde) && String.IsNullOrEmpty(precioHasta))
            {
                precioDesde = "0";
                precioHasta = "0";
            }
            else
            {
                if (String.IsNullOrEmpty(precioDesde))
                {
                    precioDesde = "0";
                }
                else
                {
                    if (String.IsNullOrEmpty(precioHasta)) { precioHasta = "0"; }
                }
            }

            //valido null de la dirección
            if (String.IsNullOrEmpty(clientAddress))
            {
                radio = -1;
            }
        }
    }

}