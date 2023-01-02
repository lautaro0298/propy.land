using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using LibreriaExperto;
using LibreriaExperto.Publicaciones;
using LibreriaExperto.Usuarios;
using LibreriaMercadoPago.Factura;
using LibreriaMercadoPago.Plan;
using MercadoPago.DataStructures.Customer;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaMercadoPago.Credito
{
    public class ExpertoCredito
    {
        public List<DTOCredito> TraerPackCreditos()
        {
            List<DTOCredito> dTOCreditos = new List<DTOCredito>();
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            var tareaTraerPacksCreditos = httpClient.GetAsync("api/Credito/Obtener_Packs_Creditos");
            tareaTraerPacksCreditos.Wait();

            if (!tareaTraerPacksCreditos.Result.IsSuccessStatusCode)
            {
                ErrorPropy error = new ErrorPropy();
                error.codigoError = (int)tareaTraerPacksCreditos.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaTraerPacksCreditos.Result.StatusCode;
                throw new Exception(error.codigoError + error.descripcionError);
            }
            else
            {
                List<TransferenciaCredito> transferenciaCredito = tareaTraerPacksCreditos.Result.Content.ReadAsAsync<List<TransferenciaCredito>>().Result;

                foreach (var packCredito in transferenciaCredito)
                {
                    DTOCredito dTOCredito = new DTOCredito();
                    dTOCredito.CantidadCreditos = packCredito.CantidadCreditos;
                    dTOCredito.PaqueteID = packCredito.PaqueteID;
                    dTOCredito.Precio = packCredito.Precio;
                    dTOCredito.NombrePack = packCredito.NombrePack;
                    dTOCredito.TipoMoneda = new DTOTipoMoneda { denominacionMoneda = packCredito.TipoMoneda.denominacionMoneda, tipoMonedaId = packCredito.TipoMoneda.tipoMonedaId };

                    dTOCreditos.Add(dTOCredito);
                }
            }
            return dTOCreditos;
        }

        public DTOCredito TraerPackCredito_ID(string id, HttpClient httpClient)
        {
            var tareaTraeePackCreditos_ID = httpClient.GetAsync("api/Credito/ObtenerPorID/" + id);
            DTOCredito dTOCredito = new DTOCredito();

            if (!tareaTraeePackCreditos_ID.Result.IsSuccessStatusCode)
            {
                ErrorPropy errorPropy = new ErrorPropy();
                errorPropy.codigoError = (int)tareaTraeePackCreditos_ID.Result.StatusCode;
                errorPropy.descripcionError = "Error: " + errorPropy.codigoError + " " + tareaTraeePackCreditos_ID.Result.StatusCode;
                throw new Exception(errorPropy.descripcionError);
            }
            else
            {
                TransferenciaCredito transferenciaCredito = tareaTraeePackCreditos_ID.Result.Content.ReadAsAsync<TransferenciaCredito>().Result;

                dTOCredito.Activo = transferenciaCredito.Activo;
                dTOCredito.CantidadCreditos = transferenciaCredito.CantidadCreditos;
                dTOCredito.NombrePack = transferenciaCredito.NombrePack;
                dTOCredito.PaqueteID = transferenciaCredito.PaqueteID;
                dTOCredito.Precio = transferenciaCredito.Precio;
                dTOCredito.TipoMoneda = new DTOTipoMoneda { denominacionMoneda = transferenciaCredito.TipoMoneda.denominacionMoneda, tipoMonedaId = transferenciaCredito.TipoMoneda.tipoMonedaId };
            }
            return dTOCredito;
        }

        public void Reload_credits(Preference preference, TransferenciaPagoMP Pago)
        {
            HttpClient httpClient = ApiConfiguracion.Inicializar();

            Payer payer = new Payer();
            payer = (Payer)preference.Payer;

            Item item = new Item();
            item = (Item)preference.Items.FirstOrDefault();

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
            expertoFactura.Hacer_Facturacion(item, User,Pago);

            reloading(User, item, httpClient);

            RehabilitarPublicaciones(User, httpClient);

        }

        private void reloading(TransferenciaUsuario payer, Item item, HttpClient httpClient)
        {

            ExpertoPlan expertoPlan = new ExpertoPlan();
            DTOPlanUsuario dTOPlanUsuario = expertoPlan.ObtenerPlanUsuario(payer.usuarioId);

            DTOCredito dTOCredito = new DTOCredito();
            dTOCredito = TraerPackCredito_ID(item.Id, httpClient);

            dTOPlanUsuario.cantidadCreditosActivos = dTOPlanUsuario.cantidadCreditosActivos + dTOCredito.CantidadCreditos;

            TransferenciaPlanUsuario transferenciaPlanUsuario = new TransferenciaPlanUsuario();

            transferenciaPlanUsuario.activo = dTOPlanUsuario.activo;
            transferenciaPlanUsuario.cantidadCreditosActivos = dTOPlanUsuario.cantidadCreditosActivos;
            transferenciaPlanUsuario.CreditoPaqueteId = dTOPlanUsuario.CreditoPaqueteId;
            transferenciaPlanUsuario.FechaCompra = dTOPlanUsuario.FechaCompra;
            transferenciaPlanUsuario.fechaContratacion = dTOPlanUsuario.fechaContratacion;
            transferenciaPlanUsuario.fechaVencimiento = dTOPlanUsuario.fechaVencimiento;
            transferenciaPlanUsuario.PagoMPId = dTOPlanUsuario.PagoMPId;
            transferenciaPlanUsuario.NumFactura = dTOPlanUsuario.NumFactura;
            transferenciaPlanUsuario.planId = dTOPlanUsuario.planId;
            transferenciaPlanUsuario.planUsuarioId = dTOPlanUsuario.planUsuarioId;
            transferenciaPlanUsuario.usuarioId = dTOPlanUsuario.usuarioId;
               

            var tareaEditarPlanUsuario = httpClient.PostAsJsonAsync<TransferenciaPlanUsuario>("api/PlanUsuario/editarPlanUsuario", transferenciaPlanUsuario);
            tareaEditarPlanUsuario.Wait();

            if (!tareaEditarPlanUsuario.Result.IsSuccessStatusCode)
            {
                ErrorPropy errorPropy2 = new ErrorPropy();
                errorPropy2.codigoError = (int)tareaEditarPlanUsuario.Result.StatusCode;
                errorPropy2.descripcionError = "Error: " + errorPropy2.codigoError + " " + tareaEditarPlanUsuario.Result.StatusCode;
                throw new Exception(errorPropy2.descripcionError);
            }
        }

        private void RehabilitarPublicaciones(TransferenciaUsuario payer, HttpClient httpClient)    
        {
            var tareaObtenerPublicacionesDelUsuario = httpClient.GetAsync("api/Publicacion/obtenerPublicacionesPorUsuario/" + payer.usuarioId);
            tareaObtenerPublicacionesDelUsuario.Wait();
            if (!tareaObtenerPublicacionesDelUsuario.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerPublicacionesDelUsuario.Result.StatusCode.ToString());
            }
            List<TransferenciaPublicacion> publicacionesUsuario = tareaObtenerPublicacionesDelUsuario.Result.Content.ReadAsAsync<List<TransferenciaPublicacion>>().Result;
            ExpertoPublicaciones.HabilitarDeshabilitarPublicacionesUsuario(publicacionesUsuario, (int)CodigosEstados.Estados.activa, httpClient);
        }
    }
}

