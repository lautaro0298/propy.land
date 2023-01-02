using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using LibreriaExperto;
using LibreriaExperto.Usuarios;
using MercadoPago;
using MercadoPago.Resources;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaMercadoPago.Comunicacion_MercadoPago
{
    public class ExpertoNotification
    {
        private string ClientID = "310547333208529";
        private string ClientSecret = "bQBIrPmu2I8OacRxbknqGR4wlPnvWw0O";
        private string AccessToken = "TEST-310547333208529-010219-d8cdcce154be5b8148af156c1502b1f1-167342294";

        public (DTONotification, Preference,TransferenciaPagoMP) ProcesarEstado(string payment_id, string preference_id)
        {
            MercadoPago.SDK.CleanConfiguration();
            MercadoPago.SDK.ClientId = ClientID;
            MercadoPago.SDK.ClientSecret = ClientSecret;
            MercadoPago.SDK.AccessToken = AccessToken;

            DTONotification DTOnotification = new DTONotification();
            Preference preference = new Preference();
            Payment payment = new Payment();
            TransferenciaPagoMP Pago = null;

            if (payment_id == null || preference_id == null)
            {
                if (payment_id == null) throw new HttpListenerException((int)new HttpResponseMessage(HttpStatusCode.BadRequest).StatusCode, new ArgumentNullException("payment_id was null").Message);
                if (preference_id == null) throw new HttpListenerException((int)new HttpResponseMessage(HttpStatusCode.BadRequest).StatusCode, new ArgumentNullException("preference_id was null").Message);
            }
            else
            {
                (Payment payment, Preference preference) result = BuscarObjetos(payment_id, preference_id);
                payment = result.payment;
                preference = result.preference;

                switch ((int)payment.Status.Value)
                {
                    case 0: //pending todavia no se completa el pago
                        DTOnotification.NombrePlan = preference.Items.FirstOrDefault().Title;
                        DTOnotification.StatusPayment = "pendiente, una vez acreditado se le asignará el plan.";
                        DTOnotification.Aprobado = false;
                        break;
                    case 1: //approved el pago fue aceptado y acreditado
                        Pago = ProcesarPagoMP(payment,preference);
                        DTOnotification.NombrePlan = preference.Items.FirstOrDefault().Title;
                        DTOnotification.StatusPayment = "aprobado, desde Propy esperamos que lo disfrutes.";
                        DTOnotification.Aprobado = true;
                        break;
                    case 2: //authorized autorizado pero no capturado
                        DTOnotification.NombrePlan = preference.Items.FirstOrDefault().Title;
                        DTOnotification.StatusPayment = "autorizado pero todavía no se acepta, una vez aceptado se le asignará el plan.";
                        DTOnotification.Aprobado = false;
                        break;
                    case 3: //in_process el pago está en revisión
                        DTOnotification.NombrePlan = preference.Items.FirstOrDefault().Title;
                        DTOnotification.StatusPayment = "siendo revisado.";
                        DTOnotification.Aprobado = false;
                        break;
                    case 5: //rejected el pago fue rechazado. El usuario puede reintentar el pago
                        DTOnotification.NombrePlan = preference.Items.FirstOrDefault().Title;
                        DTOnotification.StatusPayment = "rechazado, por favor vuelva a intentar.";
                        DTOnotification.Aprobado = false;
                        break;
                    case 6: //cancelled El pago fue cancelado por una de las partes o el pago expiró.
                        DTOnotification.NombrePlan = preference.Items.FirstOrDefault().Title;
                        DTOnotification.StatusPayment = "cancelado o expiró, por favor vuelva a intentar.";
                        DTOnotification.Aprobado = false;
                        break;
                    case 7: //refunded El pago fue devuelto al usuario.
                        DTOnotification.NombrePlan = preference.Items.FirstOrDefault().Title;
                        DTOnotification.StatusPayment = "rechazado, su pago fue devuelto.";
                        DTOnotification.Aprobado = false;
                        break;
                    case 8: //charged_back Se ha realizado un contracargo en la tarjeta de crédito del comprador.
                        DTOnotification.NombrePlan = preference.Items.FirstOrDefault().Title;
                        DTOnotification.StatusPayment = "rechazado, se ha realizado un contracargo a su tarjeta de crédito.";
                        DTOnotification.Aprobado = false;
                        break;
                }
            }
            return (DTOnotification, preference,Pago);
        }
        private (Payment, Preference) BuscarObjetos(string payment_id, string preference_id)
        {
            Payment payment = Payment.FindById(Convert.ToInt32(payment_id));

            Preference preference = Preference.FindById(preference_id);

            if (payment == null) throw new NullReferenceException("payment not found");
            if (preference == null) throw new NullReferenceException("preference not found");

            return (payment, preference);
        }
        private TransferenciaPagoMP ProcesarPagoMP(Payment payment,Preference preference)
        {
            HttpClient httpClient = ApiConfiguracion.Inicializar();
            TransferenciaPagoMP Pago = new TransferenciaPagoMP();
            (ErrorPropy errorPropy, TransferenciaUsuario transferenciaUsuario) = ExpertoUsuarios.ObtenerUsuario(preference.Payer.Value.Email,httpClient);

            Pago.PagoMPId = System.Guid.NewGuid().ToString();
            Pago.idPago = payment.Id.ToString();
            Pago.currency_id = payment.CurrencyId.ToString();
            Pago.date_approved = payment.DateApproved.Value;
            Pago.date_created = payment.DateApproved.Value;
            Pago.transaction_amount =(float) payment.TransactionAmount;
            Pago.status = payment.Status.Value.ToString();
            Pago.operation_type = payment.OperationType.Value.ToString();
            Pago.payment_type_id = payment.PaymentTypeId.Value.ToString();
            Pago.UsuarioId = transferenciaUsuario.usuarioId;

            return Pago;
        }
    }
}
