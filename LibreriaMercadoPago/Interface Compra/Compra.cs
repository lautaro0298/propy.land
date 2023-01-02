using LibreriaClases;
using LibreriaClases.DTO;
using LibreriaMercadoPago.DTO;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaMercadoPago.Interface_Compra
{
    public interface Compra<T>
    {
        (ErrorPropy, List<T>) ObtenerPlanes_o_Credito();
        (ErrorPropy, DTOCompra) ArmarDTOCompra<R>(string user, List<R> dTO) where R : T;

    }
}
