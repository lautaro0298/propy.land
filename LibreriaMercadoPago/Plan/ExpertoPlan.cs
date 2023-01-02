using LibreriaClases;
using LibreriaClases.Transferencia;
using MercadoPago.DataStructures.Customer;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
using System.Linq;
using LibreriaClases.DTO;
using LibreriaExperto.Usuarios;
using LibreriaExperto;
using LibreriaMercadoPago.Factura;

namespace LibreriaMercadoPago.Plan
{
    public class ExpertoPlan
    {
        public bool YaTienePlan(string user)
        {
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            var Existe = false;

            var tareaHayPlan = httpClient.GetAsync("api/PlanUsuario/ObtenerPorID/" + user);
            tareaHayPlan.Wait();

            if (!tareaHayPlan.Result.IsSuccessStatusCode)
            {
                ErrorPropy errorPropy = new ErrorPropy();
                errorPropy.codigoError = (int)tareaHayPlan.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaHayPlan.Result.StatusCode;
                throw new Exception(errorPropy.descripcionError);
            }
            else
            {
                TransferenciaPlanUsuario planUsuario = tareaHayPlan.Result.Content.ReadAsAsync<TransferenciaPlanUsuario>().Result;

                if (planUsuario == null)
                {
                    Existe = false;
                }
                else
                {
                    Existe = true;
                }
            }
            return Existe;
        }


        /// <summary>
        /// Obtiene un objeto de PlanUsuario a través del ID del usario
        /// </summary>
        /// <returns>
        /// Retorna un DTOPlanUsuario
        /// </returns>

        public DTOPlanUsuario ObtenerPlanUsuario(string user)
        {
            DTOPlanUsuario dTOPlanUsuario = new DTOPlanUsuario();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerPlanUsuario = httpClient.GetAsync("api/PlanUsuario/ObtenerPorID/" + user);
            tareaTraerPlanUsuario.Wait();

            if (!tareaTraerPlanUsuario.Result.IsSuccessStatusCode)
            {
                ErrorPropy errorPropy = new ErrorPropy();
                errorPropy.codigoError = (int)tareaTraerPlanUsuario.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerPlanUsuario.Result.StatusCode;
                throw new Exception(errorPropy.descripcionError);
            }
            else
            {
                TransferenciaPlanUsuario transferenciaPlanUsuario = tareaTraerPlanUsuario.Result.Content.ReadAsAsync<TransferenciaPlanUsuario>().Result;

                dTOPlanUsuario.activo = transferenciaPlanUsuario.activo;
                dTOPlanUsuario.cantidadCreditosActivos = transferenciaPlanUsuario.cantidadCreditosActivos;
                dTOPlanUsuario.CreditoPaqueteId = transferenciaPlanUsuario.CreditoPaqueteId;
                dTOPlanUsuario.FechaCompra = transferenciaPlanUsuario.FechaCompra;
                dTOPlanUsuario.fechaContratacion = transferenciaPlanUsuario.fechaContratacion;
                dTOPlanUsuario.fechaVencimiento = transferenciaPlanUsuario.fechaVencimiento;
                dTOPlanUsuario.PagoMPId = transferenciaPlanUsuario.PagoMPId;
                dTOPlanUsuario.NumFactura = transferenciaPlanUsuario.NumFactura;
                dTOPlanUsuario.planId = transferenciaPlanUsuario.planId;
                dTOPlanUsuario.planUsuarioId = transferenciaPlanUsuario.planUsuarioId;
                dTOPlanUsuario.usuarioId = transferenciaPlanUsuario.usuarioId;

            }
            return dTOPlanUsuario;
        }


        /// <summary>
        /// Obtiene el plan asignado al usuario por ID del usuario
        /// </summary>
        /// <returns>
        /// Retorna un Error y un DTOPlan
        /// </returns>
        public (ErrorPropy, DTOPlan) ObtenerPlanAsociado(string user)
        {
            ErrorPropy errorPropy = new ErrorPropy();
            DTOPlan dTOPlan = new DTOPlan();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerPlanUsuario = httpClient.GetAsync("api/PlanUsuario/ObtenerPorID/" + user);
            tareaTraerPlanUsuario.Wait();

            if (!tareaTraerPlanUsuario.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerPlanUsuario.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerPlanUsuario.Result.StatusCode;
                dTOPlan = null;
            }
            else
            {
                TransferenciaPlanUsuario transferenciaPlanUsuario = tareaTraerPlanUsuario.Result.Content.ReadAsAsync<TransferenciaPlanUsuario>().Result;
                var result = ObtenerPlan_Con_Id(transferenciaPlanUsuario.planId);

                dTOPlan = result;
            }
            return (errorPropy, dTOPlan);
        }


        /// <summary>
        ///  Obtiene un plan por ID, ID corresponde al ID del plan
        /// </summary>
        /// <returns>
        /// Retorna un Error y un DTOPlan
        /// </returns>
        public DTOPlan ObtenerPlan_Con_Id(string ID)
        {
            ErrorPropy errorPropy = new ErrorPropy();
            DTOPlan dTOPlan = new DTOPlan();
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            var tareaTraerPlan = httpClient.GetAsync("api/Plan/obtenerPorID/" + ID);
            tareaTraerPlan.Wait();

            if (!tareaTraerPlan.Result.IsSuccessStatusCode)
            {
                errorPropy.codigoError = (int)tareaTraerPlan.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraerPlan.Result.StatusCode;
                throw new Exception(errorPropy.descripcionError);
            }
            else
            {
                TransferenciaPlan transferenciaPlan = tareaTraerPlan.Result.Content.ReadAsAsync<TransferenciaPlan>().Result;
                if (transferenciaPlan == null)
                {
                    dTOPlan = null;
                }
                else
                {
                    dTOPlan.Vencimiento = transferenciaPlan.Vencimiento;
                    dTOPlan.planId = transferenciaPlan.planId;
                    dTOPlan.cantidadCreditosIniciales = transferenciaPlan.cantidadCreditosIniciales;
                    dTOPlan.nombrePlan = transferenciaPlan.nombrePlan;
                    dTOPlan.accesoEstadisticasAvanzadas = transferenciaPlan.accesoEstadisticasAvanzadas;
                    dTOPlan.cantidadMaxImagenesPermitidasPorPublicacion = transferenciaPlan.cantidadMaxImagenesPermitidasPorPublicacion;
                    dTOPlan.permiteVideo = transferenciaPlan.permiteVideo;
                    dTOPlan.precioPlan = transferenciaPlan.precioPlan;
                    dTOPlan.tipoMoneda = new DTOTipoMoneda { denominacionMoneda = transferenciaPlan.TipoMoneda.denominacionMoneda, tipoMonedaId = transferenciaPlan.TipoMoneda.tipoMonedaId };
                }
            }
            return (dTOPlan);
        }

        public (ErrorPropy, DTOPlan) ConsultarPlan(string user)
        {

            ExpertoPlan expertoPlan = new ExpertoPlan();
            (ErrorPropy errorPropy, DTOPlan dTOPlan) result = expertoPlan.ObtenerPlanAsociado(user);

            return result;
        }

        #region Asignar Plan al Usuario
        public void AsignarPlan(Preference preference, TransferenciaPagoMP Pago)
        {
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            Payer payer = new Payer();
            payer = (Payer)preference.Payer;

            MercadoPago.DataStructures.Preference.Item item = new MercadoPago.DataStructures.Preference.Item();
            item = preference.Items.FirstOrDefault();

            (ErrorPropy errorPropy, TransferenciaUsuario User) = ExpertoUsuarios.ObtenerUsuario(payer.Email, httpClient);

            var tareaTraerNumFactura = httpClient.GetAsync("api/PlanUsuario/ObtenerNumeroFactura");
            tareaTraerNumFactura.Wait();

            if (!tareaTraerNumFactura.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaTraerNumFactura.Result.StatusCode.ToString());
            }

            long NumFactura = tareaTraerNumFactura.Result.Content.ReadAsAsync<long>().Result;
            ExpertoFactura.NumeroFactura = NumFactura;
            ExpertoFactura.NumeroFactura++;
            ExpertoFactura expertoFactura = new ExpertoFactura();
            expertoFactura.Hacer_Facturacion(item, User, Pago);
        }
        #endregion
    }
}
