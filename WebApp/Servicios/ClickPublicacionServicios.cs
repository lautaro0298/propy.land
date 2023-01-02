using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Servicios
{
    public class ClickPublicacionServicios
    {
        public void Crear(string userId, Guid publicacionId) {
            using (var db = new ApplicationDbContext()) {

                //Me aseguro que a un publicante no se le descuente un click de un usuario que ya vio su publicacion
                var data = (from clickPub in db.ClickPublicacion
                            where clickPub.UserId == userId && clickPub.publicacionId.ToString() == publicacionId.ToString()
                            select clickPub).FirstOrDefault();
                //Si devuelve null significa que nunca vio su publicación
                if (data == null)
                {
                    ClickPublicacion cp = new ClickPublicacion();
                    cp.clickPublicacionId = System.Guid.NewGuid();
                    cp.fechaHoraClickPublicacion = DateTime.Now;
                    cp.publicacionId = publicacionId;
                    cp.UserId = userId;
                    //cp.cantidadVisitas = 1;
                    db.ClickPublicacion.Add(cp);
                    db.SaveChanges();
                }
                else {
                    Editar(data.clickPublicacionId);
                }

                
            }
        }
        public void Editar(Guid clickPublicacionId) {
            using (var db = new ApplicationDbContext()) {
                var clickPubAEditar = db.ClickPublicacion.Find(clickPublicacionId);
                //clickPubAEditar.cantidadVisitas = clickPubAEditar.cantidadVisitas + 1;
                db.Entry(clickPubAEditar).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}