using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Servicios
{
    public class PropiedadTipoAmbienteServicios
    {
        public void CrearPropiedadTipoAmbiente(Guid propiedadId,List<Guid> tipoAmbienteId,List<int> cantidadAmbientes) {
            try
            {
                using (var db = new ApplicationDbContext()) {
                    for (int i=0;i< tipoAmbienteId.Count;i++) {
                        var propiedadTipoAmbienteId = System.Guid.NewGuid();
                        PropiedadTipoAmbiente pta = new PropiedadTipoAmbiente();

                        

                        pta.propiedadTipoAmbienteId = propiedadTipoAmbienteId;
                        pta.propiedadId = propiedadId;
                        pta.tipoAmbienteId = tipoAmbienteId[i];
                        pta.cantidadAmbientes = cantidadAmbientes[i];

                        db.PropiedadTipoAmbiente.Add(pta);
                        db.SaveChanges();
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