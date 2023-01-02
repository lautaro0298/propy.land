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
        public static ErrorPropy Crear_y_Asignar_Caracteristica_A_Propiedad(string TipoPropiedad, string caracteristica)
        {

            ErrorPropy errorPropy = new ErrorPropy();
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            
            TransferenciaPropiedadCaracteristica transferenciaTipoPropiedadCaracteristica = new TransferenciaPropiedadCaracteristica();
            transferenciaTipoPropiedadCaracteristica.TipopropiedadId = TipoPropiedad;
            transferenciaTipoPropiedadCaracteristica.caracteristicaId = caracteristica;
                transferenciaTipoPropiedadCaracteristica.tipoPropiedadCaracteristicaID = System.Guid.NewGuid().ToString();
            var tareaCrearTipoPropiedadCaracteristica = httpClient.PostAsJsonAsync<TransferenciaPropiedadCaracteristica >("api/TipoPropiedadCaracteristica/CrearTipoPropiedadCaracteristica", transferenciaTipoPropiedadCaracteristica );
                tareaCrearTipoPropiedadCaracteristica.Wait();
          
            if (!tareaCrearTipoPropiedadCaracteristica.Result.IsSuccessStatusCode)
                {
                    errorPropy.codigoError = (int)tareaCrearTipoPropiedadCaracteristica.Result.StatusCode;
                    errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaCrearTipoPropiedadCaracteristica.Result.StatusCode;
                 
                }
            
            return errorPropy;
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
                    DTOCaracteristica caracteristica= new DTOCaracteristica();
                    caracteristica.nombreCaracteristica= tipoProp.caracteristicas.nombreCaracteristica;
                    caracteristica.caracteristicaId = tipoProp.caracteristicas.nombreCaracteristica;
                    count++;
                    dTO_TP_Y_C.dTOCaracteristicas=( caracteristica);
                    dTO_TP_Y_C.dTOTipoPropiedades.tipoPropiedadId = tipoProp.TipopropiedadId;

                    ListaDTOTIpoPropiedad.Add(dTO_TP_Y_C);
                }
            }
            return (errorPropy, ListaDTOTIpoPropiedad);

        }
    }
}
