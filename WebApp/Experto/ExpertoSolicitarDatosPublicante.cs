using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DTO;

namespace WebApp.Experto
{
    public class ExpertoSolicitarDatosPublicante
    {
        //Nueva implementacion
        public DTOPublicante SolicitarDatosPublicante(string user,Guid publicacionId) {
            DTOPublicante dtoPublicante = new DTOPublicante();
            using (var db = new ApplicationDbContext()) {
                //Preparo datos para mostrarlos a la interfaz
                var data = (from p in db.Publicacion
                            where p.publicacionId == publicacionId
                            select p).FirstOrDefault();
                
                dtoPublicante.nombrePublicante = data.ApplicationUser.nombre;
                dtoPublicante.apellidoPublicante = data.ApplicationUser.apellido;
                dtoPublicante.email = data.ApplicationUser.Email;
                dtoPublicante.telefono = data.ApplicationUser.nroTelefono;
                
                //Creo instancia de ClickContactoPublicante
                ClickContactoPublicante obj = (from x in db.ClickContactoPublicante
                                               where x.publicacionId == publicacionId && x.UserId == user
                                               select x).FirstOrDefault();
                if (obj == null)
                {
                    ClickContactoPublicante instanciaCCP = new ClickContactoPublicante();
                    instanciaCCP.clickContactoPublicanteId = System.Guid.NewGuid();
                    instanciaCCP.UserId = user;
                    instanciaCCP.publicacionId = publicacionId;
                    instanciaCCP.fechaHoraClickContactoPublicante = DateTime.UtcNow;
                    db.ClickContactoPublicante.Add(instanciaCCP);
                    db.SaveChanges();
                }
                string actividad = "Solicitó los datos del publicante " + data.ApplicationUser.nombre + " " + data.ApplicationUser.apellido + " con el teléfono: "+data.ApplicationUser.nroTelefono;
                Servicios.ServiciosUsuario.RegistrarActividad(actividad,user,db);
            }
            return dtoPublicante;
        }

        
    }
}