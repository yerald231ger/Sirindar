using CNSirindar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CNSirindar.Extensions;

namespace CNSirindar.Repositories
{
    public class DependenciaRepository : IRepository<Dependencia, int>
    {

        public bool Create(Dependencia entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
                    entity.FechaAlta = DateTime.Now;
                    db.Entry(entity).State = EntityState.Added;
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        public Dependencia Read(int? id)
        {
            var entity = new Dependencia();
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity = db.Dependencias.Find(id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public bool Update(Dependencia entity)
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

        public List<Dependencia> List()
        {
            var list = new List<Dependencia>();
            using (var db = new SirindarDbContext())
            {
                db.Dependencias.WhereIsActive().ForEach(d =>
                        list.Add(new Dependencia
                        {
                            DependenciaId = d.DependenciaId,
                            Nombre = d.Nombre,
                            Clave = d.Clave
                        })
                    );
            }
            return list;
        }
        
    }
}