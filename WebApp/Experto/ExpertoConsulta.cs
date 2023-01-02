using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DTO;
using WebApp.Fachada;
using WebApp.Models;

namespace WebApp.Experto
{
    public class ExpertoConsulta
    {
        public List<DTOProvincia> ConsultarProvincias(string pais)
        {
            List<DTOProvincia> listaprovincia = new List<DTOProvincia>();
            using (var db = new ApplicationDbContext())
            {
                
            }
            return listaprovincia;
        }

        public List<DTOCiudad> ConsultarCiudades(string provincia)
        {
            List<DTOCiudad> listaciudad = new List<DTOCiudad>();
            using (var db = new ApplicationDbContext())
            {
                
            }
            return listaciudad;
        }

        public DTOCredito ConsultarCredito(string user)
        {
            DTOCredito dTOCredito = new DTOCredito();
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    Repository<PlanUsuario> repositoryPlanUsuario = new Repository<PlanUsuario>();
                    var PlanUsuario = repositoryPlanUsuario.Buscar(x => x.UserId == user, db).FirstOrDefault();
                    if (PlanUsuario == null)
                    {
                        dTOCredito.CreditoActual = 0;
                    }
                    else
                    {
                        dTOCredito.CreditoActual = PlanUsuario.TotalCreditosActivos;
                    }
                }
            }catch(NullReferenceException ex)
            {
                dTOCredito.CreditoActual = 0;
            }

            return dTOCredito;
        }
    }
}