using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API_Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanUsuarioController : Controller
    {
        private ConexionDB con;
        public PlanUsuarioController(ConexionDB conexion)
        {
            con = conexion;
        }

        [HttpGet("ObtenerTodosPorID/{id}")]
        public List<PlanUsuario> ObtenerTodosPorID(string id)
        {
            List<PlanUsuario> planUsuarios = (from x in con.PlanUsuario.Include(x => x.Plan).ThenInclude(x => x.TipoMoneda)
                                              .Include(x => x.Credito).ThenInclude(x => x.TipoMoneda)
                                              .Include(x => x.Pago) where x.usuarioId == id orderby x.NumFactura ascending select x).ToList();

            return planUsuarios;
        }
        
        [HttpGet("ObtenerPorID/{id}")]
        public PlanUsuario ObtenerPorID (string id)
        {
            PlanUsuario planUsuario = (from x in con.PlanUsuario
                                       .Include(x => x.Plan).Include(x => x.Pago)
                                       where (x.usuarioId == id && x.activo==true && x.CreditoPaqueteId == null) select x).FirstOrDefault();

            return planUsuario;
        }
        [HttpGet("ObtenerNumeroFactura")]
        public long ObtenerNumeroFactura()
        {
            long NumeroFactura = (from x in con.PlanUsuario orderby x.NumFactura descending select x.NumFactura).FirstOrDefault();

            return NumeroFactura;
        }
        [HttpPost("CrearPlanUsuario")]
        public ActionResult CrearPlanUsuario(PlanUsuario planUsuario)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(planUsuario);
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
        [HttpPost("editarPlanUsuario")]
        public ActionResult EditarPlanUsuario(PlanUsuario planUsuario) {
            using (var db = con.Database.BeginTransaction()) {
                try
                {
                    con.Entry(planUsuario).State = EntityState.Modified;
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
