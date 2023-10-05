using API_Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace API_Persistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyCleanupServiceController : ControllerBase
    {
        private ConexionDB con;
        private DateTime lastExecutionTime = DateTime.MinValue;

        public DailyCleanupServiceController(ConexionDB conexion) {
            con = conexion;
           
        }
        private void PerformCleanup()
        {
            var now = DateTime.Now;
            var expiredPublications = con.Set<Publicacion>()
                .Where(p => p.fechaFinPublicacion <= now)
                .ToList();

            // Eliminar las publicaciones vencidas
            foreach (var publication in expiredPublications)
            {
                con.Publicacion.Remove(publication);
                var visitasRelacionadas = con.VisitaInmueble.Where(v => v.publicacionId == publication.publicacionId).ToList();

                // Si existen visitas relacionadas, elimina las referencias
                foreach (var visita in visitasRelacionadas)
                {
                    visita.publicacionId = null;
                }
            }

            con.SaveChanges();
        }
        [HttpGet("BorrarPublicaciones")]
        public ActionResult CleanupIfNeeded()
        {
            var now = DateTime.Now;
            var timeSinceLastExecution = now - lastExecutionTime;

            if (timeSinceLastExecution >= TimeSpan.FromHours(24))
            {
                using (var db = con.Database.BeginTransaction())
                {
                    try
                    {
                        PerformCleanup();
                        db.Commit();
                    }
                    catch (Exception)
                    {
                        db.Rollback();
                        throw;
                    }
                }

                lastExecutionTime = now;
            }

            return Ok();
        }
    }
    
    
}
