using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Servicios
{
    public class TipoUserServicios
    {
        public List<TipoUser> ListarTipoUsers() {
            using (var db = new ApplicationDbContext()) {
                return db.TipoUser.ToList();
            }
        }
    }
}