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
        public ActionResult ObtenerTodos()
        {
            if (con == null)
            {
                return NotFound();
            }

            List<Plan> listadoPlanes = (from x in con.Plan.Include(x => x.TipoMoneda)
                                        where x.activo == true
                                        orderby x.nombrePlan ascending
                                        select x).ToList();
            return Ok(listadoPlanes);
        }
        [HttpGet("obtenerPorID/{id}")]
        public ActionResult ObtenerPorID(string id)
        {
            if (con == null)
            {
                return NotFound();
            }

            Plan plan = (from x in con.Plan.Include(x => x.TipoMoneda)
                         where x.planId == id
                         orderby x.nombrePlan
                         select x).FirstOrDefault();
            if (plan == null)
            {
                return NotFound();
            }

            return Ok(plan);
        }

        [HttpPost("crearPlan")]
        public ActionResult CrearPlan([FromBody] Plan plan)
        {
            if (con == null || plan == null)
            {
                return BadRequest();
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Add(plan);
                    con.SaveChanges();
                    db.Commit();
                    return Ok("Plan created successfully.");
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        [HttpPost("editarPlan")]
        public ActionResult EditarPlan([FromBody] Plan plan)
        {
            if (con == null || plan == null)
            {
                return BadRequest();
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(plan).State = EntityState.Modified;
                    con.SaveChanges();
                    db.Commit();
                    return Ok("Plan updated successfully.");
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        [HttpPost("eliminarPlan/{id}")]
        public ActionResult eliminarPlan(string id)
        {
            if (con == null)
            {
                return NotFound();
            }

            Plan plan = (from x in con.Plan.Include(x => x.TipoMoneda)
                         where x.planId == id
                         orderby x.nombrePlan
                         select x).FirstOrDefault();
            if (plan == null)
            {
                return NotFound();
            }

            using (var db = con.Database.BeginTransaction())
            {
                try
                {
                    con.Entry(plan).State = EntityState.Deleted;
                    con.SaveChanges();
                    db.Commit();
                    return Ok("Plan deleted successfully.");
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
    }
}
