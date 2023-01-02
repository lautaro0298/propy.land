using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Servicios
{
    public class PublicacionEstadoServicios
    {
        public void CrearPublicacionEstado(Guid publicacionId) {
            using (var db = new ApplicationDbContext()) {
                try
                {
                    //Obtengo el Id del estado con el que se guardará la publicacion
                    
                    var dato = db.EstadoPublicacion.Find(Guid.Parse("CDDD0F2D-5C67-46D0-BAE8-9D5AAC393C72"));
                    
                    

                    //Instancio un objeto de tipo PublicacionEstado el cual voy a configurar para agregar a la bd
                    PublicacionEstado pe = new PublicacionEstado();

                    //Configuro fechas
                    DateTime fechaDesde = DateTime.Now;
                    DateTime fechaHasta = fechaDesde.AddDays(15);

                    //Configuro objeto
                    pe.publicacionEstadoId = System.Guid.NewGuid();
                    pe.fechaDesde = fechaDesde;
                    pe.fechaHasta = fechaHasta;
                    pe.publicacionId = publicacionId;
                    pe.estadoPublicacionId = dato.estadoPublicacionId;

                    //Agrego objeto a registro
                    db.PublicacionEstado.Add(pe);
                    //Salvo los cambios
                    db.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}