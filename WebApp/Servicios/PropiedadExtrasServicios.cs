using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Servicios
{
    public class PropiedadExtrasServicios
    {
        public void Crear(Guid propiedadId, List<Guid> extrasId) {
            
            try
            {
                using (var db = new ApplicationDbContext()) {
                    //Itero por cada Seleccion de Extras que realizó el Cliente
                    if (extrasId!=null) {
                        foreach (var item in extrasId)
                        {
                            //Defino un nuevo identificador único para PropiedadExtras
                            var propiedadExtrasId = System.Guid.NewGuid();
                            PropiedadExtras propiedadExtras = new PropiedadExtras();
                            propiedadExtras.extrasId = item;
                            propiedadExtras.propiedadId = propiedadId;
                            propiedadExtras.propiedadExtrasId = propiedadExtrasId;
                            db.PropiedadExtras.Add(propiedadExtras);
                            db.SaveChanges();
                        }
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}