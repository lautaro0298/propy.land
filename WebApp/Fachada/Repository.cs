using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebApp.Models;

namespace WebApp.Fachada
{
    public class Repository<T>:IRepository<T> where T:class
    {
        
        
        public List<T> Buscar(Expression<Func<T, bool>> predicado,ApplicationDbContext conexion)
        {
            return conexion.Set<T>().Where(predicado).ToList();
        }
        public void Crear(T entidad,ApplicationDbContext conexion) {
            conexion.Set<T>().Add(entidad);
        }
        public void Editar(T entidad, ApplicationDbContext conexion) {
            conexion.Entry(entidad).State = System.Data.Entity.EntityState.Modified;
        }
        public void Guardar(ApplicationDbContext conexion) {
            conexion.SaveChanges();
        }
        public T BuscarPorId(string id, ApplicationDbContext conexion) {
            return conexion.Set<T>().Find(id);
        }
    }
}