using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DTO;
using WebApp.Fachada;
using WebApp.Models;

namespace WebApp.Servicios
{
    public static class DatosCompraServicios
    {
        public static DTOCompras LlenarDatos(string obj)
        {
            DTOCompras dTOCompras = new DTOCompras();

            if(obj == "plan")
            {
                using(var database = new ApplicationDbContext())
                {
                    Repository<Plan> repositoryPlan = new Repository<Plan>();
                    var plan = repositoryPlan.Buscar(x => x.activo == true, database).OrderBy(x => x.precioPlan).ToList();//wtf

                    foreach (var pl in plan)
                    {
                        DatosCompra datosCompra = new DatosCompra();

                        datosCompra.cantidadMaxImagenesPermitidasPorPub = pl.cantidadMaxImagenesPermitidasPorPub;
                        datosCompra.creditos = pl.creditos;
                        datosCompra.moneda = pl.moneda;
                        datosCompra.nombre = pl.nombrePlan;
                        datosCompra.precio = pl.precioPlan;
                        datosCompra.accesoEstadisticasPremium = pl.accesoEstadisticasPremium;
                        datosCompra.vencimientoCreditos = pl.vencimientoCreditos;
                        datosCompra.subirVideos = pl.subirVideos;
                        datosCompra.ID = pl.itemId;

                        dTOCompras.AddcontratarPlan(datosCompra);
                    }
                }
            }
            else
            {
                if(obj == "PackCreditos")
                {
                    using(var database = new ApplicationDbContext())
                    {
                        Repository<CreditoPlan> repositoryPackCreditos = new Repository<CreditoPlan>();
                        var PackCreditos = repositoryPackCreditos.Buscar(x => x.activo == true, database).OrderBy(x => x.precioPlanCredito).ToList();

                        foreach (var pC in PackCreditos)
                        {
                            DatosCompra datosCompra = new DatosCompra();

                            datosCompra.creditos = pC.cantidadTotalCreditoPorPaquete;
                            datosCompra.moneda = pC.moneda;
                            datosCompra.precio = pC.precioPlanCredito;
                            datosCompra.activo = true;
                            datosCompra.ID = Convert.ToString(pC.CreditoPlanID);

                            dTOCompras.AddcontratarPlan(datosCompra);
                        }
                    }
                }
            }
            return dTOCompras;
        }
    }
}