using API_Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private Timer _timer;
        public DailyCleanupServiceController(ConexionDB conexion) {
            con = conexion;
            _timer = new Timer(24 * 60 * 60 * 1000); // 24 horas en milisegundos
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            var now = DateTime.Now;
            var expiredPublications = con.Set<Publicacion>()
                .Where(p => p.fechaFinPublicacion <= now)
                .ToList();

            // Eliminar las publicaciones vencidas
            foreach (var publication in expiredPublications)
            {
                con.Set<Publicacion>().Remove(publication);
            }

            con.SaveChanges();
        }
    
    }
}
