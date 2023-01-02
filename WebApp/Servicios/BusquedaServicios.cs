using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Servicios;
using WebApp.Models;
using WebApp.DTO;
using System.Globalization;

namespace WebApp.Servicios
{
    public class BusquedaServicios
    {
        DTOTodosResultadosBusqueda dtoTodosResultadosBusqueda = new DTOTodosResultadosBusqueda();
        DTOFiltrosBusqueda dtoFiltroBusqueda = new DTOFiltrosBusqueda();

        TipoPropiedadServicios serviciosTipoPropiedad = new TipoPropiedadServicios();
        TipoPublicacionServicios serviciosTipoPublicacion = new TipoPublicacionServicios();
        
        TipoConstruccionServicios serviciosTipoConstruccion = new TipoConstruccionServicios();
        ExtrasServicios serviciosExtras = new ExtrasServicios();



        PublicacionServicios serviciosPublicacion = new PublicacionServicios();

        List<TipoPropiedad> listadoTipoPropiedades;
        List<TipoPublicacion> listadoTipoOperaciones;
        
        List<TipoConstruccion> listadoTipoConstrucciones;
        List<Extras> listadoExtras;
        List<Publicacion> listadoPublicacion;

        public void CargarDatosFiltroTipoPropiedad() {
            listadoTipoPropiedades = serviciosTipoPropiedad.ListarTipoPropiedades();
            foreach (var item in listadoTipoPropiedades) {
                DTOTipoPropiedad dtoTipoPropiedad = new DTOTipoPropiedad();
                dtoTipoPropiedad.tipoPropiedadId = item.tipoPropiedadId;
                dtoTipoPropiedad.nombreTipoPropiedad = item.nombreTipoPropiedad;
                dtoFiltroBusqueda.AgregarDTOTipoPropiedad(dtoTipoPropiedad);

            }
        }
        public void CargarDatosFiltroTipoOperacion() {
            listadoTipoOperaciones = serviciosTipoPublicacion.ListarTipoPublicaciones();
            foreach (var item in listadoTipoOperaciones) {
                DTOTipoOperacion dtoTipoOperacion = new DTOTipoOperacion();
                dtoTipoOperacion.tipoOperacionId = item.tipoPublicacionId;
                dtoTipoOperacion.nombreTipoOperacion = item.nombreTipoPublicacion;
                dtoFiltroBusqueda.AgregarDTOTipoOperacion(dtoTipoOperacion);
            }
        }
        public void CargarDatosFiltroPais() {
            
        }
        public void CargarDatosFiltroProvincia() {
            
        }
        public void CargarDatosFiltroCiudad() {
            
        }
        public void CargarDatosFiltroTipoConstruccion() {
            listadoTipoConstrucciones = serviciosTipoConstruccion.ListarTipoConstrucciones();
            foreach (var item in listadoTipoConstrucciones) {
                DTOTipoConstruccion dtoTipoConstruccion = new DTOTipoConstruccion();
                dtoTipoConstruccion.tipoConstruccionId = item.tipoConstuccionId;
                dtoTipoConstruccion.nombreTipoConstruccion = item.nombreTipoConstruccion;
                dtoFiltroBusqueda.AgregarDTOTipoConstruccion(dtoTipoConstruccion);
            }
        }
        public void CargarDatosFiltroExtras() {
            listadoExtras = serviciosExtras.ListarExtras();
            foreach (var item in listadoExtras) {
                DTOExtras dtoExtra = new DTOExtras();
                dtoExtra.extraId = item.extraId;
                dtoExtra.nombreExtra = item.nombreExtra;
                dtoFiltroBusqueda.AgregarDTOExtra(dtoExtra);

            }
        }

        public DTOTodosResultadosBusqueda CargarResultadoBusquedaGenerica() {
            

            using (var db = new ApplicationDbContext()) {
                var data = (from p in db.Publicacion
                            where p.fechaFinPublicacion >= DateTime.Now
                            select p).ToList();

                foreach (var item in data)
                {
                    DTOResultadoBusquedaPublicacion dtoRes = new DTOResultadoBusquedaPublicacion();
                    dtoRes.propiedadId = item.propiedadId.ToString();
                    
                    dtoRes.direccionPropiedad = item.Propiedad.calle;
                    
                    dtoRes.publicacionId = item.publicacionId;
                    dtoRes.fechaInicioPublicacion = item.fechaInicioPublicacion;
                    dtoRes.tipoOperacion = item.TipoPublicacion.nombreTipoPublicacion;
                    dtoRes.tipoPropiedad = item.Propiedad.TipoPropiedad.nombreTipoPropiedad;


                    dtoTodosResultadosBusqueda.AgregarDTOUnResultadoPublicacion(dtoRes);
                }
            }
            return dtoTodosResultadosBusqueda;

                


                
            
        }

        public DTOFiltrosBusqueda enviarPaqueteDatosFiltroBusqueda() {
            return dtoFiltroBusqueda;
        }

        public DTOTodosResultadosBusqueda CargarResultadoBusqueda(bool tenerCuentaPais,bool tenerCuentaProvincia,bool tenerCuentaCiudad,bool genericaEspecifica,string pais,string provincia,string ciudad,string antiguedad) {
            using (var db = new ApplicationDbContext()) {
                List<Propiedad> dataPropiedad=null;
                //Aseguro que si no se quiere filtrar por pais no se tenga en cuenta el filtrado por provincia
                if (tenerCuentaPais==false) {
                    tenerCuentaProvincia = false;
                }
                //Aseguro que si no se quiere filtrar por provincia no se tenga en cuenta el filtrado por ciudad
                if (tenerCuentaProvincia==false) {
                    tenerCuentaCiudad = false;
                }
                //Si es falso significa que es uan busqueda especifica
                if (genericaEspecifica == false)
                {
                    int añosAntiguedadDesde;
                    int añosAntiguedadHasta;

                    if (antiguedad.Length > 3)
                    {
                        añosAntiguedadDesde = Convert.ToInt32(antiguedad.Substring(0, 2));
                        añosAntiguedadHasta = Convert.ToInt32(antiguedad.Substring(2, 3));

                    }
                    else
                    {
                        añosAntiguedadDesde = Convert.ToInt32(antiguedad);
                        añosAntiguedadHasta = Convert.ToInt32(antiguedad);
                    }

                    if (tenerCuentaPais==false) {
                        dataPropiedad = (from propiedad in db.Propiedad
                                         select propiedad).ToList();
                    }
                    if (tenerCuentaPais == true) {
                        
                    }
                    

                    foreach (var item in dataPropiedad)
                    {
                        DTOResultadoBusquedaPublicacion dto = new DTOResultadoBusquedaPublicacion();
                        dto.tipoPropiedad = item.TipoPropiedad.nombreTipoPropiedad;
                        dto.nroPlantas = item.nroPlantas;
                        
                        dto.direccionPropiedad = item.calle;
                        

                        var dataPublicacion = (from publicacion in db.Publicacion
                                               where publicacion.propiedadId == item.propiedadId
                                               select publicacion).ToList();
                        foreach (var itemPublicacion in dataPublicacion)
                        {
                            dto.precioPropiedad = itemPublicacion.precioPropiedad.ToString("N", CultureInfo.CreateSpecificCulture("es-AR")); ;
                            dto.publicacionId = itemPublicacion.publicacionId;
                            dto.fechaInicioPublicacion = itemPublicacion.fechaInicioPublicacion;
                            dto.tipoOperacion = itemPublicacion.TipoPublicacion.nombreTipoPublicacion;
                        }
                        dtoTodosResultadosBusqueda.AgregarDTOUnResultadoPublicacion(dto);
                    }

                }
            }
                


            return dtoTodosResultadosBusqueda; 
        }


        public DTOTodosResultadosBusqueda CargarResultadosBusquedaEspecifica(string condicionAntiguedad) {
            using (var db = new ApplicationDbContext()) {
                int añosAntiguedadDesde;
                int añosAntiguedadHasta;
                
                if (condicionAntiguedad.Length > 3) {
                    añosAntiguedadDesde = Convert.ToInt32(condicionAntiguedad.Substring(0, 2));
                    añosAntiguedadHasta = Convert.ToInt32(condicionAntiguedad.Substring(2, 3));
                    
                }
                else {
                    añosAntiguedadDesde = Convert.ToInt32(condicionAntiguedad);
                    añosAntiguedadHasta= Convert.ToInt32(condicionAntiguedad);
                }
                var data = (from propiedad in db.Propiedad
                            where propiedad.antiguedad >= añosAntiguedadDesde && propiedad.antiguedad<=añosAntiguedadHasta
                            select propiedad).ToList();

                foreach (var item in data) {
                    DTOResultadoBusquedaPublicacion dto = new DTOResultadoBusquedaPublicacion();
                    dto.tipoPropiedad = item.TipoPropiedad.nombreTipoPropiedad;
                    
                    dto.nroPlantas = item.nroPlantas;
                    
                    dto.direccionPropiedad = item.calle;
                    

                    var dataPublicacion = (from publicacion in db.Publicacion
                                           where publicacion.propiedadId == item.propiedadId
                                           select publicacion).ToList();
                    foreach (var itemPublicacion in dataPublicacion) {
                        dto.publicacionId = itemPublicacion.publicacionId;
                        dto.fechaInicioPublicacion = itemPublicacion.fechaInicioPublicacion;
                        dto.tipoOperacion = itemPublicacion.TipoPublicacion.nombreTipoPublicacion;
                        dto.precioPropiedad = itemPublicacion.precioPropiedad.ToString("N", CultureInfo.CreateSpecificCulture("es-AR")); ;
                    }
                    dtoTodosResultadosBusqueda.AgregarDTOUnResultadoPublicacion(dto);
                }
            }

            return dtoTodosResultadosBusqueda;
            
        }

        //A partir de aca la lógica sirve
        public void ArmarPaqueteDatosResultadoBusqueda(List<Publicacion> publicaciones) {

        }

        
    }
}