using CNSirindar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using CNSirindar.Extensions;

namespace CNSirindar.Repositories
{
    public class AsignacionBloqueRepository : IRepository<AsignacionBloque,int>
    {
        public bool Create(AsignacionBloque entity)
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

        public AsignacionBloque Read(int? id)
        {
            var entity = new AsignacionBloque();
            using (var db = new SirindarDbContext())
            {
                try
                {
                    entity = db.AsigancionesBloques
                        .Include(dd => dd.Deporte)
                        .Include(dd => dd.Deportista)
                        .Include(dd => dd.Bloque)
                        .FirstIsActive(dd => dd.AsignacionBloqueId == id);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return entity;
        }

        public bool Update(AsignacionBloque entity)
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
                    var entity = db.AsigancionesBloques.Find(id);
                    entity.FechaModificacion = DateTime.Now;
                    var cantidad = db.AsigancionesBloques
                        .WhereIsActive()
                        .Count(ab => ab.DeportistaId == entity.DeportistaId && ab.DeporteId == entity.DeporteId);

                    if (cantidad <= 1)
                    {
                        entity.BloqueId = null;
                    }else
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
                

        public List<AsignacionBloque> List()
        {
            throw new NotImplementedException();
        }
    }
}