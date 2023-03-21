using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaClases.Transferencia;
using LibreriaExperto.Mensajeria;
using LibreriaExperto.Publicaciones;
using LibreriaExperto.Seguridad;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaExperto.Usuarios
{

    public static class ExpertoUsuarios
    {
        public static (ErrorPropy, List<DTOFavorito>) ConsultarListaFavoritos(string usuarioId) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            (ErrorPropy error, TransferenciaUsuario usuario) respuestaObtenerUsuario = ExpertoUsuarios.ObtenerUsuarioPorID(usuarioId,clienteHttp);
            if (respuestaObtenerUsuario.error.codigoError!=0) {
                return (respuestaObtenerUsuario.error,null);
            }
            
            List<DTOFavorito> favoritos = new List<DTOFavorito>();
            foreach (var favorito in respuestaObtenerUsuario.usuario.Favorito) {
                (ErrorPropy error, TransferenciaPublicacion publicacion) respuestaObtenerPublicacion = ExpertoPublicaciones.ObtenerPublicacion(favorito.publicacionId,clienteHttp);
                if (respuestaObtenerPublicacion.error.codigoError!=0) {
                    return (respuestaObtenerPublicacion.error, null);
                }
                if (favorito.activo!=false) {
                    DTOFavorito fav = new DTOFavorito();
                    fav.publicacionId = respuestaObtenerPublicacion.publicacion.publicacionId;
                    fav.ubicación = respuestaObtenerPublicacion.publicacion.Propiedad.ubicacion;
                    int f = 0;
                    foreach (var g in respuestaObtenerPublicacion.publicacion.Propiedad.TipoPropiedad)
                    {
                        fav.tipoPropiedad.Add(respuestaObtenerPublicacion.publicacion.Propiedad.TipoPropiedad.ElementAt(f).nombreTipoPropiedad);
                        f++;
                    }
                    fav.tipoPublicacion = respuestaObtenerPublicacion.publicacion.TipoPublicacion.nombreTipoPublicacion;
                    fav.precioPropiedad = String.Format("{0:c}", respuestaObtenerPublicacion.publicacion.Propiedad.precioPropiedad);
                    fav.tipoMoneda = respuestaObtenerPublicacion.publicacion.Propiedad.TipoMoneda.denominacionMoneda;
                    favoritos.Add(fav);
                }
                
            }
            return (error, favoritos);
        }
        public static ErrorPropy QuitarFavorito(string publicacionId,string usuarioId) {
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            ErrorPropy error = new ErrorPropy();
            (ErrorPropy error, TransferenciaUsuario usuario) respuestaObtenerUsuario = ExpertoUsuarios.ObtenerUsuarioPorID(usuarioId,clienteHttp);
            if (respuestaObtenerUsuario.error.codigoError!=0) {
                return respuestaObtenerUsuario.error;
            }
            foreach (var favorito in respuestaObtenerUsuario.usuario.Favorito) {
                if (favorito.publicacionId==publicacionId) {
                    TransferenciaFavorito fav = favorito;
                    fav.activo = false;
                    var tareaEliminarFavorito = clienteHttp.PostAsJsonAsync<TransferenciaFavorito>("api/Favorito/eliminarFavorito", fav);
                    tareaEliminarFavorito.Wait();
                    if (!tareaEliminarFavorito.Result.IsSuccessStatusCode) {
                        throw new Exception(tareaEliminarFavorito.Result.StatusCode.ToString());
                    }
                }
            }
            return error;
             
        }
        public static ErrorPropy AgregarFavorito(string usuarioId,string publicacionId) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            
            (ErrorPropy error, TransferenciaUsuario usuario) respuestaObtenerUsuario = ExpertoUsuarios.ObtenerUsuarioPorID(usuarioId,clienteHttp);
            if (respuestaObtenerUsuario.error.codigoError!=0) {
                return respuestaObtenerUsuario.error;
            }
            foreach (var fav in respuestaObtenerUsuario.usuario.Favorito) {
                if (fav.activo==true && fav.publicacionId==publicacionId) {
                    error.codigoError = -1;
                    error.descripcionError = "Usted ya agregó esta publicación a su lista de favoritos";
                    return error;
                }
            }
            TransferenciaFavorito favorito = new TransferenciaFavorito();
            favorito.favoritoId = Guid.NewGuid().ToString();
            favorito.activo = true;
            favorito.publicacionId = publicacionId;
            favorito.usuarioId = usuarioId;
            var tareaCrearFavorito = clienteHttp.PostAsJsonAsync<TransferenciaFavorito>("api/Favorito/crearFavorito", favorito);
            tareaCrearFavorito.Wait();
            if (!tareaCrearFavorito.Result.IsSuccessStatusCode) {
                throw new Exception(tareaCrearFavorito.Result.StatusCode.ToString());
            }
            return error;
        }
        public static (ErrorPropy,TransferenciaPlanUsuario) ObtenerPlanActivoUsuario(string usuarioId,HttpClient clienteHttp) {
            
            ErrorPropy error = new ErrorPropy();
            (ErrorPropy error, TransferenciaUsuario usuario) respuestaObtenerUsuario = ObtenerUsuarioPorID(usuarioId, clienteHttp);
            if (respuestaObtenerUsuario.error.codigoError != 0)
            {
                error = respuestaObtenerUsuario.error;
                return (error,null);
            }
            TransferenciaPlanUsuario planUsuario = respuestaObtenerUsuario.usuario.PlanUsuario.Where(x => x.activo == true).FirstOrDefault();
            if (planUsuario == null) { error.codigoError = -5; error.descripcionError = "El usuario no ha contratado plan."; return (error,null); }
            return (error,planUsuario);
        }
        public static void RegistrarActividad(string usuarioId,string descripcionActividad) {
            
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            
            TransferenciaActividad actividad = new TransferenciaActividad();
            actividad.actividadId = Guid.NewGuid().ToString();
            actividad.descripcionActividad = descripcionActividad;
            actividad.fechaActividad = DateTime.UtcNow;
            actividad.usuarioId = usuarioId;
            var tareaRegistrarActividad = clienteHttp.PostAsJsonAsync<TransferenciaActividad>("api/Usuario/registrarActividad",actividad);
            tareaRegistrarActividad.Wait();
            if (!tareaRegistrarActividad.Result.IsSuccessStatusCode) {
                throw new Exception("Error: "+(int)tareaRegistrarActividad.Result.StatusCode+" "+tareaRegistrarActividad.Result.StatusCode);
            }
            
        }
        public static async Task<ErrorPropy> ReenvioMailVerificacion(string email,string rutaConfirmarCuenta) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            (ErrorPropy error, TransferenciaUsuario usuario) respuesta = ObtenerUsuario(email, clienteHttp);
            if (respuesta.error.codigoError != 0)
            {
                error = respuesta.error;
                return error;
            }
            if (respuesta.usuario == null)
            {
                error.codigoError = -1;
                error.descripcionError = "No se ha encontrado un usuario con el email ingresado";
                return error;
            }
            await ExpertoMensajeria.ReenvioMailVerificacion(email, rutaConfirmarCuenta);
            return error;
        }
        public static async Task<ErrorPropy> RecuperarContraseñaPaso1(string email,string rutaRecuperarClave) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            (ErrorPropy error, TransferenciaUsuario usuario) respuesta = ObtenerUsuario(email, clienteHttp);
            if (respuesta.error.codigoError!=0) {
                error = respuesta.error;
                return error;
            }
            if (respuesta.usuario==null) {
                error.codigoError = -1;
                error.descripcionError = "No se ha encontrado un usuario con el email ingresado";
                return error;
            }
            await ExpertoMensajeria.EnviarMailRecuperarClave(email,rutaRecuperarClave);
            return error;
        }
        public static ErrorPropy RecuperarContraseña(string email,string clave,string claveRepetida) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            error = ValidacionParametros.ValidacionRecuperarContraseñaPaso2(email,clave,claveRepetida);
            if (error.codigoError!=0) {
                return error;
            }

            (ErrorPropy error,TransferenciaUsuario usuario) respuesta = ObtenerUsuario(email,clienteHttp);
            if (respuesta.error.codigoError!=0) {
                error.codigoError = respuesta.error.codigoError;
                error.descripcionError = respuesta.error.descripcionError;
                return error;
            }
            if (respuesta.usuario==null) {
                error.codigoError = -1;
                error.descripcionError = "No se ha podido encontrar el email ingresado. Por favor, regístrese en propy";
                return error;
            }
            TransferenciaUsuario usuario = respuesta.usuario;

            DTOCryptography crypto = new DTOCryptography();
            crypto.OriginalText = clave;
            Criptografia.Encrypt(crypto);

            usuario.contraseña = crypto.OriginalTextEncrypted;
            usuario.Key = crypto.Key;
            usuario.Vector = crypto.Vector;

            var tareaRecuperarClave = clienteHttp.PostAsJsonAsync<TransferenciaUsuario>("api/Usuario/editarUsuario", usuario);
            tareaRecuperarClave.Wait();
            if (!tareaRecuperarClave.Result.IsSuccessStatusCode) {
                error.codigoError = (int)tareaRecuperarClave.Result.StatusCode;
                error.descripcionError = "Error: " + error.codigoError + " " + tareaRecuperarClave.Result.StatusCode;
                return error;
            }
            RegistrarActividad(usuario.usuarioId, "Modificó su clave");
            return error;
        }
        public static (ErrorPropy,TransferenciaUsuario) Login(string email,string clave) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            (ErrorPropy error, TransferenciaUsuario usuario) respuesta = ObtenerUsuario(email,clienteHttp);
            error = respuesta.error;
            if (respuesta.error.codigoError!=0) {
                
                return (error,respuesta.usuario);   
            }
            if (respuesta.usuario==null) {
                error.codigoError = -1;
                error.descripcionError = "No se encuentra registrado un usuario con las credenciales ingresadas.";
                return (error,respuesta.usuario);
            }
            if (respuesta.usuario.emailConfirmado==false) {
                
                error.codigoError = -2;
                error.descripcionError = "Debe verificar su email a través de su correo electrónico para poder continuar.";
                return (error,respuesta.usuario);
            }
            DTOCryptography crypto = new DTOCryptography();
            crypto.Key = respuesta.usuario.Key;
            crypto.Vector = respuesta.usuario.Vector;
            crypto.OriginalTextEncrypted = respuesta.usuario.contraseña;
            Criptografia.Decrypt(crypto);
            if (clave!=crypto.OriginalText) {
                error.codigoError = -1;
                error.descripcionError = "Ha ingresado una clave incorrecta";
                return (error,respuesta.usuario);
            }
            
            return (error,respuesta.usuario);
        }
        public static (ErrorPropy, TransferenciaUsuario) LoginGoogle(string code)
        {
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            ErrorPropy error = new ErrorPropy();
            var tareaObtenerUsuario = clienteHttp.GetAsync("api/Login/callback/" + code);
            tareaObtenerUsuario.Wait();
            var action = tareaObtenerUsuario.Result.Content.ReadAsAsync<TransferenciaUsuario>().Result;
            if (!tareaObtenerUsuario.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerUsuario.Result.StatusCode.ToString());
            }
            if (action.Equals(null))
            {
                error.codigoError = -1;
                error.descripcionError = "No se a podido ingresar intente de nuevo";
            };

            return (error, action);

        }
        public static ErrorPropy ConfirmarCuenta(string email) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            (ErrorPropy error,TransferenciaUsuario usuario) respuesta = ObtenerUsuario(email,clienteHttp);
            if (respuesta.usuario==null) {
                error.codigoError = (int)HttpStatusCode.NotFound;
                error.descripcionError = "No se pudo encontrar el usuario con el email ingresado.";
                return error;
            }
            respuesta.usuario.emailConfirmado = true;
            var tareaPostConfirmarCuenta = clienteHttp.PostAsJsonAsync<TransferenciaUsuario>("api/Usuario/editarUsuario", respuesta.usuario);
            tareaPostConfirmarCuenta.Wait();
            if (!tareaPostConfirmarCuenta.Result.IsSuccessStatusCode) {
                error.codigoError = (int)tareaPostConfirmarCuenta.Result.StatusCode;
                error.descripcionError ="Error: "+error.codigoError+" "+ tareaPostConfirmarCuenta.Result.StatusCode;
                return error;
            }
            RegistrarActividad(respuesta.usuario.usuarioId,"Confirmó su cuenta");
            return error;
        }
        public static (ErrorPropy,TransferenciaUsuario) ObtenerUsuario(string email,HttpClient clienteHttp) {
            ErrorPropy error = new ErrorPropy();
            var tareaObtenerUsuario = clienteHttp.GetAsync("api/Usuario/obtenerUsuarioPorEmail/" + email);
            tareaObtenerUsuario.Wait();
            TransferenciaUsuario usuario = tareaObtenerUsuario.Result.Content.ReadAsAsync<TransferenciaUsuario>().Result;
            if (!tareaObtenerUsuario.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerUsuario.Result.StatusCode.ToString());
            }
            
            return (error,usuario);
        }
        public static (ErrorPropy, TransferenciaUsuario) ObtenerUsuarioPorID(string ID, HttpClient clienteHttp)
        {
            ErrorPropy error = new ErrorPropy();
            var tareaObtenerUsuario = clienteHttp.GetAsync("api/Usuario/obtenerUsuarioPorID/" + ID);
            tareaObtenerUsuario.Wait();
            TransferenciaUsuario usuario = tareaObtenerUsuario.Result.Content.ReadAsAsync<TransferenciaUsuario>().Result;
            if (!tareaObtenerUsuario.Result.IsSuccessStatusCode)
            {
                throw new Exception(tareaObtenerUsuario.Result.StatusCode.ToString());
            }
            return (error, usuario);
        }
        public static async Task<ErrorPropy> CrearCuenta(string nombreUsuario, string apellidoUsuario, string telefono1, string telefono2, string email, string clave, bool permitirSerContactadoPorPublicante, bool permitirSerNotificado,string rutaConfirmarCuenta) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();

            long tel2 = -1;
            if (!String.IsNullOrEmpty(telefono2)) { tel2 = Convert.ToInt64(telefono2); }

            DTOCryptography cripto = new DTOCryptography();
            cripto.OriginalText = clave;
            Criptografia.Encrypt(cripto);
            


            #region Validación de existencia de usuario
            (ErrorPropy error, TransferenciaUsuario usuario) respuestaObtenerUsuario = ObtenerUsuario(email, clienteHttp);

            if (respuestaObtenerUsuario.error.codigoError != 0)
            {
                error.codigoError = respuestaObtenerUsuario.error.codigoError;
                error.descripcionError = respuestaObtenerUsuario.error.descripcionError;
                return error;
            }
            if (respuestaObtenerUsuario.usuario != null)
            {
                error.codigoError = -1;
                error.descripcionError = "Ya existe un usuario con el email ingresado";
                return error;
            }
            #endregion

            

            TransferenciaUsuario usuario = new TransferenciaUsuario();
            usuario.nombreUsuario = nombreUsuario;
            usuario.apellidoUsuario = apellidoUsuario;
            usuario.telefono1 = Convert.ToInt64(telefono1);
            usuario.telefono2 = tel2;
            usuario.email = email;
            usuario.contraseña = cripto.OriginalTextEncrypted;
            usuario.Key = cripto.Key;
            usuario.Vector = cripto.Vector;
            usuario.permiterSerContactadoPorPublicante = permitirSerContactadoPorPublicante;
            usuario.permiteSerNotificado = permitirSerNotificado;
            usuario.usuarioId = Guid.NewGuid().ToString();

            var tareaPostUsuario = clienteHttp.PostAsJsonAsync<TransferenciaUsuario>("api/Usuario/crearCuenta",usuario);
            tareaPostUsuario.Wait();
           
            if (!tareaPostUsuario.Result.IsSuccessStatusCode) {
                throw new Exception(tareaPostUsuario.Result.StatusCode.ToString());
            }

            await ExpertoMensajeria.EnviarEmailBienvenida(nombreUsuario,email,rutaConfirmarCuenta);
            RegistrarActividad(usuario.usuarioId,"Creó su cuenta.");
            return error;
        }
        
        public static (ErrorPropy,List<DTOConsultaActividadUsuario>) ConsultarActividad(string usuarioId) {
            ErrorPropy error = new ErrorPropy();
            HttpClient clienteHttp = ApiConfiguracion.Inicializar();
            (ErrorPropy error, TransferenciaUsuario usuario) respuestaObtenerUsuario = ObtenerUsuarioPorID(usuarioId,clienteHttp);
            if (respuestaObtenerUsuario.usuario==null) {
                throw new Exception("Error -5. No se pudieron recuperar los datos solicitados");
            }
            List<DTOConsultaActividadUsuario> listadoActividades = new List<DTOConsultaActividadUsuario>();
            foreach (var actividad in respuestaObtenerUsuario.usuario.Actividad) {
                DTOConsultaActividadUsuario dtoConsultaActividadUsuario = new DTOConsultaActividadUsuario();
                dtoConsultaActividadUsuario.descripcionActividad = actividad.descripcionActividad;
                dtoConsultaActividadUsuario.fechaActividad = actividad.fechaActividad.ToShortDateString() + "-" + actividad.fechaActividad.ToShortTimeString();
                listadoActividades.Add(dtoConsultaActividadUsuario);
            }
            return (error,listadoActividades);
        }
    }
}
