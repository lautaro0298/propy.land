using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;
using WebApp.DTO;
using WebApp.Servicios;
using System.Web.Mvc;
using WebApp.CodigosBusqueda;
using System.Globalization;
using WebApp.Fachada;


namespace WebApp.Experto
{
    public class ExpertoRealizarBusqueda
    {
        public DTOFiltrosBusqueda PrepararFiltrosParaBusqueda()
        {
            DTOFiltrosBusqueda dtoFiltrosBusqueda = new DTOFiltrosBusqueda();
            using (var db = new ApplicationDbContext())
            {
                var tipoPropiedadRepo = new Repository<TipoPropiedad>();
                List<TipoPropiedad> dataFilterTipoPropiedad = tipoPropiedadRepo.Buscar(c => c.activo == true, db).OrderBy(x => x.nombreTipoPropiedad).ToList();
                /*var datosFiltroTipoPropiedad = (from tp in db.TipoPropiedad
                                            orderby tp.nombreTipoPropiedad ascending
                                            where tp.activo == true
                                            select tp).ToList();*/
                var tipoOperacionRepo = new Repository<TipoPublicacion>();
                List<TipoPublicacion> dataFilterTipoPublicacion = tipoOperacionRepo.Buscar(x => x.activo == true, db).OrderBy(x => x.nombreTipoPublicacion).ToList();

                /*var datosFiltroTipoOperacion = (from to in db.TipoPublicacion
                                            orderby to.nombreTipoPublicacion ascending
                                            where to.activo==true
                                            select to).ToList();*/
                var tipoConstruccionRepo = new Repository<TipoConstruccion>();
                List<TipoConstruccion> dataFilterTipoConstruccion = tipoConstruccionRepo.Buscar(x => x.activo == true, db).OrderBy(x => x.nombreTipoConstruccion).ToList();
                /*var datosFiltroTipoConstruccion = (from tc in db.TipoConstruccion
                                                   orderby tc.nombreTipoConstruccion ascending
                                                   where tc.activo==true
                                                   select tc);*/
                var extrasRepo = new Repository<Extras>();
                List<Extras> dataFilterExtras = extrasRepo.Buscar(x => x.activo == true, db).OrderBy(x => x.nombreExtra).ToList();

                /*var datosFiltroExtras = (from e in db.Extras
                                         orderby e.nombreExtra ascending
                                         where e.activo==true
                                         select e).ToList();*/

                var tipoMonedaRepo = new Repository<TipoMoneda>();
                List<TipoMoneda> dataFilterTipoMoneda = tipoMonedaRepo.Buscar(x => x.activo == true, db).OrderBy(x => x.nombreTipoMoneda).ToList();
                /*var datosFiltroTipoMoneda = (from tm in db.TipoMoneda
                                             where tm.activo == true
                                             select tm).ToList();*/
                foreach (var tipoMoneda in dataFilterTipoMoneda)
                {
                    DTOTipoMoneda dtoTipoMoneda = new DTOTipoMoneda();
                    dtoTipoMoneda.tipoMonedaId = tipoMoneda.tipoMonedaId;
                    dtoTipoMoneda.nombreTipoMoneda = tipoMoneda.nombreTipoMoneda;
                    dtoFiltrosBusqueda.AgregarDTOTipoMoneda(dtoTipoMoneda);
                }
                foreach (var tipoPropiedad in dataFilterTipoPropiedad)
                {
                    DTOTipoPropiedad dtoTipoPropiedad = new DTOTipoPropiedad();
                    dtoTipoPropiedad.tipoPropiedadId = tipoPropiedad.tipoPropiedadId;
                    dtoTipoPropiedad.nombreTipoPropiedad = tipoPropiedad.nombreTipoPropiedad;
                    dtoFiltrosBusqueda.AgregarDTOTipoPropiedad(dtoTipoPropiedad);
                }
                foreach (var tipoOperacion in dataFilterTipoPublicacion)
                {
                    DTOTipoOperacion dtoTipoOperacion = new DTOTipoOperacion();
                    dtoTipoOperacion.tipoOperacionId = tipoOperacion.tipoPublicacionId;
                    dtoTipoOperacion.nombreTipoOperacion = tipoOperacion.nombreTipoPublicacion;
                    dtoFiltrosBusqueda.AgregarDTOTipoOperacion(dtoTipoOperacion);
                }

                foreach (var tipoConstruccion in dataFilterTipoConstruccion)
                {
                    DTOTipoConstruccion dtoTipoConstruccion = new DTOTipoConstruccion();
                    dtoTipoConstruccion.tipoConstruccionId = tipoConstruccion.tipoConstuccionId;
                    dtoTipoConstruccion.nombreTipoConstruccion = tipoConstruccion.nombreTipoConstruccion;
                    dtoFiltrosBusqueda.AgregarDTOTipoConstruccion(dtoTipoConstruccion);
                }
                foreach (var extra in dataFilterExtras)
                {
                    DTOExtras dtoExtras = new DTOExtras();
                    dtoExtras.extraId = extra.extraId;
                    dtoExtras.nombreExtra = extra.nombreExtra;
                    dtoFiltrosBusqueda.AgregarDTOExtra(dtoExtras);
                }

                return dtoFiltrosBusqueda;
            }

        }
        public DTOTodosResultadosBusqueda RealizarBusqueda(DTOBusqueda paqueteDatos, string tipoMonedaSeleccionada)
        {
            //LA PRINCIPAL BUSQUEDA SE HACE EN BASE A LA SELECCION DE PAIS, PROVINCIA Y CIUDAD. LUEGO SE EVALUAN LOS DEMÁS PARÁMETROS
            int codCombinacion = -1;

            List<Publicacion> publicacionesFiltradasPaso1 = new List<Publicacion>();
            try
            {
                #region Validaciones paso 1

                string finCadenaAreaAdmNivel1 = "";
                string finCadenaAreaAdmNivel2 = "";
                //AGREGADO ANTES NO ESTABA
                if (paqueteDatos.pais != "-1" && paqueteDatos.areaAdministrativaNivel1 == "-1" && paqueteDatos.areaAdministrativaNivel2 == "-1" && paqueteDatos.radioElegido == -1)
                {
                    codCombinacion = GuiaBusqueda.eligioPais;
                }
                if (paqueteDatos.areaAdministrativaNivel1 != "-1" && paqueteDatos.areaAdministrativaNivel2 == "-1" && paqueteDatos.pais == "-1" && paqueteDatos.radioElegido == -1)
                {
                    codCombinacion = GuiaBusqueda.eligioProvincia;
                }
                if (paqueteDatos.areaAdministrativaNivel1 == "-1" && paqueteDatos.pais == "-1" && paqueteDatos.areaAdministrativaNivel2 != "-1" && paqueteDatos.radioElegido == -1)
                {
                    codCombinacion = GuiaBusqueda.eligioCiudad;
                }
                if (paqueteDatos.pais != "-1" && paqueteDatos.areaAdministrativaNivel1 != "-1" && paqueteDatos.areaAdministrativaNivel2 == "-1" && paqueteDatos.radioElegido == -1)
                {
                    codCombinacion = GuiaBusqueda.eligioPaisProvincia;
                }
                if (paqueteDatos.pais != "-1" && paqueteDatos.areaAdministrativaNivel1 == "-1" && paqueteDatos.areaAdministrativaNivel2 != "-1" && paqueteDatos.radioElegido == -1)
                {
                    codCombinacion = GuiaBusqueda.eligioPaisCiudad;
                }
                if (paqueteDatos.pais == "-1" && paqueteDatos.areaAdministrativaNivel1 != "-1" && paqueteDatos.areaAdministrativaNivel2 != "-1" && paqueteDatos.radioElegido == -1)
                {
                    codCombinacion = GuiaBusqueda.eligioProvinciaCiudad;
                }
                if (paqueteDatos.pais != "-1" && paqueteDatos.areaAdministrativaNivel1 != "-1" && paqueteDatos.areaAdministrativaNivel2 != "-1" && paqueteDatos.radioElegido == -1)
                {
                    codCombinacion = GuiaBusqueda.eligioPaisProvinciaCiudad;
                }
                if (paqueteDatos.pais == "-1" && paqueteDatos.areaAdministrativaNivel1 == "-1" && paqueteDatos.areaAdministrativaNivel2 == "-1" && paqueteDatos.radioElegido==-1)
                {
                    codCombinacion = GuiaBusqueda.noEligioUbicacion;
                }
                if (paqueteDatos.radioElegido!=-1) {
                    codCombinacion = GuiaBusqueda.eligioRadioEspecifico;
                }
                using (var db = new ApplicationDbContext())
                {
                    switch (paqueteDatos.pais)
                    {
                        case "Argentina":
                            if (paqueteDatos.areaAdministrativaNivel1.Contains("Province"))
                            {
                                finCadenaAreaAdmNivel1 = " Province";
                            }
                            if (paqueteDatos.areaAdministrativaNivel2.Contains("Department"))
                            {
                                finCadenaAreaAdmNivel2 = " Department";
                            }
                            break;
                    }
                    switch (codCombinacion)
                    {
                        case 0:
                            publicacionesFiltradasPaso1 = (from pub in db.Publicacion
                                                           select pub).ToList();
                            break;
                        case 1:
                            publicacionesFiltradasPaso1 = (from pub in db.Publicacion
                                                           where pub.Propiedad.pais == paqueteDatos.pais
                                                           select pub).ToList();
                            break;
                        case 2:
                            publicacionesFiltradasPaso1 = (from pub in db.Publicacion
                                                           where pub.Propiedad.pais == paqueteDatos.pais && pub.Propiedad.areaAdministrativaNivel1 + finCadenaAreaAdmNivel1 == paqueteDatos.areaAdministrativaNivel1
                                                           select pub).ToList();
                            break;
                        case 3:
                            publicacionesFiltradasPaso1 = (from pub in db.Publicacion
                                                           where pub.Propiedad.pais == paqueteDatos.pais && pub.Propiedad.areaAdministrativaNivel1 + finCadenaAreaAdmNivel1 == paqueteDatos.areaAdministrativaNivel1 && pub.Propiedad.areaAdministrativaNivel2 + finCadenaAreaAdmNivel2 == paqueteDatos.areaAdministrativaNivel2
                                                           select pub).ToList();
                            break;
                        case 4:
                            publicacionesFiltradasPaso1 = (from pub in db.Publicacion
                                                           where pub.Propiedad.areaAdministrativaNivel1 + finCadenaAreaAdmNivel1 == paqueteDatos.areaAdministrativaNivel1
                                                           select pub).ToList();
                            break;
                        case 5:
                            publicacionesFiltradasPaso1 = (from pub in db.Publicacion
                                                           where pub.Propiedad.areaAdministrativaNivel1 + finCadenaAreaAdmNivel1 == paqueteDatos.areaAdministrativaNivel1 && pub.Propiedad.areaAdministrativaNivel2 + finCadenaAreaAdmNivel2 == paqueteDatos.areaAdministrativaNivel2
                                                           select pub).ToList();
                            break;
                        case 6:
                            publicacionesFiltradasPaso1 = (from pub in db.Publicacion
                                                           where pub.Propiedad.areaAdministrativaNivel2 + finCadenaAreaAdmNivel2 == paqueteDatos.areaAdministrativaNivel2
                                                           select pub).ToList();
                            break;
                        case 7:
                            publicacionesFiltradasPaso1 = (from pub in db.Publicacion
                                                           where pub.Propiedad.pais == paqueteDatos.pais && pub.Propiedad.areaAdministrativaNivel2 + finCadenaAreaAdmNivel2 == paqueteDatos.areaAdministrativaNivel2
                                                           select pub).ToList();
                            break;
                        case 8:
                            publicacionesFiltradasPaso1 = (from pub in db.Publicacion
                                                           where pub.Propiedad.pais == paqueteDatos.pais
                                                           select pub).ToList();
                            break;

                    }


                    #endregion
                    DTOTodosResultadosBusqueda datosPublicaciones = new DTOTodosResultadosBusqueda();
                    List<Publicacion> publicacionesFiltradasPaso2 = new List<Publicacion>();
                    #region Validaciones paso 2
                    if (paqueteDatos.tipoPropiedad != "-1" && paqueteDatos.tipoOperacion == "-1" && paqueteDatos.tipoConstruccion == "-1")
                    {
                        codCombinacion = GuiaBusqueda.eligioTipoPropiedad;
                    }
                    else if (paqueteDatos.tipoPropiedad != "-1" && paqueteDatos.tipoOperacion != "-1" && paqueteDatos.tipoConstruccion == "-1")
                    {
                        codCombinacion = GuiaBusqueda.eligioTipoPropiedadTipoOperacion;
                    }
                    else if (paqueteDatos.tipoPropiedad == "-1" && paqueteDatos.tipoOperacion != "-1" && paqueteDatos.tipoConstruccion == "-1")
                    {
                        codCombinacion = GuiaBusqueda.eligioTipoOperacion;
                    }
                    else if (paqueteDatos.tipoConstruccion != "-1" && paqueteDatos.tipoOperacion == "-1" && paqueteDatos.tipoPropiedad == "-1")
                    {
                        codCombinacion = GuiaBusqueda.eligioTipoConstruccion;
                    }
                    else if (paqueteDatos.tipoConstruccion != "-1" && paqueteDatos.tipoPropiedad != "-1" && paqueteDatos.tipoOperacion == "-1")
                    {
                        codCombinacion = GuiaBusqueda.eligioTipoPropiedadTipoConstruccion;
                    }
                    else if (paqueteDatos.tipoConstruccion != "-1" && paqueteDatos.tipoOperacion != "-1" && paqueteDatos.tipoPropiedad == "-1")
                    {
                        codCombinacion = GuiaBusqueda.eligioTipoConstruccionTipoOperacion;
                    }
                    else if (paqueteDatos.tipoConstruccion != "-1" && paqueteDatos.tipoOperacion != "-1" && paqueteDatos.tipoPropiedad != "-1")
                    {
                        codCombinacion = GuiaBusqueda.eligioTipoConstruccionTipoPropiedadTipoOperacion;
                    }
                    else
                    {
                        codCombinacion = GuiaBusqueda.eligioDefault;
                    }
                    switch (codCombinacion)
                    {
                        case 8:
                            foreach (var publicacion in publicacionesFiltradasPaso1)
                            {
                                if (publicacion.Propiedad.TipoPropiedad.nombreTipoPropiedad == paqueteDatos.tipoPropiedad)
                                {
                                    publicacionesFiltradasPaso2.Add(publicacion);

                                }

                            }
                            break;
                        case 9:
                            foreach (var publicacion in publicacionesFiltradasPaso1)
                            {
                                if (publicacion.Propiedad.TipoPropiedad.nombreTipoPropiedad == paqueteDatos.tipoPropiedad && publicacion.TipoPublicacion.nombreTipoPublicacion == paqueteDatos.tipoOperacion)
                                {
                                    publicacionesFiltradasPaso2.Add(publicacion);
                                }

                            }
                            break;
                        case 10:
                            foreach (var publicacion in publicacionesFiltradasPaso1)
                            {
                                publicacionesFiltradasPaso2.Add(publicacion);



                            }
                            break;
                        case 11:
                            foreach (var publicacion in publicacionesFiltradasPaso1)
                            {
                                if (publicacion.TipoPublicacion.nombreTipoPublicacion == paqueteDatos.tipoOperacion)
                                {
                                    publicacionesFiltradasPaso2.Add(publicacion);
                                }

                            }
                            break;
                        case 12:
                            foreach (var publicacion in publicacionesFiltradasPaso1)
                            {
                                if (publicacion.Propiedad.TipoConstruccion.nombreTipoConstruccion == paqueteDatos.tipoConstruccion)
                                {
                                    publicacionesFiltradasPaso2.Add(publicacion);
                                }

                            }
                            break;
                        case 13:
                            foreach (var publicacion in publicacionesFiltradasPaso1)
                            {
                                if (publicacion.Propiedad.TipoConstruccion.nombreTipoConstruccion == paqueteDatos.tipoConstruccion && publicacion.Propiedad.TipoPropiedad.nombreTipoPropiedad == paqueteDatos.tipoPropiedad)
                                {
                                    publicacionesFiltradasPaso2.Add(publicacion);
                                }

                            }
                            break;
                        case 14:
                            foreach (var publicacion in publicacionesFiltradasPaso1)
                            {
                                if (publicacion.Propiedad.TipoConstruccion.nombreTipoConstruccion == paqueteDatos.tipoConstruccion && publicacion.TipoPublicacion.nombreTipoPublicacion == paqueteDatos.tipoOperacion)
                                {
                                    publicacionesFiltradasPaso2.Add(publicacion);
                                }

                            }
                            break;
                        case 15:
                            foreach (var publicacion in publicacionesFiltradasPaso1)
                            {
                                if (publicacion.Propiedad.TipoConstruccion.nombreTipoConstruccion == paqueteDatos.tipoConstruccion && publicacion.TipoPublicacion.nombreTipoPublicacion == paqueteDatos.tipoOperacion && publicacion.Propiedad.TipoPropiedad.nombreTipoPropiedad == paqueteDatos.tipoPropiedad)
                                {
                                    publicacionesFiltradasPaso2.Add(publicacion);
                                }

                            }
                            break;

                    }
                    #endregion

                    #region Validaciones paso 3
                    List<PropiedadTipoAmbiente> configAmbientes = new List<PropiedadTipoAmbiente>();
                    List<Publicacion> publicacionesFiltradasPaso3 = new List<Publicacion>();
                    //Recupero instancias de PropiedadTipoAmbiente de las publicaciones filtradas hasta el momento



                    if (paqueteDatos.cantidadBaños != -1 && paqueteDatos.cantidadDormitorios == -1 && paqueteDatos.cantidadCocheras == -1)
                    {
                        codCombinacion = GuiaBusqueda.eligioCantidadBaños;
                    }
                    else if (paqueteDatos.cantidadDormitorios != -1 && paqueteDatos.cantidadBaños != -1 && paqueteDatos.cantidadCocheras == -1)
                    {
                        codCombinacion = GuiaBusqueda.eligioCantidadBañosCantidadDormitorios;
                    }
                    else if (paqueteDatos.cantidadDormitorios != -1 && paqueteDatos.cantidadBaños != -1 && paqueteDatos.cantidadCocheras != -1)
                    {
                        codCombinacion = GuiaBusqueda.eligioCantidadBañosCantidadDormitoriosCantidadCocheras;
                    }
                    else if (paqueteDatos.cantidadBaños != -1 && paqueteDatos.cantidadCocheras != -1 && paqueteDatos.cantidadDormitorios == -1)
                    {
                        codCombinacion = GuiaBusqueda.eligioCantidadBañosCantidadCocheras;
                    }
                    else if (paqueteDatos.cantidadBaños == -1 && paqueteDatos.cantidadDormitorios != -1 && paqueteDatos.cantidadCocheras != -1)
                    {
                        codCombinacion = GuiaBusqueda.eligioCantidadDormitoriosCantidadCocheras;
                    }
                    else if (paqueteDatos.cantidadDormitorios != -1 && paqueteDatos.cantidadBaños == -1 && paqueteDatos.cantidadCocheras == -1)
                    {
                        codCombinacion = GuiaBusqueda.eligioCantidadDormitorios;
                    }
                    else if (paqueteDatos.cantidadCocheras != -1 && paqueteDatos.cantidadBaños == -1 && paqueteDatos.cantidadDormitorios == -1)
                    {
                        codCombinacion = GuiaBusqueda.eligioCantidadCocheras;
                    }
                    else
                    {
                        codCombinacion = GuiaBusqueda.noEligioBDC;
                    }
                    bool cumpleCondicionBaño = false;
                    bool cumpleCondicionDormitorios = false;
                    bool cumpleCondicionCochera = false;
                    switch (codCombinacion)
                    {
                        case 16:
                            foreach (var publicacion in publicacionesFiltradasPaso2)
                            {
                                configAmbientes = publicacion.Propiedad.PropiedadTipoAmbiente.ToList();
                                foreach (var y in configAmbientes)
                                {
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Baño" && y.cantidadAmbientes == paqueteDatos.cantidadBaños)
                                    {
                                        publicacionesFiltradasPaso3.Add(publicacion);
                                    }
                                }



                            }
                            break;
                        case 17:

                            foreach (var publicacion in publicacionesFiltradasPaso2)
                            {
                                cumpleCondicionBaño = false;
                                cumpleCondicionDormitorios = false;
                                configAmbientes = publicacion.Propiedad.PropiedadTipoAmbiente.ToList();
                                foreach (var y in configAmbientes)
                                {
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Baño" && y.cantidadAmbientes == paqueteDatos.cantidadBaños)
                                    {
                                        cumpleCondicionBaño = true;

                                    }
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Dormitorio" && y.cantidadAmbientes == paqueteDatos.cantidadDormitorios)
                                    {
                                        cumpleCondicionDormitorios = true;
                                    }
                                    if (cumpleCondicionBaño == true && cumpleCondicionDormitorios == true)
                                    {
                                        publicacionesFiltradasPaso3.Add(publicacion);
                                    }
                                }



                            }
                            break;
                        case 18:

                            foreach (var publicacion in publicacionesFiltradasPaso2)
                            {
                                cumpleCondicionBaño = false;
                                cumpleCondicionDormitorios = false;
                                cumpleCondicionCochera = false;
                                configAmbientes = publicacion.Propiedad.PropiedadTipoAmbiente.ToList();
                                foreach (var y in configAmbientes)
                                {
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Baño" && y.cantidadAmbientes == paqueteDatos.cantidadBaños)
                                    {
                                        cumpleCondicionBaño = true;

                                    }
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Dormitorio" && y.cantidadAmbientes == paqueteDatos.cantidadDormitorios)
                                    {
                                        cumpleCondicionDormitorios = true;
                                    }
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Cochera" && y.cantidadAmbientes == paqueteDatos.cantidadCocheras)
                                    {
                                        cumpleCondicionCochera = true;
                                    }
                                    if (cumpleCondicionBaño == true && cumpleCondicionDormitorios == true && cumpleCondicionCochera == true)
                                    {
                                        publicacionesFiltradasPaso3.Add(publicacion);
                                    }
                                }



                            }
                            break;
                        case 19:
                            foreach (var publicacion in publicacionesFiltradasPaso2)
                            {
                                cumpleCondicionBaño = false;
                                cumpleCondicionCochera = false;
                                configAmbientes = publicacion.Propiedad.PropiedadTipoAmbiente.ToList();
                                foreach (var y in configAmbientes)
                                {
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Baño" && y.cantidadAmbientes == paqueteDatos.cantidadBaños)
                                    {
                                        cumpleCondicionBaño = true;

                                    }
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Cochera" && y.cantidadAmbientes == paqueteDatos.cantidadCocheras)
                                    {
                                        cumpleCondicionCochera = true;
                                    }

                                    if (cumpleCondicionBaño == true && cumpleCondicionCochera == true)
                                    {
                                        publicacionesFiltradasPaso3.Add(publicacion);
                                    }
                                }



                            }
                            break;
                        case 20:
                            foreach (var publicacion in publicacionesFiltradasPaso2)
                            {
                                publicacionesFiltradasPaso3.Add(publicacion);



                            }
                            break;
                        case 21:
                            foreach (var publicacion in publicacionesFiltradasPaso2)
                            {
                                cumpleCondicionDormitorios = false;
                                cumpleCondicionCochera = false;
                                configAmbientes = publicacion.Propiedad.PropiedadTipoAmbiente.ToList();
                                foreach (var y in configAmbientes)
                                {
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Dormitorio" && y.cantidadAmbientes == paqueteDatos.cantidadDormitorios)
                                    {
                                        cumpleCondicionDormitorios = true;

                                    }
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Cochera" && y.cantidadAmbientes == paqueteDatos.cantidadCocheras)
                                    {
                                        cumpleCondicionCochera = true;
                                    }

                                    if (cumpleCondicionBaño == true && cumpleCondicionCochera == true)
                                    {
                                        publicacionesFiltradasPaso3.Add(publicacion);
                                    }
                                }



                            }
                            break;
                        case 22:
                            foreach (var publicacion in publicacionesFiltradasPaso2)
                            {
                                configAmbientes = publicacion.Propiedad.PropiedadTipoAmbiente.ToList();
                                foreach (var y in configAmbientes)
                                {
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Dormitorio" && y.cantidadAmbientes == paqueteDatos.cantidadDormitorios)
                                    {
                                        publicacionesFiltradasPaso3.Add(publicacion);
                                    }
                                }



                            }
                            break;
                        case 23:
                            foreach (var publicacion in publicacionesFiltradasPaso2)
                            {
                                configAmbientes = publicacion.Propiedad.PropiedadTipoAmbiente.ToList();
                                foreach (var y in configAmbientes)
                                {
                                    if (y.TipoAmbiente.nombreTipoAmbiente == "Cochera" && y.cantidadAmbientes == paqueteDatos.cantidadCocheras)
                                    {
                                        publicacionesFiltradasPaso3.Add(publicacion);
                                    }
                                }



                            }
                            break;

                    }
                    #endregion

                    #region Validaciones paso 4
                    List<Publicacion> publicacionesFiltradasPaso4 = new List<Publicacion>();
                    int antiguedadDesde = -1;
                    int antiguedadHasta = -1;
                    int codCombinacionAntiguedad = -1;
                    if (paqueteDatos.antiguedad != -1 && paqueteDatos.cantidadPlantas == -1)
                    {
                        codCombinacion = GuiaBusqueda.eligioAntiguedad;
                    }
                    else if (paqueteDatos.cantidadPlantas != -1 && paqueteDatos.antiguedad == -1)
                    {
                        codCombinacion = GuiaBusqueda.eligioCantidadPlantas;
                    }
                    else if (paqueteDatos.cantidadPlantas != -1 && paqueteDatos.antiguedad != -1)
                    {
                        codCombinacion = GuiaBusqueda.eligioAntiguedadCantidadPlantas;
                    }
                    else
                    {
                        codCombinacion = GuiaBusqueda.noEligioAntiguedadCantidadPlantas;
                    }
                    switch (codCombinacion)
                    {
                        case 24:
                            switch (paqueteDatos.antiguedad)
                            {
                                case 0:
                                    antiguedadDesde = 0;
                                    antiguedadHasta = 0;
                                    codCombinacionAntiguedad = 0;
                                    break;
                                case 50:
                                    antiguedadDesde = 50;
                                    antiguedadHasta = 50;
                                    codCombinacionAntiguedad = 1;
                                    break;
                                case 50100:
                                    antiguedadDesde = 50;
                                    antiguedadHasta = 100;
                                    codCombinacionAntiguedad = 2;
                                    break;
                                case 100:
                                    antiguedadDesde = 100;
                                    antiguedadHasta = 100;
                                    codCombinacionAntiguedad = 3;
                                    break;
                            }
                            foreach (var publicacion in publicacionesFiltradasPaso3)
                            {
                                switch (codCombinacionAntiguedad)
                                {
                                    case 0:
                                        if (publicacion.Propiedad.antiguedad == antiguedadDesde)
                                        {
                                            publicacionesFiltradasPaso4.Add(publicacion);
                                        }
                                        break;
                                    case 1:
                                        if (publicacion.Propiedad.antiguedad <= antiguedadDesde)
                                        {
                                            publicacionesFiltradasPaso4.Add(publicacion);
                                        }
                                        break;
                                    case 2:
                                        if (publicacion.Propiedad.antiguedad >= antiguedadDesde && publicacion.Propiedad.antiguedad <= antiguedadHasta)
                                        {
                                            publicacionesFiltradasPaso4.Add(publicacion);
                                        }
                                        break;
                                    case 3:
                                        if (publicacion.Propiedad.antiguedad > antiguedadDesde)
                                        {
                                            publicacionesFiltradasPaso4.Add(publicacion);
                                        }
                                        break;
                                }
                            }
                            break;
                        case 25:

                            foreach (var publicacion in publicacionesFiltradasPaso3)
                            {

                                if (publicacion.Propiedad.nroPlantas == paqueteDatos.cantidadPlantas)
                                {
                                    publicacionesFiltradasPaso4.Add(publicacion);
                                }
                                else if ((paqueteDatos.cantidadPlantas == 4) && (publicacion.Propiedad.nroPlantas > 3))
                                {
                                    publicacionesFiltradasPaso4.Add(publicacion);

                                }

                            }
                            break;
                        case 26:
                            switch (paqueteDatos.antiguedad)
                            {
                                case 0:
                                    antiguedadDesde = 0;
                                    antiguedadHasta = 0;
                                    codCombinacionAntiguedad = 0;
                                    break;
                                case 50:
                                    antiguedadDesde = 50;
                                    antiguedadHasta = 50;
                                    codCombinacionAntiguedad = 1;
                                    break;
                                case 50100:
                                    antiguedadDesde = 50;
                                    antiguedadHasta = 100;
                                    codCombinacionAntiguedad = 2;
                                    break;
                                case 100:
                                    antiguedadDesde = 100;
                                    antiguedadHasta = 100;
                                    codCombinacionAntiguedad = 3;
                                    break;
                            }
                            foreach (var publicacion in publicacionesFiltradasPaso3)
                            {
                                switch (codCombinacionAntiguedad)
                                {
                                    case 0:
                                        if ((publicacion.Propiedad.antiguedad == antiguedadDesde && publicacion.Propiedad.nroPlantas == paqueteDatos.cantidadPlantas)
                                            || ((publicacion.Propiedad.antiguedad == antiguedadDesde) && (publicacion.Propiedad.nroPlantas > 3) && (paqueteDatos.cantidadPlantas == 4)))
                                        {
                                            publicacionesFiltradasPaso4.Add(publicacion);
                                        }
                                        break;
                                    case 1:
                                        if ((publicacion.Propiedad.antiguedad <= antiguedadDesde && publicacion.Propiedad.nroPlantas == paqueteDatos.cantidadPlantas)
                                            || ((publicacion.Propiedad.antiguedad <= antiguedadDesde) && (publicacion.Propiedad.nroPlantas > 3) && (paqueteDatos.cantidadPlantas == 4)))
                                        {
                                            publicacionesFiltradasPaso4.Add(publicacion);
                                        }
                                        break;
                                    case 2:
                                        if ((publicacion.Propiedad.antiguedad >= antiguedadDesde && publicacion.Propiedad.antiguedad <= antiguedadHasta && publicacion.Propiedad.nroPlantas == paqueteDatos.cantidadPlantas)
                                        || ((publicacion.Propiedad.antiguedad >= antiguedadDesde) && (publicacion.Propiedad.antiguedad <= antiguedadHasta) && (publicacion.Propiedad.nroPlantas > 3) && (paqueteDatos.cantidadPlantas == 4)))
                                        {
                                            publicacionesFiltradasPaso4.Add(publicacion);
                                        }
                                        break;
                                    case 3:
                                        if ((publicacion.Propiedad.antiguedad > antiguedadDesde && publicacion.Propiedad.nroPlantas == paqueteDatos.cantidadPlantas)
                                            || ((publicacion.Propiedad.antiguedad > antiguedadDesde) && (publicacion.Propiedad.nroPlantas > 3) && (paqueteDatos.cantidadPlantas == 4)))
                                        {
                                            publicacionesFiltradasPaso4.Add(publicacion);
                                        }
                                        break;
                                }
                                /*if (publicacion.Propiedad.antiguedad==paqueteDatos.antiguedad && publicacion.Propiedad.nroPlantas == paqueteDatos.cantidadPlantas) {
                                    publicacionesFiltradasPaso4.Add(publicacion);
                                }*/
                            }
                            break;
                        case 27:
                            foreach (var publicacion in publicacionesFiltradasPaso3)
                            {
                                publicacionesFiltradasPaso4.Add(publicacion);
                            }
                            break;
                    }
                    #endregion

                    #region Validaciones paso 5
                    bool cumpleCondicionExtra = false;
                    List<Publicacion> publicacionesFiltradasPaso5 = new List<Publicacion>();
                    List<PropiedadExtras> configExtras = new List<PropiedadExtras>();
                    foreach (var publicacion in publicacionesFiltradasPaso4)
                    {
                        if (paqueteDatos.extras != null)
                        {
                            configExtras = publicacion.Propiedad.PropiedadExtras.ToList();
                            foreach (var extra in configExtras)
                            {
                                if (extra.activo == true)
                                {
                                    foreach (var extraFiltro in paqueteDatos.extras)
                                    {
                                        if (extra.Extras.nombreExtra == extraFiltro)
                                        {
                                            cumpleCondicionExtra = true;
                                        }
                                    }
                                }

                            }
                            if (cumpleCondicionExtra == true)
                            {
                                publicacionesFiltradasPaso5.Add(publicacion);
                                cumpleCondicionExtra = false;
                            }
                        }
                        else
                        {
                            publicacionesFiltradasPaso5.Add(publicacion);
                        }

                    }
                    #endregion

                    #region Conversion de monedas
                    List<DTOCotizacion> dTOCotizacion = new List<DTOCotizacion>();
                    if (tipoMonedaSeleccionada != "-1")
                    {                    
                       // dTOCotizacion = ExpertoCotizacion.ConsultarCotizaciones(tipoMonedaSeleccionada);
                    }
                    #endregion


                    //precios
                    #region Validaciones paso 6 
                    int codCombinacionPrecio = -1;

                    if ((paqueteDatos.precio != -1 && paqueteDatos.precioDesde == 0 && paqueteDatos.precioHasta == 0) || (paqueteDatos.precio == -1 && paqueteDatos.precioDesde == 0 && paqueteDatos.precioHasta == 0))// ESTA VALIDACION ESTA OK
                    {
                        codCombinacionPrecio = 0; //selecciona una opcion o no selecciona ninguna opcion del listbox, dejando los input vacios
                    }
                    else
                    {
                        if (paqueteDatos.precio == -1 && paqueteDatos.precioDesde >= 0 && paqueteDatos.precioHasta > 0)//ESTA VALIDACION ESTA OK
                        {
                            codCombinacionPrecio = 1; // ingresa un precio minimo y un precio maximo, dejando el precio rapido sin seleccionar
                        }
                    }

                    List<Publicacion> publicacionesFiltradasPaso6 = new List<Publicacion>();
                    switch (codCombinacionPrecio)
                    {
                        case 0:
                            foreach (var publicacion in publicacionesFiltradasPaso5)
                            {
                                switch (paqueteDatos.precio)
                                {
                                    case -1:
                                        publicacionesFiltradasPaso6.Add(publicacion);
                                        break;
                                    case 500000:
                                        if (publicacion.precioPropiedad <= 500000)
                                        {
                                            publicacionesFiltradasPaso6.Add(publicacion);
                                        }
                                        break;
                                    case 500100:
                                        if (publicacion.precioPropiedad >= 500000 && publicacion.precioPropiedad <= 1000000)
                                        {
                                            publicacionesFiltradasPaso6.Add(publicacion);
                                        }
                                        break;
                                    case 100200:
                                        if (publicacion.precioPropiedad >= 1000000 && publicacion.precioPropiedad <= 2000000)
                                        {
                                            publicacionesFiltradasPaso6.Add(publicacion);
                                        }
                                        break;
                                    case 2000000:
                                        if (publicacion.precioPropiedad >= 2000000)
                                        {
                                            publicacionesFiltradasPaso6.Add(publicacion);
                                        }
                                        break;
                                }
                            }
                            break;
                        case 1:
                            foreach (var publicacion in publicacionesFiltradasPaso5)
                            {
                               if (publicacion.precioPropiedad >= paqueteDatos.precioDesde && publicacion.precioPropiedad <= paqueteDatos.precioHasta && publicacion.TipoMoneda.nombreTipoMoneda == tipoMonedaSeleccionada)
                                {
                                    publicacionesFiltradasPaso6.Add(publicacion);
                                }

                                for (int cont = 0; (cont < dTOCotizacion.Count()); cont++)
                                {
                                    if (publicacion.precioPropiedad >= ((decimal)dTOCotizacion.ElementAt(cont).value * paqueteDatos.precioDesde) &&
                                        publicacion.precioPropiedad <= ((decimal)dTOCotizacion.ElementAt(cont).value * paqueteDatos.precioHasta) 
                                        && publicacion.TipoMoneda.nombreTipoMoneda == dTOCotizacion.ElementAt(cont).target)
                                        {
                                            publicacionesFiltradasPaso6.Add(publicacion);
                                        }
                                    }
                            }
                            break;
                    }
                    #endregion

                    #region Validaciones paso 7
                    List<Publicacion> publicacionesFiltradasPaso7 = new List<Publicacion>();
                    if (paqueteDatos.radioElegido != -1)
                    {
                        #region Validaciones Paso 7

                        DTOPosicion origen = new DTOPosicion(paqueteDatos.latitudOrigen, paqueteDatos.longitudOrigen);
                        foreach (var publicacion in publicacionesFiltradasPaso6)
                        {
                            DTOPosicion destino = new DTOPosicion(publicacion.Propiedad.latitud, publicacion.Propiedad.longitud);
                            double distancia = DistanciaDosPuntosGeograficosServicios.CalculoFormulaHaversine(origen, destino);
                            int distanciaEntero = Convert.ToInt32(distancia*1000);
                            if (distanciaEntero < paqueteDatos.radioElegido * 1000)
                            {
                                publicacionesFiltradasPaso7.Add(publicacion);
                            }
                        }
                        #endregion
                    }
                    else {
                        publicacionesFiltradasPaso7 = publicacionesFiltradasPaso6;
                    }

                    #endregion
                    //superficieTerreno
                    #region validaciones paso 8
                    List<Publicacion> publicacionesFiltradasPaso8 = new List<Publicacion>();
                    if(paqueteDatos.superficieTerrenoMax > 0 && paqueteDatos.superficieTerrenoMin > 0)
                    {
                        foreach(var pub in publicacionesFiltradasPaso7)
                        {
                            if(pub.Propiedad.superficieTerreno >= paqueteDatos.superficieTerrenoMin && pub.Propiedad.superficieTerreno <= paqueteDatos.superficieTerrenoMax)
                            {
                                publicacionesFiltradasPaso8.Add(pub);
                            }
                        }
                    }
                    else
                    {
                        publicacionesFiltradasPaso8 = publicacionesFiltradasPaso7;
                    }
                    #endregion

                    //Lleno DTO con todos los datos filtrados
                    foreach (var publicacion in publicacionesFiltradasPaso8)
                    {
                        DTOMarcadorGoogleMaps dtoMarcadorGoogleMaps = new DTOMarcadorGoogleMaps();
                        DTOResultadoBusquedaPublicacion datoPublicacion = new DTOResultadoBusquedaPublicacion();
                        datoPublicacion.publicacionId = publicacion.publicacionId;
                        datoPublicacion.direccionPropiedad = publicacion.Propiedad.calle;
                        datoPublicacion.pais = publicacion.Propiedad.pais;
                        datoPublicacion.provincia = publicacion.Propiedad.areaAdministrativaNivel1;
                        datoPublicacion.ciudad = publicacion.Propiedad.areaAdministrativaNivel2;
                        datoPublicacion.nroCalle = publicacion.Propiedad.nroCalle;

                        if(publicacion.TipoMoneda.nombreTipoMoneda == "ARS") datoPublicacion.precioPropiedad = publicacion.precioPropiedad.ToString("N", CultureInfo.CreateSpecificCulture("es-AR"));
                        if(publicacion.TipoMoneda.nombreTipoMoneda == "EUR") datoPublicacion.precioPropiedad = publicacion.precioPropiedad.ToString("N", CultureInfo.CreateSpecificCulture("es-ES"));
                        if(publicacion.TipoMoneda.nombreTipoMoneda == "USD") datoPublicacion.precioPropiedad = publicacion.precioPropiedad.ToString("N", CultureInfo.CreateSpecificCulture("en-EU"));
                        
                        datoPublicacion.tipoMoneda = publicacion.TipoMoneda.nombreTipoMoneda;
                        datoPublicacion.fechaInicioPublicacion = publicacion.fechaInicioPublicacion;
                        datoPublicacion.tipoPropiedad = publicacion.Propiedad.TipoPropiedad.nombreTipoPropiedad;
                        datoPublicacion.tipoOperacion = publicacion.TipoPublicacion.nombreTipoPublicacion;
                        datosPublicaciones.tipomonedaseleccionada = tipoMonedaSeleccionada;

                        dtoMarcadorGoogleMaps.latitud = publicacion.Propiedad.latitud;
                        dtoMarcadorGoogleMaps.longitud = publicacion.Propiedad.longitud;
                        dtoMarcadorGoogleMaps.precioPropiedad = string.Format("{0:c}", publicacion.precioPropiedad);
                        dtoMarcadorGoogleMaps.ubicacion = publicacion.Propiedad.direccionFormateada;
                        dtoMarcadorGoogleMaps.tipoPropiedad = publicacion.Propiedad.TipoPropiedad.nombreTipoPropiedad;
                        dtoMarcadorGoogleMaps.publicacionId = publicacion.publicacionId;
                        dtoMarcadorGoogleMaps.tipoMoneda = publicacion.TipoMoneda.nombreTipoMoneda;
                        string ruta = ImagenServicios.ObtenerRutaRelativa(publicacion.ImagenPublicacion.Where(x => x.imagenRepresentativa == true).FirstOrDefault());
                        datoPublicacion.rutaImagenBD.Add(ruta);
                        /*foreach (var imagen in publicacion.ImagenPublicacion)
                        {
                            string ruta = @imagen.rutaBD;

                            if (String.IsNullOrEmpty(imagen.rutaBD))
                            {
                                ruta = "rutaInvalida";
                            }
                            else {

                                //ruta ="/"+ imagen.rutaBD.Substring(paqueteDatos.rutaRelativa.Length).Replace("\\", "/");
                                ruta = ImagenServicios.ObtenerRutaRelativa(imagen);
                                //ruta = imagen.rutaBD.Replace("\\", "/");
                            }
                            
                            
                            
                            datoPublicacion.rutaImagenBD.Add(ruta);
                            //datoPublicacion.imagen.Add(imagen.imagen);

                        }*/
                        datosPublicaciones.AgregarDTOUnResultadoPublicacion(datoPublicacion);
                        datosPublicaciones.AgregarDTOCoordenadasPropiedad(dtoMarcadorGoogleMaps);
                    }

                    return datosPublicaciones;


                }




            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}