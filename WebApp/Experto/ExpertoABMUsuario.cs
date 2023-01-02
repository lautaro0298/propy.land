using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Fachada;
using WebApp.Models;
using WebApp.DTO;
using WebApp.Factoria;

namespace WebApp.Experto
{
    public class ExpertoABMUsuario
    {
        public void EditarUsuario(string idUsuario) {
            
        }
        public DTOUsuario PreparDatosParaEdicion(string idUsuario) {
            using (var db = new ApplicationDbContext()) {
                Repository<ApplicationUser> userRepo = new Repository<ApplicationUser>();
                ApplicationUser usuario = userRepo.Buscar(x => x.Id == idUsuario,db).FirstOrDefault();
                DTOUsuario dtoUsuario = new DTOUsuario();
                dtoUsuario.nombre = usuario.nombre;
                dtoUsuario.apellido = usuario.apellido;
                dtoUsuario.nroTelefono = usuario.nroTelefono;
                
                dtoUsuario.permitirSerContactadoPublicante = usuario.permitirSerContactadoPublicante;
                dtoUsuario.permitirSerNotificado = usuario.permitirSerNotificado;
                return dtoUsuario;
            }
                
            /*IRepositorioUsuario<ApplicationUser> repositorioUsuario = new RepositorioUsuario<ApplicationUser>();
            ApplicationUser usuario = repositorioUsuario.ObtenerPorID(idUsuario);

            DTOUsuario dtoUsuario = Creador.CrearDTOUsuario(usuario);*/
            
            

        }
    }
}