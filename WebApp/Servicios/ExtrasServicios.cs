using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Servicios
{
    public class ExtrasServicios
    {
        public List<Extras> ListarExtras() {
            using (var db = new ApplicationDbContext()) {
                var data = from e in db.Extras
                           orderby e.nombreExtra ascending
                           select e;
                return data.ToList();
            }
        }
        
    }
}