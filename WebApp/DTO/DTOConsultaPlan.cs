using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOConsultaPlan
    {
        public Guid planID { get; set; }
        public string nombrePlan { get; set; }
        public string monedaPlan { get; set; }
        public string precioPlan { get; set; }
        public bool nullable { get; set; }


        #region Características Plan
        public bool accesoEstadisticasAvanzadas { get; set; }
        public int cantidadMaximaImagenesPublicacion { get; set; }
        #endregion
    }
}