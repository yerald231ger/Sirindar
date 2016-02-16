using System;
using System.Linq;
using System.Data.Entity;
using CNSirindar.Models;
using System.Collections.Generic;
using CNSirindar.Extensions;

namespace CNSirindar.Repositories
{
    public class HorarioRepository : IRepository<Horario, int>
    {
        public List<Horario> List()
        {
            using (var db = new SirindarDbContext())
            {
                return db.Horarios.WhereIsActive();
            }
        }

        public bool Delete(int? id)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    var entity = db.Horarios.Find(id);
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

        public bool Update(Horario entity)
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

        public bool Create(Horario entity)
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

        public Horario Read(int? key)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    return db.Horarios.Find(key);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

    }

}
