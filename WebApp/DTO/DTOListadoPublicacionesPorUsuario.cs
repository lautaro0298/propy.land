using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.DTO
{
    public class DTOListadoPublicacionesPorUsuario
    {
        public List<DTOPublicacionUsuario> dtoContenedor = new List<DTOPublicacionUsuario>();

        public void AgregarDtoPublicacionUsuario(DTOPublicacionUsuario dto) {
            
            dtoContenedor.Add(dto);
        }
    }
}