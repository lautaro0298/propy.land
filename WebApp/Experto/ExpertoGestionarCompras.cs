using MercadoPago.Common;
using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DTO;
using WebApp.Enumerables;
using WebApp.Fachada;
using WebApp.Models;
using WebApp.Servicios;
using Plan = WebApp.Models.Plan;

namespace WebApp.Experto
{
    public class ExpertoGestionarCompras
    {

        public DTOCompras Trolley(string user, DTOCompras dTOCompras)
        {
            MercadoPago.SDK.CleanConfiguration();
            MercadoPago.SDK.ClientId = "6676181862368985";
            MercadoPago.SDK.ClientSecret = "rEm8ib9tejXnIWw5qpgBB7OqsDejL4nX";
            MercadoPago.SDK.AccessToken = "TEST-6676181862368985-081423-d1e488a560e8eed32b3be8d2a78fbb9c-172147688";

            using (var database = new ApplicationDbContext())
            {

                Repository<ApplicationUser> repository1 = new Repository<ApplicationUser>();
                var USER = repository1.BuscarPorId(user, database);

                foreach (var dto in dTOCompras.datosCompras)
                {
                    Preference preference = new Preference();
                    Item item = new Item();
                    Payer payer = new Payer();

                    item.CurrencyId = CurrencyId.ARS;
                    item.Id = dto.ID;
                    item.Title = dto.nombre;
                    item.UnitPrice = dto.precio;
                    item.Quantity = 1;

                    payer.Email = USER.Email;

                    payer.Name = USER.nombre;
                    payer.Surname = USER.apellido;
                    payer.DateCreated = DateTime.Now;

                    preference.Items.Add(item);
                    preference.Payer = payer;
                    preference.Save();

                    dTOCompras.AddPreference(preference);
                }
            }
            return (dTOCompras);
        }


        public void AsignarPlan(Payment payment, MerchantOrder merchant, Preference preference, DTOError dTOError)
        {
            MercadoPago.DataStructures.Payment.Payer payer1 = new MercadoPago.DataStructures.Payment.Payer();
            payer1 = payment.Payer;


            MercadoPago.DataStructures.Preference.Payer payer2 = new MercadoPago.DataStructures.Preference.Payer();//prueba
            payer2 = (MercadoPago.DataStructures.Preference.Payer)preference.Payer;//prueba


            MercadoPago.DataStructures.Preference.Item item = new MercadoPago.DataStructures.Preference.Item();//prueba
            item = (MercadoPago.DataStructures.Preference.Item)preference.Items.First();//prueba

            using (var database = new ApplicationDbContext())
            {
                using (var db = database.Database.BeginTransaction())
                {
                    try
                    {

                        Repository<ApplicationUser> repositoryUser = new Repository<ApplicationUser>();
                        var user = repositoryUser.Buscar(x => x.Email == payer2.Email, database).FirstOrDefault();

                        Repository<Plan> repositoryPlan = new Repository<Plan>();
                        var plan = repositoryPlan.Buscar(x => x.itemId == item.Id, database).FirstOrDefault();

                        Repository<PlanUsuario> repositoryPlanUsuario = new Repository<PlanUsuario>();
                        PlanUsuario planUsuario = new PlanUsuario();
                        planUsuario.planUsuarioID = System.Guid.NewGuid();
                        planUsuario.fechaContratacion = DateTime.Now;
                        planUsuario.activo = true;
                        planUsuario.planID = plan.planId;
                        planUsuario.UserId = user.Id;
                        planUsuario.TotalCreditosActivos = plan.creditos;


                        repositoryPlanUsuario.Crear(planUsuario, database);
                        repositoryPlanUsuario.Guardar(database);
                        db.Commit();
                        dTOError.codigoError = (int)Enums.CodigosError.codNoError;

                    }
                    catch (Exception e)
                    {
                        dTOError.codigoError = (int)Enums.CodigosError.codErrorContratarPlan;
                        dTOError.descripcionError.Add(NotificacionesServicios.errorContratarPlan);
                        db.Rollback();
                    }
                }
            }
        }

        public void RenovarCreditos(Payment payment, MerchantOrder merchant, Preference preference, DTOError dTOError)
        {
            MercadoPago.DataStructures.Payment.Payer payer1 = new MercadoPago.DataStructures.Payment.Payer();
            payer1 = payment.Payer;


            MercadoPago.DataStructures.Preference.Payer payer2 = new MercadoPago.DataStructures.Preference.Payer();//prueba
            payer2 = (MercadoPago.DataStructures.Preference.Payer)preference.Payer;//prueba


            MercadoPago.DataStructures.Preference.Item item = new MercadoPago.DataStructures.Preference.Item();//prueba
            item = (MercadoPago.DataStructures.Preference.Item)preference.Items.First();//prueba

            Guid CopiaItemId = System.Guid.Parse(item.Id);

            using (var database = new ApplicationDbContext())
            {
                using (var db = database.Database.BeginTransaction())
                {
                    try
                    {

                        Repository<ApplicationUser> repositoryUser = new Repository<ApplicationUser>();
                        var user = repositoryUser.Buscar(x => x.Email == payer2.Email, database).FirstOrDefault();

                        Repository<CreditoPlan> repositoryCreditoPlan = new Repository<CreditoPlan>();
                        var CreditoPlan = repositoryCreditoPlan.Buscar(x => x.CreditoPlanID == CopiaItemId, database).FirstOrDefault();

                        Repository<PlanUsuario> repositoryPlanUsuario = new Repository<PlanUsuario>();
                        var planusuario = repositoryPlanUsuario.Buscar(x => x.UserId == user.Id, database).FirstOrDefault();

                        planusuario.TotalCreditosActivos = planusuario.TotalCreditosActivos + CreditoPlan.cantidadTotalCreditoPorPaquete; 

                        repositoryPlanUsuario.Editar(planusuario, database);
                        repositoryPlanUsuario.Guardar(database);
                        db.Commit();
                        dTOError.codigoError = (int)Enums.CodigosError.codNoError;

                    }
                    catch (Exception e)
                    {
                        dTOError.codigoError = (int)Enums.CodigosError.codErrorContratarPlan;
                        dTOError.descripcionError.Add(NotificacionesServicios.errorContratarPlan);
                        db.Rollback();
                    }
                }
            }
        }
    }
}
