using MercadoPago.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercadoPago.DataStructures.Preference;
using LibreriaClases.Transferencia;
using LibreriaMercadoPago.Credito;
using LibreriaClases.DTO;
using System.Net.Http;
using LibreriaExperto;
using LibreriaMercadoPago.Plan;
using LibreriaClases;
using LibreriaExperto.Usuarios;

namespace LibreriaMercadoPago.Factura
{
    public class ExpertoFactura
    {
        public static long NumeroFactura { get; set; }
        public void Hacer_Facturacion(Item item, TransferenciaUsuario payer, TransferenciaPagoMP Pago)
        {

            HttpClient httpClient = ApiConfiguracion.Inicializar();
            TransferenciaPlanUsuario transferenciaPlanUsuario = new TransferenciaPlanUsuario();

            ExpertoPlan expertoPlan = new ExpertoPlan();
            DTOPlan dTOPlan = expertoPlan.ObtenerPlan_Con_Id(item.Id);

            if (dTOPlan != null)
            {
                transferenciaPlanUsuario.planId = dTOPlan.planId;
                transferenciaPlanUsuario.cantidadCreditosActivos = dTOPlan.cantidadCreditosIniciales;
                transferenciaPlanUsuario.fechaContratacion = DateTime.Now;
                transferenciaPlanUsuario.fechaVencimiento = DateTime.Now.AddMonths(dTOPlan.Vencimiento);
            }
            else
            {
                transferenciaPlanUsuario.CreditoPaqueteId = item.Id;
            }

            transferenciaPlanUsuario.activo = true;
            transferenciaPlanUsuario.FechaCompra = DateTime.Now;
            transferenciaPlanUsuario.planUsuarioId = System.Guid.NewGuid().ToString();
            transferenciaPlanUsuario.PagoMPId = Pago.PagoMPId;
            transferenciaPlanUsuario.Pago = Pago;
            transferenciaPlanUsuario.usuarioId = payer.usuarioId;
            transferenciaPlanUsuario.NumFactura = NumeroFactura;

            var tareaGuardarFactura = httpClient.PostAsJsonAsync<TransferenciaPlanUsuario>("api/PlanUsuario/CrearPlanUsuario", transferenciaPlanUsuario);
            tareaGuardarFactura.Wait();

            if (!tareaGuardarFactura.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaGuardarFactura.Result.StatusCode.ToString());
            }

        }

        public DTO_PU_Y_User ConsultarFacturacion(string User)
        {
            DTO_PU_Y_User dTO_PU_Y_User = new DTO_PU_Y_User();

            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerFacturas = httpClient.GetAsync("api/PlanUsuario/ObtenerTodosPorID/" + User);
            tareaTraerFacturas.Wait();

            if (!tareaTraerFacturas.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaTraerFacturas.Result.StatusCode.ToString());
            }
            else
            {
                List<TransferenciaPlanUsuario> transferenciaPlanUsuarios = tareaTraerFacturas.Result.Content.ReadAsAsync<List<TransferenciaPlanUsuario>>().Result;

                if (transferenciaPlanUsuarios.Count == 0)
                {
                    dTO_PU_Y_User.dTOs = null;
                }
                else
                {
                    foreach (var PlanUser in transferenciaPlanUsuarios)
                    {

                        DTOPlanUsuario dTOPlanUsuario = new DTOPlanUsuario();
                        DTOPlan dTOPlan = new DTOPlan();
                        DTOCredito dTOCredito = new DTOCredito();
                        DTOPagoMP dTOPagoMP = new DTOPagoMP();

                        TransferenciaPlan transferenciaPlan = PlanUser.Plan;
                        TransferenciaCredito transferenciaCredito = PlanUser.Credito;
                        TransferenciaPagoMP transferenciaPagoMP = PlanUser.Pago;

                        dTOPagoMP.payment_type_id = transferenciaPagoMP.payment_type_id;
                        dTOPagoMP.status = transferenciaPagoMP.status;

                        if (transferenciaPlan == null)
                        {
                            dTOCredito.NombrePack = transferenciaCredito.NombrePack;
                            dTOCredito.Precio = transferenciaCredito.Precio;
                            dTOCredito.TipoMoneda = new DTOTipoMoneda { denominacionMoneda = transferenciaCredito.TipoMoneda.denominacionMoneda };
                            dTOPlanUsuario.DTOCredito = dTOCredito;
                            dTOPlanUsuario.DTOPlan = null;
                        }
                        else
                        {
                            dTOPlan.nombrePlan = transferenciaPlan.nombrePlan;
                            dTOPlan.precioPlan = transferenciaPlan.precioPlan;
                            dTOPlan.tipoMoneda = new DTOTipoMoneda { denominacionMoneda = transferenciaPlan.TipoMoneda.denominacionMoneda };
                            dTOPlan.Vencimiento = transferenciaPlan.Vencimiento;
                            dTOPlanUsuario.DTOPlan = dTOPlan;
                            dTOPlanUsuario.DTOCredito = null;
                        }
                        dTOPlanUsuario.NumFactura = PlanUser.NumFactura;
                        dTOPlanUsuario.FechaCompra = PlanUser.FechaCompra;
                        dTOPlanUsuario.DTOPagoMP = dTOPagoMP;

                        dTO_PU_Y_User.dTOs.Add(dTOPlanUsuario);
                    }
                    (ErrorPropy error, TransferenciaUsuario transferenciaUsuario) = ExpertoUsuarios.ObtenerUsuarioPorID(User, httpClient);

                    dTO_PU_Y_User.User = transferenciaUsuario;
                }
            }
            return dTO_PU_Y_User;
        }
    }
}
