using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Servicios
{
    public class TipoPublicacionServicios
    {
       
        
        public List<TipoPublicacion> ListarTipoPublicaciones() {
            using (var db = new ApplicationDbContext()) {
                return db.TipoPublicacion.ToList();
            }
        }
    }
}