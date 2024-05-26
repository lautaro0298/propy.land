using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LibreriaExperto.Desarrollo
{
    public static class ABMPlan
    {
        public static async Task<ErrorPropy> CrearPlan(DTOPlan dTOPlan)
        {
            ErrorPropy errorPropy = new ErrorPropy();
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            TransferenciaPlan transferenciaPlan = new TransferenciaPlan();

            transferenciaPlan.planId = System.Guid.NewGuid().ToString();
            transferenciaPlan.nombrePlan = dTOPlan.nombrePlan;
            transferenciaPlan.precioPlan = dTOPlan.precioPlan;
            transferenciaPlan.TipoMonedaID = dTOPlan.tipoMonedaId;
            transferenciaPlan.cantidadCreditosIniciales = dTOPlan.cantidadCreditosIniciales;
            transferenciaPlan.cantidadMaxImagenesPermitidasPorPublicacion = dTOPlan.cantidadMaxImagenesPermitidasPorPublicacion;
            transferenciaPlan.permiteVideo = dTOPlan.permiteVideo;
            transferenciaPlan.accesoEstadisticasAvanzadas = dTOPlan.accesoEstadisticasAvanzadas;
            transferenciaPlan.activo = true;

            HttpResponseMessage response = await httpClient.PostAsJsonAsync<TransferenciaPlan>("api/Plan/crearPlan", transferenciaPlan);

            if (!response.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)response.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + response.StatusCode;
            }

            return errorPropy;
        }

        public static async Task<(ErrorPropy, List<DTOPlan>)> traerPlan()
        {
            ErrorPropy errorPropy = new ErrorPropy();
            List<DTOPlan> ListaPlanes = new List<DTOPlan>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            HttpResponseMessage response = await httpClient.GetAsync("api/Plan/obtenerPlanes");

            if (!response.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)response.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + response.StatusCode;
                return (errorPropy, null);
            }

            ListaPlanes = await response.Content.ReadAsAsync<List<DTOPlan>>();

            return (errorPropy, ListaPlanes);
        }

        public static async Task<ErrorPropy> EditarPlan(DTOPlan dTOPlan)
        {
            ErrorPropy errorPropy = new ErrorPropy();
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            TransferenciaPlan transferenciaPlan = new TransferenciaPlan();

            transferenciaPlan.planId = dTOPlan.planId;
            transferenciaPlan.nombrePlan = dTOPlan.nombrePlan;
            transferenciaPlan.precioPlan = dTOPlan.precioPlan;
            transferenciaPlan.TipoMonedaID = dTOPlan.tipoMonedaId;
            transferenciaPlan.cantidadCreditosIniciales = dTOPlan.cantidadCreditosIniciales;
            transferenciaPlan.cantidadMaxImagenesPermitidasPorPublicacion = dTOPlan.cantidadMaxImagenesPermitidasPorPublicacion;
            transferenciaPlan.permiteVideo = dTOPlan.permiteVideo;
            transferenciaPlan.accesoEstadisticasAvanzadas = dTOPlan.accesoEstadisticasAvanzadas;
            transferenciaPlan.activo = true;

            HttpResponseMessage response = await httpClient.PostAsJsonAsync<TransferenciaPlan>("api/Plan/EditarPlan", transferenciaPlan);

            if (!response.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)response.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + response.StatusCode;
            }

            return errorPropy;
        }

        public static async Task<ErrorPropy> EliminarPlan(string idPlan)
        {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            HttpResponseMessage PlanResponse = await clienteHttp.GetAsync("api/Plan/obtenerPorID/" + idPlan);

            if (!PlanResponse.IsSuccessStatusCode)
            {
                throw new Exception(PlanResponse.StatusCode.ToString());
            }

            TransferenciaPlan tipo = await PlanResponse.Content.ReadAsAsync<TransferenciaPlan>();

            HttpResponseMessage eliminarPlanResponse = await clienteHttp.PostAsJsonAsync<TransferenciaPlan>("api/Plan/eliminarPlan", tipo);

            if (!eliminarPlanResponse.IsSuccessStatusCode)
            {
                error.codigoError = (int)eliminarPlanResponse.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + eliminarPlanResponse.StatusCode;
                return error;
            }

            return error;
        }
    }
}
