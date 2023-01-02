using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
namespace WebApp.Servicios
{
    public class TipoConstruccionServicios
    {
        public List<TipoConstruccion> ListarTipoConstrucciones() {
            using (var db = new ApplicationDbContext()) {
                return db.TipoConstruccion.ToList();
            }
        }
    }
}