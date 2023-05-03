using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DTO;
using WebApp.Enumerables;
using WebApp.Fachada;
using WebApp.Models;
using WebApp.Servicios;

namespace WebApp.Experto
{
    public static class GestionarFavoritosExperto
    {
        public static List<DTOFavorito> ListarFavoritos(string usuarioId) {
            Repository<Favorito> favoritoRepo = new Repository<Favorito>();
            using (var db = new ApplicationDbContext()) {
                List<Favorito> favoritos = favoritoRepo.Buscar(x => x.usuarioId == usuarioId && x.activo==true,db);
                List<DTOFavorito> listadoFavoritos = new List<DTOFavorito>();
                foreach (var favorito in favoritos) {
                    DTOFavorito dtoFavorito = new DTOFavorito();
                    dtoFavorito.direccionPropiedad = favorito.Publicacion.Propiedad.direccionFormateada;
                    dtoFavorito.precio = String.Format("{0:c}",favorito.Publicacion.precioPropiedad);
                    dtoFavorito.usuarioId = usuarioId;
                    dtoFavorito.publicacionId = favorito.publicacionId;
                    foreach (var imagen in favorito.Publicacion.ImagenPublicacion) {
                        if (imagen.imagenRepresentativa==true) {
                            dtoFavorito.rutaImagen = ImagenServicios.ObtenerRutaRelativa(imagen);
                        }
                    }
                    
                    listadoFavoritos.Add(dtoFavorito);
                }
                return listadoFavoritos;
            }
                
        }
        public static DTOError QuitarFavorito(string usuarioID, Guid publicacionID) {
            using (var db = new ApplicationDbContext()) {
                using (var database = db.Database.BeginTransaction()) {
                    DTOError error = new DTOError();
                    try
                    {
                        
                        Repository<Favorito> favoritoRepo = new Repository<Favorito>();
                        Favorito favorito = favoritoRepo.Buscar(x => x.publicacionId == publicacionID && x.usuarioId == usuarioID, db).FirstOrDefault();
                        if (favorito != null)
                        {
                            favorito.activo = false;
                            favoritoRepo.Editar(favorito, db);
                            favoritoRepo.Guardar(db);
                            database.Commit();
                            error.codigoError = (int)Enums.CodigosError.codNoError;

                        }
                    }
                    catch (Exception)
                    {
                        error.codigoError = (int)Enums.CodigosError.codErrorNuevaPublicacion;
                       
                    }
                    return error;
                    
                }
            }
        }
        public static DTOError AgregarFavorito(string usuarioId,Guid publicacionId) {
            DTOError error = new DTOError();
            using (var db = new ApplicationDbContext()) {
                using (var database = db.Database.BeginTransaction()) {
                    try
                    {
                        Repository<Favorito> favoritoRepo = new Repository<Favorito>();
                        #region Verificio si ya agrego la publicacion a su lista
                        if (favoritoRepo.Buscar(x => x.usuarioId == usuarioId && x.publicacionId == publicacionId && x.activo==true, db).Count > 0)
                        {
                            error.codigoError = 1;
                            return error;
                        }
                        
                        #endregion
                        
                        Favorito favorito = new Favorito();
                        string favoritoId = System.Guid.NewGuid().ToString();
                        favorito.favoritoId = favoritoId;
                        //favorito.fechaSeleccion = DateTime.UtcNow;
                        favorito.publicacionId = publicacionId;
                        favorito.usuarioId = usuarioId;
                        favorito.activo = true;

                        favoritoRepo.Crear(favorito, db);
                        favoritoRepo.Guardar(db);
                        database.Commit();
                        error.codigoError = (int)Enums.CodigosError.codNoError;
                        
                    }
                    catch (Exception)
                    {

                        error.codigoError = (int)Enums.CodigosError.codErrorAgregarFavorito;
                        error.descripcionError.Add(NotificacionesServicios.errorNuevaPublicacion);
                        database.Rollback();
                    }
                    return error;

                }
            }
        }   
    }
}