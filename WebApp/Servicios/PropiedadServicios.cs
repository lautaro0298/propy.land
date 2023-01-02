using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebApp.Models;
using WebApp.DTO;

namespace WebApp.Servicios
{
    public class PropiedadServicios
    {
        
        public Guid CrearPropiedad(int añosAntiguedad,string direccion,string userId,Guid pais,Guid provincia,Guid ciudad,Guid tipoConstruccion,Guid tipoPropiedad,float superficieCubierta,float superficieTerreno,int nroPlantas) {
            using (var db = new ApplicationDbContext()) {
                try
                {
                    //Configuro la Instancia de Propiedad que se va a crear
                    Guid propiedadId = System.Guid.NewGuid();
                    Propiedad propiedad = new Propiedad();
                    propiedad.propiedadId = propiedadId;
                    propiedad.antiguedad = añosAntiguedad;
                    propiedad.calle = direccion;
                    
                    propiedad.tipoConstruccionId = tipoConstruccion;
                    propiedad.tipoPropiedadId = tipoPropiedad;
                    propiedad.fechaRegistro = DateTime.Now;
                    propiedad.superficieCubierta = superficieCubierta;
                    propiedad.superficieTerreno = superficieTerreno;
                    propiedad.nroPlantas = nroPlantas;
                    
                    propiedad.UserId = userId;
                    

                    //Mando Orden a la bd de crear la instancia
                    db.Propiedad.Add(propiedad);
                    //Guardo cambios
                    db.SaveChanges();
                    //Devuelvo el identificador de la propiedad que se acaba de crear
                    return propiedadId;
                }
                catch (Exception)
                {

                    throw;
                }
                
                
            }
        }

        
        }
 
   }
