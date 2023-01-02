using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DTO;
using WebApp.Fachada;

namespace WebApp.Servicios
{
    public static class ServiciosUsuario
    {
        public static DTOPublicante SolicitarDatosPublicante(Guid publicacionId) {
            using (var db = new ApplicationDbContext()) {
                var data = (from p in db.Publicacion
                           where p.publicacionId == publicacionId
                           select p).FirstOrDefault();
                DTOPublicante dtoPublicante = new DTOPublicante();
                dtoPublicante.nombrePublicante = data.ApplicationUser.nombre;
                dtoPublicante.apellidoPublicante = data.ApplicationUser.apellido;
                dtoPublicante.email = data.ApplicationUser.Email;
                dtoPublicante.telefono = data.ApplicationUser.nroTelefono;
                return dtoPublicante;
            }
        }
        public static bool VerificarCUIL(string cuil)
        {
            int[] factores = new int[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int acumulado = 0;


            int digitoVerificador = 0;
            if (String.IsNullOrEmpty(cuil) || cuil.Length != 11)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < factores.Length; i++)
                {
                    acumulado += int.Parse(cuil[i].ToString()) * factores[i];
                }
                digitoVerificador = 11 - (acumulado % 11);
            }
            if (digitoVerificador == 11)
            {
                digitoVerificador = 0;
            }
            if (int.Parse(cuil[10].ToString()) != digitoVerificador)
            {
                return false;
            }

            return true;
        }
        public static void RegistrarActividad(string actividad,string idUsuario,ApplicationDbContext db) {
            Repository<ActividadUsuario> actividadUsuarioRepo = new Repository<ActividadUsuario>();
            ActividadUsuario actividadUsuario = new ActividadUsuario();
            actividadUsuario.IDActividadUsuario = Guid.NewGuid();
            actividadUsuario.UserId = idUsuario;
            actividadUsuario.fechaActividad = DateTime.UtcNow;
            actividadUsuario.descripcionActividad = actividad;
            actividadUsuarioRepo.Crear(actividadUsuario,db);

            actividadUsuarioRepo.Guardar(db);
        }
        public static List<DTOActividad> ConsultarActividades(string IDUsuario) {
            List<DTOActividad> listadoActividadesMostrar = new List<DTOActividad>();
            using (var db = new ApplicationDbContext()) {
                try
                {
                    Repository<ActividadUsuario> actividadUsuarioRepo = new Repository<ActividadUsuario>();
                    List<ActividadUsuario> listadoActividades = actividadUsuarioRepo.Buscar(x=>x.UserId==IDUsuario,db);
                    foreach (var item in listadoActividades) {
                        DTOActividad dtoActividad = new DTOActividad();
                        dtoActividad.descripcionActividad = item.descripcionActividad;
                        TimeZoneInfo zonaHorariaArgentina = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
                        dtoActividad.fechaActividad = TimeZoneInfo.ConvertTimeFromUtc(item.fechaActividad, zonaHorariaArgentina);
                        listadoActividadesMostrar.Add(dtoActividad);
                    }
                    List<DTOActividad> listadoActividadesOrdenado = listadoActividadesMostrar.OrderBy(x => x.fechaActividad).Reverse().ToList();
                    return listadoActividadesOrdenado;
                }
                catch (Exception)
                {

                    throw;
                }
            }
                
            
        }
    }
}