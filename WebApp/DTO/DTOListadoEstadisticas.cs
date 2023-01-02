using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DTO;

namespace WebApp.DTO
{
    public class DTOListadoEstadisticas
    {
        public string propiedad { get; set; }
        public List<DTOEstadistica> listadoDTOEstadistica = new List<DTOEstadistica>();

        public void AgregarDTOEstadistica(DTOEstadistica dto) {
            listadoDTOEstadistica.Add(dto);
        }
    }
}