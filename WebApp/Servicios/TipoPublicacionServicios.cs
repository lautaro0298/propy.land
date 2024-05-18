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
        public List<SimpleTipoPublicacionDto> ListarTipoPublicacionesSimple()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.TipoPublicacion.Select(tp => new SimpleTipoPublicacionDto
                {
                    TipoPublicacionId = tp.tipoPublicacionId,
                    NombreTipoPublicacion = tp.nombreTipoPublicacion,
                    Activo = tp.activo
                }).ToList();
            }
        }
        public class SimpleTipoPublicacionDto
        {
            public Guid TipoPublicacionId { get; set; }
            public string NombreTipoPublicacion { get; set; }
            public bool Activo { get; set; }
        }
    }
}