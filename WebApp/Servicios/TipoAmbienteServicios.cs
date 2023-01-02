using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Servicios
{
    public class TipoAmbienteServicios
    {
        public List<TipoAmbiente> ListarTipoAmbientes() {
            using (var db = new ApplicationDbContext()) {
                return db.TipoAmbiente.ToList();
            }
        }
    }
}