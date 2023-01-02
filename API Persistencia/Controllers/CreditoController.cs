using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API_Persistencia.Models;
using LibreriaClases.Transferencia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private ConexionDB con;
        public CreditoController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpGet("obtenerCredito")]
        public List<Credito> ObtenerTodos()
        {
            List<Credito> listadoCredito = (from x in con.Credito.Include(x => x.TipoMoneda)
                                        where x.Activo == true
                                        orderby x.NombrePack ascending
                                        select x).ToList();
            return listadoCredito;
        }
        [HttpPost("crearPack")]
        public ActionResult CrearPlan(Credito credito)
        {
         


            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(credito);
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
            return Ok();
        }
        [HttpGet("Obtener_Packs_Creditos")]
        public List<Credito> Obtener_Packs_Creditos()
        {
            List<Credito> creditos = (from x in con.Credito.Include(x => x.TipoMoneda) where x.Activo == true orderby x.Precio ascending select x).ToList();

            return creditos;
        }

        [HttpGet("ObtenerPorID/{id}")]
        public Credito ObtenerPorID(string id)
        {
            Credito credito = (from x in con.Credito.Include(x => x.TipoMoneda) where x.PaqueteID == id select x).FirstOrDefault();

            return credito;
        }
        [HttpPost("EditarCredito")]
        public ActionResult EditarCaracteristica(Credito credito)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(credito).State = EntityState.Modified;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
            return Ok();
        }
        [HttpPost("eliminarCredito")]
        public ActionResult eliminarCredito(Credito credito)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(credito).State = EntityState.Deleted;
                    con.SaveChanges();
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
                return Ok();
            }
        }
    }
}
