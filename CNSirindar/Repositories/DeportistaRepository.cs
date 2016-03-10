using System;
using System.Linq;
using System.Data.Entity;
using CNSirindar.Models;
using System.Collections.Generic;
using CNSirindar.Extensions;
using System.Data.Entity.Validation;

namespace CNSirindar.Repositories
{
    public class DeportistaRepository : IRepository<Deportista, int>
    {
        public Deportista Read(int? id)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    return db.Deportistas.Include(d => d.Dependencia).FirstIsActive(d => d.DeportistaId == id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool Create(Deportista entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
                    entity.FechaAlta = DateTime.Now;
                    entity.FechaRegistro = DateTime.Now;
                    db.Entry(entity).State = EntityState.Added;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Update(Deportista entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
                    entity.FechaModificacion = DateTime.Now;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public bool Delete(int? id)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var entity = db.Deportistas.Find(id);
                    entity.FechaModificacion = DateTime.Now;
                    entity.EsActivo = false;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public List<Deportista> List()
        {
            using (var db = new SirindarDbContext())
            {
                return db.Deportistas.WhereIsActive().Select(d =>
                         new Deportista(d.DeportistaId)
                         {
                             Nombre = d.Nombre,
                             Apellidos = d.Apellidos,
                             Matricula = d.Matricula,
                             Genero = d.Genero,
                             Dependencia = new Dependencia { DependenciaId = d.Dependencia.DependenciaId, Clave = d.Dependencia.Clave, Nombre = d.Dependencia.Nombre },
                             Status = d.Status,
                         }).ToList();
            }
        }

    }
}
