using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace LibreriaExperto.Desarrollo
{
    public static class ABMTipoPropiedadcaracteristica
    {

        public static ErrorPropy Crear_y_Asignar_Caracteristicas_A_Propiedades(string[] tiposPropiedad, string[] caracteristicas)
        {
            ErrorPropy errorPropy = new ErrorPropy();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            try
            {
                foreach (var tipoProp in tiposPropiedad)
                {
                    foreach (var carac in caracteristicas)
                    {
                        TransferenciaPropiedadCaracteristica transferenciaTipoPropiedadCaracteristica = new TransferenciaPropiedadCaracteristica();
                        transferenciaTipoPropiedadCaracteristica.TipopropiedadId = tipoProp;
                        transferenciaTipoPropiedadCaracteristica.caracteristicaId = carac;
                        transferenciaTipoPropiedadCaracteristica.tipoPropiedadCaracteristicaID = System.Guid.NewGuid().ToString();

                        var tareaCrearTipoPropiedadCaracteristica = httpClient.PostAsJsonAsync<TransferenciaPropiedadCaracteristica>("api/TipoPropiedadCaracteristica/CrearTipoPropiedadCaracteristica", transferenciaTipoPropiedadCaracteristica);
                        tareaCrearTipoPropiedadCaracteristica.Wait();

                        if (!tareaCrearTipoPropiedadCaracteristica.Result.IsSuccessStatusCode)
                        {
                            errorPropy.codigoError = (int)tareaCrearTipoPropiedadCaracteristica.Result.StatusCode;
                            errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaCrearTipoPropiedadCaracteristica.Result.StatusCode;
                            return errorPropy; // Puedes detener el proceso si hay un error en una de las asignaciones.
                        }
                    }
                }

                return errorPropy;
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante el proceso.
                errorPropy.codigoError = -1; // Código de error personalizado, si es necesario.
                errorPropy.descripcionError = ex.Message;
                return errorPropy;
            }
        }

        public static (ErrorPropy, List<DTO_TP_Y_C>) EliminarTipoPropiedadCaracteristica(string id )
        {
            ErrorPropy errorPropy = new ErrorPropy();
            DTO_TP_Y_C dto=new DTO_TP_Y_C();
            dto.tipoPropiedadCaracteristicaID = id;
            List<DTO_TP_Y_C> ListaDTOTIpoPropiedad = new List<DTO_TP_Y_C>();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            
            var tareaObtenerTipoPropiedadcaracteristica = clienteHttp.PostAsJsonAsync("api/TipoPropiedadCaracteristica/eliminarCaracteristica", dto );
            tareaObtenerTipoPropiedadcaracteristica.Wait();

            if (!tareaObtenerTipoPropiedadcaracteristica.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaObtenerTipoPropiedadcaracteristica.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaObtenerTipoPropiedadcaracteristica.Result.StatusCode;
                ListaDTOTIpoPropiedad = null;
            }
            else
            {
                List<TransferenciaPropiedadCaracteristica> tipoPropiedacaracteris = tareaObtenerTipoPropiedadcaracteristica.Result.Content.ReadAsAsync<List<TransferenciaPropiedadCaracteristica>>().Result;
                int count = 0;

                foreach (var tipoProp in tipoPropiedacaracteris)
                {

                    DTO_TP_Y_C dTO_TP_Y_C = new DTO_TP_Y_C();
                    dTO_TP_Y_C.dTOTipoPropiedades = new DTOTipoPropiedad();
                    dTO_TP_Y_C.dTOTipoPropiedades.nombreTipoPropiedad = tipoProp.tipoPropiedad.nombreTipoPropiedad;
                    dTO_TP_Y_C.dTOTipoPropiedades.tipoPropiedadId = tipoProp.tipoPropiedad.tipoPropiedadId;
                    DTOCaracteristica caracteristica = new DTOCaracteristica();
                    caracteristica.nombreCaracteristica = tipoProp.caracteristicas.nombreCaracteristica;
                    caracteristica.caracteristicaId = tipoProp.caracteristicas.caracteristicaId;
                    count++;
                    dTO_TP_Y_C.dTOCaracteristicas = (caracteristica);
                    dTO_TP_Y_C.caracteristicaId = tipoProp.TipopropiedadId;

                    ListaDTOTIpoPropiedad.Add(dTO_TP_Y_C);
                }
            }
            return (errorPropy, ListaDTOTIpoPropiedad);

        }

        public static (ErrorPropy, List<DTO_TP_Y_C>) TraerTipoPropiedadcaracteristica()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTO_TP_Y_C> ListaDTOTIpoPropiedad = new List<DTO_TP_Y_C>();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();

            var tareaObtenerTipoPropiedadcaracteristica = clienteHttp.GetAsync("api/TipoPropiedadCaracteristica/obtenerTiposPropiedadesycaracteristicas");
            tareaObtenerTipoPropiedadcaracteristica.Wait();

            if (!tareaObtenerTipoPropiedadcaracteristica.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaObtenerTipoPropiedadcaracteristica.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaObtenerTipoPropiedadcaracteristica.Result.StatusCode;
                ListaDTOTIpoPropiedad = null;
            }
            else
            {
                List<TransferenciaPropiedadCaracteristica> tipoPropiedacaracteris = tareaObtenerTipoPropiedadcaracteristica.Result.Content.ReadAsAsync<List<TransferenciaPropiedadCaracteristica>>().Result;
                int count = 0;
                
                foreach (var tipoProp in tipoPropiedacaracteris)
                {
                   
                    DTO_TP_Y_C dTO_TP_Y_C = new DTO_TP_Y_C();
                    dTO_TP_Y_C.dTOTipoPropiedades = new DTOTipoPropiedad();
                    dTO_TP_Y_C.dTOTipoPropiedades.nombreTipoPropiedad = tipoProp.tipoPropiedad.nombreTipoPropiedad;
                    dTO_TP_Y_C.dTOTipoPropiedades.tipoPropiedadId = tipoProp.tipoPropiedad.tipoPropiedadId;
                    DTOCaracteristica caracteristica= new DTOCaracteristica();
                    caracteristica.nombreCaracteristica= tipoProp.caracteristicas.nombreCaracteristica;
                    caracteristica.caracteristicaId = tipoProp.caracteristicas.caracteristicaId;
                    count++;
                    dTO_TP_Y_C.dTOCaracteristicas=( caracteristica);
                    dTO_TP_Y_C.caracteristicaId = tipoProp.TipopropiedadId;
                    dTO_TP_Y_C.tipoPropiedadCaracteristicaID=tipoProp.tipoPropiedadCaracteristicaID;
                    ListaDTOTIpoPropiedad.Add(dTO_TP_Y_C);
                }
            }
            return (errorPropy, ListaDTOTIpoPropiedad);

        }
    }
}
