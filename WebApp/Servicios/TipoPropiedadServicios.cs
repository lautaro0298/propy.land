using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Servicios
{
    public class TipoPropiedadServicios
    {
        public List<TipoPropiedad> ListarTipoPropiedades() {
            using (var db = new ApplicationDbContext()) {
                return db.TipoPropiedad.ToList();
            }
        }
    }
}