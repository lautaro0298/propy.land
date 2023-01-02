using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebApp.DTO;
using WebApp.Models;

namespace WebApp.Fachada
{
    public interface IRepository<T> where T:class
    {
        List<T> Buscar(Expression<Func<T, bool>> predicate,ApplicationDbContext conexion);
        T BuscarPorId(string id, ApplicationDbContext conexion);
        void Crear(T entidad, ApplicationDbContext conexion);
        void Editar(T entidad, ApplicationDbContext conexion);
        void Guardar(ApplicationDbContext conexion);
    }
}