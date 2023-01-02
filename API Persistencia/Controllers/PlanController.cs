using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_Persistencia.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private ConexionDB con;
        public PlanController(ConexionDB conexion)
        {
            con = conexion;
        }
        [HttpGet("obtenerPlanes")]
        public List<Plan> ObtenerTodos()
        {
            List<Plan> listadoPlanes = (from x in con.Plan.Include(x => x.TipoMoneda)
                                        where x.activo == true
                                        orderby x.nombrePlan ascending
                                        select x).ToList();
            return listadoPlanes;
        }
        [HttpGet("obtenerPorID/{id}")]
        public Plan ObtenerPorID(string id)
        {
            Plan Plan = (from x in con.Plan.Include(x => x.TipoMoneda)
                         where x.planId == id
                         orderby x.nombrePlan
                         select x).FirstOrDefault();
            return Plan;
        }

        [HttpPost("crearPlan")]
        public ActionResult CrearPlan(Plan plan)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(plan);
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
        [HttpPost("editarPlan")]
        public ActionResult EditarPlan(Plan plan)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(plan).State = EntityState.Modified;
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
        [HttpPost("eliminarPlan")]
        public ActionResult eliminarPlan(Plan plan)
        {
            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(plan).State = EntityState.Deleted;
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