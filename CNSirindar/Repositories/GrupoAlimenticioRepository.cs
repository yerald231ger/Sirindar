using CNSirindar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CNSirindar.Extensions;

namespace CNSirindar.Repositories
{
    public class GrupoAlimenticioRepository : IRepository<GrupoAlimenticio, int>
    {
        public GrupoAlimenticio Read(int? id)
        {
            var entity = new GrupoAlimenticio();
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity = db.GruposAlimenticios.Include("Grupos").First(ga => ga.GrupoAlimenticioId == id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public bool Create(GrupoAlimenticio entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
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

        public bool Update(GrupoAlimenticio entity)
        {
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity.EsActivo = true;
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
                    var entity = db.Grupos.Find(id);
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

        public List<GrupoAlimenticio> List()
        {
            var list = new List<GrupoAlimenticio>();
            using (var db = new SirindarDbContext())
            {
                db.GruposAlimenticios.WhereIsActive().ForEach(ga =>
                        list.Add(new GrupoAlimenticio
                        {
                            GrupoAlimenticioId = ga.GrupoAlimenticioId,
                            Grupo = ga.Grupo
                        })
                    );
            }
            return list;
        }
    }
}